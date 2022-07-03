namespace TGM;

/// <summary>
/// Объект студент с айди и тестом для студента.
/// </summary>
/// <param name="Id">Айди <see cref="Guid"/>.</param>
/// <param name="StudTest">Тест <see cref="Test"/></param>
[Serializable]
public record Student(Guid Id, Test StudTest);