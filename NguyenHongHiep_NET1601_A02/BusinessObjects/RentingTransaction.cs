using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public partial class RentingTransaction
{
    [DisplayName("ID")]
    public int RentingTransationId { get; set; }

    [DisplayName("Rent Date")]
    [DataType(DataType.Date)]
    public DateTime? RentingDate { get; set; }

    [DisplayName("Total Price")]
    public decimal? TotalPrice { get; set; }

    public int CustomerId { get; set; }

    [DisplayName("Status")]
    public byte? RentingStatus { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<RentingDetail> RentingDetails { get; set; } = new List<RentingDetail>();
}
