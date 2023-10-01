using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NguyenHongHiepRazorPages.Pages;

[BindProperties]
public class LoginModel : PageModel
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        return Page();
    }
}
