using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NguyenHongHiepWPF.Models;

public class OrderDetail
{
    public int CarId { get; set; }
    public string CarName { get; set; } = default!;
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? Price { get; set; }
}
