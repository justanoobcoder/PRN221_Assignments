using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using NguyenHongHiepSignalR.Admin;
using NguyenHongHiepSignalR.Hubs;
using NguyenHongHiepSignalR.Models;
using NguyenHongHiepSignalR.Utils;
using Repositories;

namespace NguyenHongHiepSignalR.Pages;

[BindProperties]
public class RegisterModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IHubContext<CustomerHub, ICustomerHub> _hubContext;

    public RegisterModel(ICustomerRepository customerRepository, IHubContext<CustomerHub, ICustomerHub> hubContext)
    {
        _customerRepository = customerRepository;
        _hubContext = hubContext;
    }

    public RegisterCustomerModel Customer { get; set; } = default!;

    public IActionResult OnGet()
    {
        var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.SessionKey.CurrentUserKey);
        if (currentUser != null)
        {
            if (currentUser.IsAdmin)
                return RedirectToPage("/Admin/Index");
            else
                return RedirectToPage("/Member/Index");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (await _customerRepository.ExistsByPhoneAsync(Customer.Telephone))
        {
            ModelState.AddModelError("Customer.Telephone", "Phone number already exists");
        }
        if (Customer.Email == AdminAccount.Email || await _customerRepository.ExistsByEmailAsync(Customer.Email))
        {
            ModelState.AddModelError("Customer.Email", "Email already exists");
        }
        if (Customer.Birthday.AddYears(18) > DateTime.Now)
        {
            ModelState.AddModelError("Customer.Birthday", "Customer must be at least 18 years old");
        }

        if (ModelState.IsValid)
        {
            var customer = new BusinessObjects.Customer
            {
                CustomerName = Customer.Name.Trim(),
                Telephone = Customer.Telephone,
                Email = Customer.Email,
                Password = Customer.Password,
                CustomerBirthday = Customer.Birthday,
                CustomerStatus = 1,
            };
            try
            {
                await _customerRepository.AddCustomerAsync(customer);
                await _hubContext.Clients.All.RegisterCustomer(customer);
                TempData["Success"] = "Register successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Register failed! {ex.Message}";
            }
        }

        return Page();
    }
}
