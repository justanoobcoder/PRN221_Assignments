using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenHongHiepSignalR.Constants;
using NguyenHongHiepSignalR.Models;
using NguyenHongHiepSignalR.Utils;
using Repositories;

namespace NguyenHongHiepSignalR.Pages.Admin.RentingManagement;

[BindProperties]
public class ChooseCustomerModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public ChooseCustomerModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IList<Customer> Customer { get; set; } = default!;

    public async Task OnGetAsync(string? name)
    {
        if (string.IsNullOrEmpty(name))
            Customer = (await _customerRepository.GetCustomersAsync())
                .Where(c => c.CustomerStatus != 0)
                .ToList();
        else
            Customer = (await _customerRepository.GetCustomersByName(name))
                .Where(c => c.CustomerStatus != 0)
                .ToList();
    }

    public IActionResult OnPost(int? id)
    {
        if (id == null)
            return BadRequest();

        TransactionModel transaction = new()
        {
            CustomerId = id.Value,
            RentingDate = DateTime.Now,
        };
        SessionHelper.SetObjectAsJson(HttpContext.Session, SessionKey.TransactionKey, transaction);

        return RedirectToPage("./ChooseCars");
    }
}
