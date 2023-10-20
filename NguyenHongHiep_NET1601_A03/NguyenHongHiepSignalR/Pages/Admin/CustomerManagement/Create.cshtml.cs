using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories;
using NguyenHongHiepSignalR.Models;
using NguyenHongHiepSignalR.Admin;

namespace NguyenHongHiepSignalR.Pages.Admin.CustomerManagement;

public class CreateModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public CreateModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public RegisterCustomerModel Customer { get; set; } = default!;
    

    public async Task<IActionResult> OnPostAsync()
    {
        if (await _customerRepository.ExistsByPhoneAsync(Customer.Telephone))
        {
            ModelState.AddModelError("Customer.Telephone", "Phone number already exists");
        }
        if (Customer.Email == AdminAccount.Email || await _customerRepository.ExistsByEmailAsync(Customer.Email))
        {
            ModelState.AddModelError("Customer.Email", "Email already exists");
        }
        if (Customer.Birthday.AddYears(18) > DateTime.Now)
        {
            ModelState.AddModelError("Customer.Birthday", "Customer must be at least 18 years old");
        }

        if (!ModelState.IsValid || Customer == null)
        {
            return Page();
        }
        var customer = new Customer
        {
            CustomerName = Customer.Name.Trim(),
            Telephone = Customer.Telephone,
            Email = Customer.Email,
            Password = Customer.Password,
            CustomerBirthday = Customer.Birthday,
            CustomerStatus = 1,
        };
        await _customerRepository.AddCustomerAsync(customer);

        return RedirectToPage("./Index");
    }
}
