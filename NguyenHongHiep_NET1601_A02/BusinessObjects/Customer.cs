using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public partial class Customer
{
    [DisplayName("ID")]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Customer name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Customer name must be between 3 and 50 characters")]
    [DisplayName("Name")]
    public string? CustomerName { get; set; }

    [Required(ErrorMessage = "Telephone is required")]
    [RegularExpression(@"^(\d{7,14})$", ErrorMessage = "Telephone must contain 7 to 14 digits")]
    [DisplayName("Telephone")]
    public string? Telephone { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email is not valid")]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Birthday is required")]
    [DataType(DataType.Date)]
    [DisplayName("Birthday")]
    public DateTime? CustomerBirthday { get; set; }

    [DisplayName("Status")]
    public byte? CustomerStatus { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 30 characters")]
    [DisplayName("Password")]
    public string? Password { get; set; }

    public virtual ICollection<RentingTransaction> RentingTransactions { get; set; } = new List<RentingTransaction>();
}
