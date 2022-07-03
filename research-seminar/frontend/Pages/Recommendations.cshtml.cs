using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nis_front_razer.Models;
using System.Linq;
using HostingEnvironmentExtensions = Microsoft.AspNetCore.Hosting.HostingEnvironmentExtensions;

namespace nis_front_razer.Pages;

public class Recommendations : PageModel
{
    [BindProperty]
    public RecommendationModel RecommendationModel { get; set; }
    [BindProperty]
    public string[] RecommendationsLines { get; set; }
    [BindProperty]
    public string RecommendationsHeader { get; set; }
    [BindProperty]
    public string[] Topics { get; set; }
    [BindProperty]
    public double Score { get; set; }
    
    static HttpClient Client = new HttpClient();
    
    public async Task OnGetAsync(Guid id, List<string> answers)
    {
        var response =
            await Client.PostAsJsonAsync($"http://172.17.0.1:5065/TRAM/get-new-recommendations/", new {id, answers});
        var content = response.Content.ReadAsStringAsync().Result;
        RecommendationModel = JsonSerializer.Deserialize<RecommendationModel>(content);
        if (RecommendationModel != null) 
            RecommendationsLines = RecommendationModel.recommendationText.Split('\n');
        RecommendationsHeader = RecommendationsLines[0];
        Topics = RecommendationsLines[1..];
        Score = (double)(RecommendationModel.validationCheck.Count(x => x)) / RecommendationModel.validationCheck.Count;
        Console.WriteLine(Score);
        Score = Math.Floor(Score * 100);
        Console.WriteLine(Score);
    }
}