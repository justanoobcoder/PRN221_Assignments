using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace NguyenHongHiepSignalR.Pages.Admin.CustomerManagement;

[BindProperties]
public class DetailsModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public DetailsModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Customer Customer { get; set; } = default!; 

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _customerRepository.GetCustomerByIdAsync(id.Value);
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
