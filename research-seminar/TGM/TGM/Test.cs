using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TGM;

/// <summary>
/// Объект тест с заданиями и ответами и информацией о тесте.
/// </summary>
[Serializable]
public class Test
{
    [Key]
    public int Id { get; set; }
    public List<string>? TestTasks { get; set; }
    public List<string>? TestAnswers { get; set; }
    public List<string>? TestThemes { get; set; }
    public Subjects Subject { get; set; }
    public int Level { get; set; }

    public Test(List<string> testTasks, List<string> testAnswers, List<string> testThemes, Subjects subject, int level)
    {
        TestTasks = testTasks;
        TestAnswers = testAnswers;
        TestThemes = testThemes;
        Subject = subject;
        Level = level;
    }

    public Test(List<string> testTasks, List<string> testAnswers, List<string> testThemes)
    {
        TestTasks = testTasks;
        TestAnswers = testAnswers;
        TestThemes = testThemes;
    }

    public Test() {}
}