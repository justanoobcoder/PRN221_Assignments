using System.ComponentModel.DataAnnotations;

namespace NguyenHongHiepRazorPages.Models;

public class RentingDetailModel
{
    public int CarId { get; set; }

    public string CarName { get; set; } = default!;
    
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    
    public decimal? Price { get; set; }
}
