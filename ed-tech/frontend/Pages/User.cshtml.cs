using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nis_front_razer.Models;

namespace nis_front_razer.Pages;

public class User : PageModel
{
    [BindProperty]
    public UserModel UserModel { get; set; }
    
    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        return RedirectToPage("/Test", UserModel);
    }
}