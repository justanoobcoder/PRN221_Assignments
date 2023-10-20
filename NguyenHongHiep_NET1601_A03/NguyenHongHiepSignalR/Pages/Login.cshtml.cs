using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using NguyenHongHiepSignalR.Admin;
using NguyenHongHiepSignalR.Models;
using NguyenHongHiepSignalR.Utils;
using Repositories;

namespace NguyenHongHiepSignalR.Pages;

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
        var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.SessionKey.CurrentUserKey);
        if (currentUser != null)
        {
            if (currentUser.IsAdmin)
                return RedirectToPage("/Admin/Index");
            else
                return RedirectToPage("/Member/Index");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (AdminAccount.IsAdmin(Email, Password))
        {
            HttpContext.Session.SetObjectAsJson(Constants.SessionKey.CurrentUserKey, new CurrentUser
            {
                Email = Email,
                IsAdmin = true,
            });
            return RedirectToPage("/Admin/Index");
        }
        else
        {
            var customer = await _customerRepository.GetCustomerByEmailAndPasswordAsync(Email, Password);
            if (customer == null)
                ModelState.AddModelError(nameof(ErrorMessage), "Wrong email or password");
            else if (customer.CustomerStatus == 0)
                ModelState.AddModelError(nameof(ErrorMessage), "This account is deleted");
            else
            {
                HttpContext.Session.SetObjectAsJson(Constants.SessionKey.CurrentUserKey, new CurrentUser
                {
                    Email = Email,
                    IsAdmin = false,
                });
                return RedirectToPage("/Member/Index");
            }
        }

        return Page();
    }

    public IActionResult OnPostLogout()
    {
        HttpContext.Session.Remove(Constants.SessionKey.CurrentUserKey);
        return RedirectToPage("/Login");
    }
}
