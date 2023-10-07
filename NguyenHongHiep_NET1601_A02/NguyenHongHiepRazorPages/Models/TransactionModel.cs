namespace NguyenHongHiepRazorPages.Models;

public class TransactionModel
{
    public int CustomerId { get; set; }
    public DateTime? RentingDate { get; set; }
    public List<RentingDetailModel> Details { get; set; } = new List<RentingDetailModel>();
}
