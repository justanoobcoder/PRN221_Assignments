using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenHongHiepRazorPages.Models;

namespace NguyenHongHiepRazorPages.Pages;

[BindProperties]
public class RegisterModel : PageModel
{
    public RegisterCustomerModel Customer { get; set; } = default!;

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        return Page();
    }
}
