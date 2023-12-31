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

[BindProperties]
public class DetailsModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public DetailsModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

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
}
