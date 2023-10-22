using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenHongHiepRazorPages.Models;
using NguyenHongHiepRazorPages.Utils;
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

    public IActionResult OnGet()
    {
        var loginUser = SessionHelper.GetObjectFromJson<CurrentUser>(HttpContext.Session, Constants.Contants.CurrentUserKey);
        if (loginUser != null)
        {
            var customer = _customerRepository.GetByEmail(loginUser.Email);
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
