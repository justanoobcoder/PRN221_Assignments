﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages.Admin.CustomerManagement;

public class DeleteModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteModel(ICustomerRepository customerRepository)
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
        else 
        {
            Customer = customer;
        }
        return Page();
    }

    public IActionResult OnPost(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var customer = _customerRepository.GetById(id.Value);

        if (customer != null && customer.CustomerStatus != 0)
        {
            Customer = customer;
            _customerRepository.Delete(Customer);
        }
        else
        {
            return NotFound();
        }

        return RedirectToPage("./Index");
    }
}
