using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NguyenHongHiepRazorPages.Models;

public class RegisterCustomerModel
{
    [Required(ErrorMessage = "Customer name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Customer name must be between 3 and 50 characters")]
    [DisplayName("Name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Telephone is required")]
    [RegularExpression(@"^(\d{7,14})$", ErrorMessage = "Telephone must contain 7 to 14 digits")]
    [DisplayName("Telephone")]
    public string Telephone { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){1,3})+)$", ErrorMessage = "Email is not valid")]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Birthday is required")]
    [DataType(DataType.Date)]
    [DisplayName("Birthday")]
    public DateTime Birthday { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 30 characters")]
    [DisplayName("Password")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("Password", ErrorMessage = "Confirm password does not match")]
    [DisplayName("Confirm password")]
    public string ConfirmPassword { get; set; } = null!;
}
