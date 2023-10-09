using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public partial class RentingDetail
{
    public int RentingTransactionId { get; set; }

    [DisplayName("Car ID")]
    public int CarId { get; set; }

    [DisplayName("Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [DisplayName("End Date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [DisplayName("Price")]
    [DataType(DataType.Currency)]
    public decimal? Price { get; set; }

    public virtual CarInformation Car { get; set; } = null!;

    public virtual RentingTransaction RentingTransaction { get; set; } = null!;
}
