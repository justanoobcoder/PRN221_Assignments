﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;
using NguyenHongHiepRazorPages.Admin;

namespace NguyenHongHiepRazorPages.Pages.Admin.CustomerManagement;

public class EditModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public EditModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [BindProperty]
    public Customer Customer { get; set; } = default!;

    public IActionResult OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = _customerRepository.GetById(id.Value);
        if (customer == null || customer.CustomerStatus == 0)
        {
            return NotFound();
        }
        Customer = customer;
        return Page();
    }

    public IActionResult OnPost()
    {
        var oldCustomer = _customerRepository.GetById(Customer.CustomerId);
        if (oldCustomer == null || oldCustomer.CustomerStatus == 0)
        {
            return NotFound();
        }

        var oldTelephone = oldCustomer.Telephone;
        var oldEmail = oldCustomer.Email;

        if (Customer.Telephone != oldTelephone && _customerRepository.ExistsByTelephone(Customer.Telephone!))
        {
            ModelState.AddModelError("Customer.Telephone", "Phone number already exists");
        }
        if (Customer.Email == AdminAccount.Email || 
            (Customer.Email != oldEmail && _customerRepository.ExistsByEmail(Customer.Email)))
        {
            ModelState.AddModelError("Customer.Email", "Email already exists");
        }
        if (Customer.CustomerBirthday > DateTime.Now.AddYears(-18))
        {
            ModelState.AddModelError("Customer.CustomerBirthday", "Customer must be at least 18 years old");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }
        Customer.CustomerStatus = oldCustomer.CustomerStatus;
        _customerRepository.Update(Customer);

        return RedirectToPage("./Index");
    }
}
