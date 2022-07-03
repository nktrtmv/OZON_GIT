using System.ComponentModel.DataAnnotations;

namespace nis_front_razer.Models;

[Serializable]
public class TestModel
{
    public Guid id { get; set; }
    public List<string> testTasks { get; set; }
}