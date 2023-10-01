using System.ComponentModel.DataAnnotations;

namespace NguyenHongHiepRazorPages.Models;

public class RegisterCustomerModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Telephone is required")]
    public string Telephone { get; set; } = default!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = default!;

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Confirm Password does not match")]
    public string ConfirmPassword { get; set; } = default!;

    [Required(ErrorMessage = "Birthday is required")]
    public DateTime Birthday { get; set; } = default!;
}
