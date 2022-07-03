using System.Formats.Asn1;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nis_front_razer.Models;
using System.IO;

namespace nis_front_razer.Pages;

public class Test : PageModel
{
    [BindProperty(SupportsGet = true)]
    public UserModel? UserModel { get; set; }
    [BindProperty]
    public TestModel TestModel { get; set; }

    [BindProperty] 
    public List<string> Answers { get; set; }
    [BindProperty]
    public static Guid TestId { get; set; }
    
    
    public static List<string> TestTasks { get; set; }
    
    public async Task OnGetAsync()
    {
        var client = new HttpClient();
        var response =
            await client.GetAsync($"http://172.17.0.1:5022/TGM/get-new-test/{UserModel.Level}/{UserModel.Subject}");
        var content = response.Content.ReadAsStringAsync().Result;
        System.IO.File.WriteAllText("test.json", content);
        TestModel = JsonSerializer.Deserialize<TestModel>(content);
        if (TestModel == null)
            RedirectToPage("/Error");
        if (TestModel != null)
        {
            TestId = TestModel.id;
            Answers = new List<string>(new string[TestModel.testTasks.Count]);
            for (int i = 0; i < Answers.Count; i++)
            {
                Answers[i] = "";
            }
            TestTasks = TestModel.testTasks;
        }
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        for (int i = 0; i < Answers.Count; i++)
        {
            if (string.IsNullOrEmpty(Answers[i]))
                Answers[i] = "-";
        }
        
        return RedirectToPage("/Results", new {id = TestId, answers = Answers});
    }
}