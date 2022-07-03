using Microsoft.AspNetCore.Mvc;
using TRGM;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler("/Error");

// Обработка пост запроса для получения рекомендаций.
app.MapPost("TRGM/get-recommendations", handler: async (HttpContext context, [FromBody]Test test) =>
{
    // Если у ученика не ответов - ошибка.
    if (test.StudentAnswers == null)
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("There is no student answers in json in request...");
        return;
    }

    // Получение рекомендаций и отправка овтета на запрос.
    Recommendations? recommendations = RecommendationGeneration.GetRecommendations(test);
    if (recommendations is null)
    {
        context.Response.StatusCode = 404; 
        await context.Response.WriteAsync("Error. System failure. Bad recommendation generation.");
        return;
    }
    await context.Response.WriteAsJsonAsync(recommendations);
});

// Если проект в разработке - используется swagger.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();