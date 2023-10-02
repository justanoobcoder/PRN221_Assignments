using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenHongHiepRazorPages.Admin;
using NguyenHongHiepRazorPages.Models;
using NguyenHongHiepRazorPages.Utils;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages;

[BindProperties]
public class RegisterModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public RegisterModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public RegisterCustomerModel Customer { get; set; } = default!;

    public IActionResult OnGet()
    {
        var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.Contants.CurrentUserKey);
        if (currentUser != null)
        {
            if (currentUser.IsAdmin)
                return RedirectToPage("/Admin/Index");
            else
                return RedirectToPage("/Member/Index");
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        if (_customerRepository.ExistsByTelephone(Customer.Telephone))
        {
            ModelState.AddModelError("Customer.Telephone", "Phone number already exists");
        }
        if (Customer.Email == AdminAccount.Email || _customerRepository.ExistsByEmail(Customer.Email))
        {
            ModelState.AddModelError("Customer.Email", "Email already exists");
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
                _customerRepository.Insert(customer);
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
