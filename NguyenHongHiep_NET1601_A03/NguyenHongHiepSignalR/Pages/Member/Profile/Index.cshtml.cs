using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenHongHiepSignalR.Constants;
using NguyenHongHiepSignalR.Models;
using NguyenHongHiepSignalR.Utils;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages.Member.Profile;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public IndexModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Customer Customer { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        var loginUser = SessionHelper.GetObjectFromJson<CurrentUser>(HttpContext.Session, SessionKey.CurrentUserKey);
        if (loginUser != null)
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(loginUser.Email);
            if (customer != null)
            {
                Customer = customer;
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }
        else
        {
            return RedirectToPage("/Login");
        }
        return Page();
    }
}
