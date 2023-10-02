using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

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
        if (customer == null)
        {
            return NotFound();
        }
        Customer = customer;
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            _customerRepository.Update(Customer);
        }
        catch (Exception)
        {
            throw;
        }

        return RedirectToPage("./Index");
    }
}
