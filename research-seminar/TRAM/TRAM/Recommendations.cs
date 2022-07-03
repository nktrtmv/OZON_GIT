namespace TRAM;

/// <summary>
/// Объект рекоммендаций для ученика по тесту.
/// </summary>
/// <param name="ValidationCheck">Словарь с результатами проверки теста.</param>
/// <param name="RecommendationText">Рекомендация ученику.</param>
[Serializable]
public record Recommendations(List<bool> ValidationCheck, string RecommendationText);