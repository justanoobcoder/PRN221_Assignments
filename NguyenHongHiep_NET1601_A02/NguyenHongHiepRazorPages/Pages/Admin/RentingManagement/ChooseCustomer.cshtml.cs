using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenHongHiepRazorPages.Models;
using NguyenHongHiepRazorPages.Utils;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages.Admin.RentingManagement;

[BindProperties]
public class ChooseCustomerModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public ChooseCustomerModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IList<Customer> Customer { get; set; } = default!;

    public void OnGet(string? name)
    {
        if (string.IsNullOrEmpty(name))
            Customer = _customerRepository.GetAll()
                .Where(c => c.CustomerStatus != 0)
                .ToList();
        else
            Customer = _customerRepository.GetByName(name)
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
        SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.Contants.TransactionKey, transaction);

        return RedirectToPage("./ChooseCars");
    }
}
