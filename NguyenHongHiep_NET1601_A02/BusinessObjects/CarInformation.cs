using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public partial class CarInformation
{
    [DisplayName("ID")]
    public int CarId { get; set; }

    [DisplayName("Name")]
    [Required(ErrorMessage = "Car name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Car name must be from 3 to 50 characters")]
    public string CarName { get; set; } = null!;

    [DisplayName("Description")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "Car description must be from 3 to 500 characters")]
    public string? CarDescription { get; set; }

    [DisplayName("Number Of Doors")]
    [Required(ErrorMessage = "Number of doors is required")]
    [Range(2, 6, ErrorMessage = "Number of doors must be from 2 to 6")]
    public int? NumberOfDoors { get; set; }

    [DisplayName("Seating Capacity")]
    [Required(ErrorMessage = "Seating capacity is required")]
    [Range(2, 8, ErrorMessage = "Seating capacity must be from 2 to 8")]
    public int? SeatingCapacity { get; set; }

    [DisplayName("Fuel Type")]
    [Required(ErrorMessage = "Fuel type is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Fuel type must be from 3 to 50 characters")]
    public string? FuelType { get; set; }

    [DisplayName("Year")]
    [Required(ErrorMessage = "Year is required")]
    [Range(1900, 2023, ErrorMessage = "Year must be from 1900 to 2023")]
    public int? Year { get; set; }

    public int ManufacturerId { get; set; }

    public int SupplierId { get; set; }

    [DisplayName("Status")]
    public byte? CarStatus { get; set; }

    [DisplayName("Price Per Day")]
    [Required(ErrorMessage = "Price per day is required")]
    [Range(0.0001, 100000000, ErrorMessage = "Price per day must be greater than 0")]
    public decimal? CarRentingPricePerDay { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<RentingDetail> RentingDetails { get; set; } = new List<RentingDetail>();

    public virtual Supplier Supplier { get; set; } = null!;
}
