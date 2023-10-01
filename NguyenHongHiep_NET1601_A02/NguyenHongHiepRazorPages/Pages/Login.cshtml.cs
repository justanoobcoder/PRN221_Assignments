using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenHongHiepRazorPages.Admin;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages;

[BindProperties]
public class LoginModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public LoginModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? ErrorMessage { get; set; } = default!;

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (AdminAccount.IsAdmin(Email, Password))
            return RedirectToPage("/Admin/Index");
        else
        {
            var customer = _customerRepository.GetByEmailAndPassword(Email, Password);
            if (customer == null)
                ModelState.AddModelError(nameof(ErrorMessage), "Wrong email or password");
            else
                return RedirectToPage("Customer/Index");
        }

        return Page();
    }
}
