using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenHongHiepRazorPages.Admin;
using NguyenHongHiepRazorPages.Models;
using NguyenHongHiepRazorPages.Utils;
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

    public IActionResult OnGet()
    {
        var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.Contants.CurrentUserKey);
        if (currentUser != null)
        {
            if (currentUser.IsAdmin)
                return RedirectToPage("/Admin/Index");
            else
                return RedirectToPage("/Customer/Index");
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        if (AdminAccount.IsAdmin(Email, Password))
        {
            HttpContext.Session.SetObjectAsJson(Constants.Contants.CurrentUserKey, new CurrentUser
            {
                Email = Email,
                IsAdmin = true,
            });
            return RedirectToPage("/Admin/Index");
        }
        else
        {
            var customer = _customerRepository.GetByEmailAndPassword(Email, Password);
            if (customer == null)
                ModelState.AddModelError(nameof(ErrorMessage), "Wrong email or password");
            else
            {
                HttpContext.Session.SetObjectAsJson(Constants.Contants.CurrentUserKey, new CurrentUser
                {
                    Email = Email,
                    IsAdmin = false,
                });
                return RedirectToPage("/Customer/Index");
            }
        }

        return Page();
    }
}
