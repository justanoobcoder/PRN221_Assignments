using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories;
using NguyenHongHiepRazorPages.Models;
using NguyenHongHiepRazorPages.Admin;

namespace NguyenHongHiepRazorPages.Pages.Admin.CustomerManagement;

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
    

    public IActionResult OnPost()
    {
        if (_customerRepository.ExistsByTelephone(Customer.Telephone))
        {
            ModelState.AddModelError("Customer.Telephone", "Phone number already exists");
        }
        if (Customer.Email == AdminAccount.Email || _customerRepository.ExistsByEmail(Customer.Email))
        {
            ModelState.AddModelError("Customer.Email", "Email already exists");
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
        _customerRepository.Insert(customer);

        return RedirectToPage("./Index");
    }
}
