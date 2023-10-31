using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenHongHiepSignalR.Constants;
using NguyenHongHiepSignalR.Models;
using NguyenHongHiepSignalR.Utils;
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

    public async Task<IActionResult> OnGetAsync()
    {
        var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(SessionKey.CurrentUserKey);
        if (currentUser == null)
        {
            return RedirectToPage("/Login");
        }
        RentingTransaction = (await _rentingRepository.GetTransactionsByCustomerEmailAsync(currentUser.Email)).ToList();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(SessionKey.CurrentUserKey);
        if (currentUser == null)
        {
            return RedirectToPage("/Login");
        }
        var customer = await _customerRepository.GetCustomerByEmailAsync(currentUser!.Email);
        if (customer == null)
        {
            return RedirectToPage("/Login");
        }
        TransactionModel transaction = new()
        {
            CustomerId = customer!.CustomerId,
            RentingDate = DateTime.Now,
        };
        SessionHelper.SetObjectAsJson(HttpContext.Session, SessionKey.TransactionKey, transaction);

        return RedirectToPage("./ChooseCars");
    }
}
