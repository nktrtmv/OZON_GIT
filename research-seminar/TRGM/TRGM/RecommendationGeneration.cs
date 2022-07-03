using System.Text;

namespace TRGM;

/// <summary>
/// Класс для создания рекомендаций.
/// </summary>
public static class RecommendationGeneration
{
    /// <summary>
    /// Метод для создания генераций
    /// </summary>
    /// <param name="test">объъект теста с ответами студента.</param>
    /// <returns></returns>
    public static Recommendations? GetRecommendations(Test test)
    {
        if (test.StudentAnswers == null)
            return null;

        // Проверка теста.
        var isCorrect = test.TestAnswers.Select((t, i) => test.StudentAnswers[i] == t).ToList();

        // Создание списка тем, в которых у ученика есть ошибки.
        var recThemes = new List<string>();
        for (var i = 0; i < isCorrect.Count; i++)
        {
            if (!isCorrect[i])
                recThemes.Add(test.TestThemes[i]);
        }

        // Генерация рекомендаций в соответствии с результатом.
        
        if (recThemes.Count == 0)
        {
            return new Recommendations(isCorrect, "Идеальный результат! Все темы усвоены!");
        }
        
        var sb = new StringBuilder("");

        if (recThemes.Count < test.TestAnswers.Count / 3)
        {
            sb.Append("Неплохой результат, но следующие темы стоит повторить:");
        }
        else if (recThemes.Count < test.TestAnswers.Count * 2 / 3)
        {
            sb.Append("Результат удовлетворительный. Настоятельно рекомендуем повторить следующие темы:");
        }
        else
        {
            sb.Append("Результат неудовлетворительный. Следующие темы необходимо повторить:");
        }
        
        Array.ForEach(recThemes.ToArray(), theme => sb.Append("\n" + theme));

        // Возвращение нового объекта рекомендаций.
        return new Recommendations(isCorrect, sb.ToString());
    }
}