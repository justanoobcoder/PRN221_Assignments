using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories;

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
    public Customer Customer { get; set; } = default!;
    

    public IActionResult OnPost()
    {
      if (!ModelState.IsValid || Customer == null)
        {
            return Page();
        }

        _customerRepository.Insert(Customer);

        return RedirectToPage("./Index");
    }
}
