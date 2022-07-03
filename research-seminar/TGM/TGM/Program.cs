using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Newtonsoft.Json;
using TGM;

var builder = WebApplication.CreateBuilder(args);

// Добавление HttpClient для отправки http запросов.
builder.Services.AddHttpClient();
HttpClient client = new();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler("/Error");

// HttpGet запрос для получения теста по параметрам в Uri
// Параметры в Uri обязательны
app.MapGet("TGM/get-new-test/{level:int:range(9,11):required}/{subject:required}", 
    async (HttpContext context, int level, string subject) =>
{
    // Если в запросе некорректный предмет - выдаем ошибку
    if (subject != "math" && subject != "rus")
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Requested subject is not supported");
        return;
    }
    Subjects sub = subject == "math" ? Subjects.Math : Subjects.Rus;
    
    // Получение теста из микросервиса парсера тестов (TPM)
    var testDictList = await client.GetFromJsonAsync<List<Dictionary<string,List<string>>>>($"http://172.17.0.1:8000/TPM/get-new-test/{level}/{subject}");
    Dictionary<string, List<string>>? testDict;
    Test? test;
    testDict = testDictList == null ? 
        JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText($@"tests/{sub.ToString().ToLower()}-{level}.json")) 
        : testDictList[0];
    test = new(testDict["testTasks"], testDict["testAnswers"], testDict["testThemes"], sub, level);

    // Создается объект student и отправляется пост запрос в микросервис TRAM.
    Student student = new(Guid.NewGuid(), test);
    await client.PostAsJsonAsync("http://172.17.0.1:5065/TRAM/post-new-student", student);
    
    // Ответ на Get запрос объектом в котором айди студента и тест.
    await context.Response.WriteAsJsonAsync(new {student.Id, student.StudTest.TestTasks});
});


// Если проект в разработке - используется swagger.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();


