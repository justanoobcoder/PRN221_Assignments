using System.ComponentModel.DataAnnotations;

namespace NguyenHongHiepSignalR.Models;

public class TransactionModel
{
    public int CustomerId { get; set; }

    [DataType(DataType.Date)]
    public DateTime? RentingDate { get; set; }

    public List<RentingDetailModel> Details { get; set; } = new List<RentingDetailModel>();
}
