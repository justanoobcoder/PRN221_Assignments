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

public class DeleteModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var customer = await _customerRepository.GetCustomerByIdAsync(id.Value);

        if (customer != null && customer.CustomerStatus != 0)
        {
            Customer = customer;
            await _customerRepository.DeleteCustomerAsync(Customer.CustomerId);
        }
        else
        {
            return NotFound();
        }

        return RedirectToPage("./Index");
    }
}
