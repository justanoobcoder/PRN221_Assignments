using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.ComponentModel.DataAnnotations;

namespace NguyenHongHiepRazorPages.Pages.Admin.ReportStatistic;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly IRentingRepository _rentingRepository;

    public IndexModel(IRentingRepository rentingRepository)
    {
        _rentingRepository = rentingRepository;
    }

    public List<RentingTransaction> Rentings = new();
    public int TotalRenting { get; set; } = 0;
    public decimal TotalPrice { get; set; } = 0;
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;
    public string ErrorMessage { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(DateTime? start, DateTime? end)
    {
        if (start == null && end == null)
        {
            StartDate = DateTime.Now.AddMonths(-6);
            EndDate = DateTime.Now;
        }
        else if (start != null && end != null)
        {
            StartDate = start;
            EndDate = end;
        }
        else
        {
            return BadRequest();
        }

        if (StartDate > DateTime.Now || EndDate > DateTime.Now)
        {
            ModelState.AddModelError(nameof(ErrorMessage), "Date must not be after today");
            return Page();
        }
        if (StartDate > EndDate)
        {
            ModelState.AddModelError(nameof(ErrorMessage), "Start date must not be after end date");
            return Page();
        }

        Rentings = (await _rentingRepository.GetAllTransactionsAsync())
            .Where(r => r.RentingDate >= StartDate && r.RentingDate <= EndDate)
            .OrderByDescending(r => r.TotalPrice)
            .ToList();
        TotalRenting = Rentings.Count;
        TotalPrice = Rentings.Sum(r => r.TotalPrice ?? 0);
        return Page();
    }
}
