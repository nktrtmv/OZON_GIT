using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TRAM;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Добавление HttpClient для отправки http запросов.
builder.Services.AddHttpClient();
HttpClient client = new();

// Подключение базы данных EntitySql
string? connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddDbContext<TestDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseExceptionHandler("/Error");

// Обработчик пост запросов для добавления студентов в базу данных.
app.MapPost("TRAM/post-new-student", async (HttpContext context, [FromServices]TestDbContext db, [FromBody]Student student) =>
{
    Test test = new(student);
    await db.Tests.AddAsync(test);
    await db.SaveChangesAsync();
});

// Обработчик гет запроса для получения рекомендаций и результатов теста.
app.MapPost("TRAM/get-new-recommendations", async (HttpContext context, [FromServices]TestDbContext db, [FromBody]IDAnswers answersWithId) =>
{
    Guid studentId = answersWithId.Id;
    List<string> studentAnswers = answersWithId.Answers;

    // Получение студента по айди из базы данных, если не найден - ошибка.
    Test? test = await db.Tests.FirstOrDefaultAsync(t => t.StudentId == studentId);
    if (test == null)
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Id not found. There is no student object in Database...\n" +
                                              "U need to get test for this student firstly...");
        return;
    }
    
    test.StudentAnswers = studentAnswers;

    // Отправка пост запроса в микросервис генерирующий рекомендции, для получени этих самых рекомендаций.
    var recommendationsResponse = await client.PostAsJsonAsync("http://172.17.0.1:5082/TRGM/get-recommendations", test);

    if (recommendationsResponse.StatusCode == HttpStatusCode.NotFound)
    { 
        context.Response.StatusCode = 404; 
        await context.Response.WriteAsync("Error. System failure.");
        return;
    }
    
    // Получение объекта рекомендаций.
    var recommendations = await recommendationsResponse.Content.ReadFromJsonAsync<Recommendations>(); 

    if (recommendations == null)
    { 
        context.Response.StatusCode = 404; 
        await context.Response.WriteAsync("Error. System failure.");
    }
    else
    { 
        // Отправка ответа на гет запрос.
        await context.Response.WriteAsJsonAsync(recommendations);
    }
});

// Если проект в разработке - используется swagger.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();