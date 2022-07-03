using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nis_front_razer.Models;

namespace nis_front_razer.Pages;

public class Results : PageModel
{
    [BindProperty(SupportsGet = true)] public List<string> TestTasks { get; set; }
    [BindProperty(SupportsGet = true)] public List<string> Answers { get; set; }
    [BindProperty(SupportsGet = true)] public RecommendationModel RecommendationModel { get; set; }
    [BindProperty(SupportsGet = true)] public List<bool> Validations { get; set; }

    public static string[] Recommendations { get; set; }
    public static Guid Id { get; set; }
    public static List<string> s_Answers { get; set; }
    static HttpClient Client = new HttpClient();
    
    public async Task OnGetAsync(Guid id, List<string> answers)
    {
        TestTasks = JsonSerializer.Deserialize<TestModel>(System.IO.File.ReadAllText("test.json")).testTasks;
        Answers = answers;
        s_Answers = answers;
        Id = id;
        var response =
            await Client.PostAsJsonAsync($"http://172.17.0.1:5065/TRAM/get-new-recommendations/", new {id, answers});
        var content = response.Content.ReadAsStringAsync().Result;
        RecommendationModel = JsonSerializer.Deserialize<RecommendationModel>(content);
        Validations = RecommendationModel.validationCheck;
        Recommendations = RecommendationModel.recommendationText.Split('\n');
    }

    public async Task<IActionResult> OnPostAsync()
    {
        return RedirectToPage("/Recommendations", new {id = Id, answers = s_Answers});
    }
}