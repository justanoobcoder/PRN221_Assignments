﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NguyenHongHiepSignalR.Models;

public class RentingDetailModel
{
    public int Index { get; set; }

    [DisplayName("Car ID")]
    public int CarId { get; set; }

    [DisplayName("Car Name")]
    public string CarName { get; set; } = default!;

    [DisplayName("Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [DisplayName("End Date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [DisplayName("Total Price")]
    [DataType(DataType.Currency)]
    public decimal? Price { get; set; }
}
