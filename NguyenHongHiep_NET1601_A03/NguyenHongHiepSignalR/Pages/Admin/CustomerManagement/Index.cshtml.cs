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

public class IndexModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public IndexModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IList<Customer> Customer { get;set; } = default!;

    public async Task OnGetAsync(string? name)
    {
        if (string.IsNullOrEmpty(name))
            Customer = (await _customerRepository.GetCustomersAsync()).ToList();
        else
            Customer = (await _customerRepository.GetCustomersByName(name)).ToList();
    }
}
