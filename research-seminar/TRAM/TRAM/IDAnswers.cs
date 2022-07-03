namespace TRAM;

/// <summary>
/// Объект с айди ученика и его ответами.
/// </summary>
/// <param name="Id">Айди ученика.</param>
/// <param name="Answers">Ответы ученика.</param>
[Serializable]
public record IDAnswers(Guid Id, List<string> Answers);