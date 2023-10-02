using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages.Admin.CustomerManagement;

public class IndexModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public IndexModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IList<Customer> Customer { get;set; } = default!;

    public void OnGet(string? name)
    {
        if (string.IsNullOrEmpty(name))
            Customer = _customerRepository.GetAll().ToList();
        else
            Customer = _customerRepository.GetByName(name).ToList();
    }
}
