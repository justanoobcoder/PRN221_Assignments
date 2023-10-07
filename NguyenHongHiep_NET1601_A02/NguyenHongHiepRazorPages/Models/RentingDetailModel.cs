namespace NguyenHongHiepRazorPages.Models;

public class RentingDetailModel
{
    public int CarId { get; set; }
    public string CarName { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? Price { get; set; }
}
