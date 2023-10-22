using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenHongHiepRazorPages.Models;
using NguyenHongHiepRazorPages.Utils;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages.Member.Renting;

public class IndexModel : PageModel
{
    private readonly IRentingRepository _rentingRepository;
    private readonly ICustomerRepository _customerRepository;

    public IndexModel(IRentingRepository rentingRepository, ICustomerRepository customerRepository)
    {
        _rentingRepository = rentingRepository;
        _customerRepository = customerRepository;
    }

    public IList<RentingTransaction> RentingTransaction { get; set; } = default!;

    public IActionResult OnGet()
    {
        var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.Contants.CurrentUserKey);
        if (currentUser == null)
        {
            return RedirectToPage("/Login");
        }
        RentingTransaction = _rentingRepository.GetTransactionsByCustomerEmail(currentUser.Email).ToList();
        return Page();
    }

    public IActionResult OnPost()
    {
        var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.Contants.CurrentUserKey);
        if (currentUser == null)
        {
            return RedirectToPage("/Login");
        }
        var customer = _customerRepository.GetByEmail(currentUser!.Email);
        if (customer == null)
        {
            return RedirectToPage("/Login");
        }
        TransactionModel transaction = new()
        {
            CustomerId = customer!.CustomerId,
            RentingDate = DateTime.Now,
        };
        SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.Contants.TransactionKey, transaction);

        return RedirectToPage("./ChooseCars");
    }
}
