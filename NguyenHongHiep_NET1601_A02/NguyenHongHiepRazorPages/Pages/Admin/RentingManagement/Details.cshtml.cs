using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages.Admin.RentingManagement;

public class DetailsModel : PageModel
{
    private readonly IRentingRepository _rentingRepository;

    public DetailsModel(IRentingRepository rentingRepository)
    {
        _rentingRepository = rentingRepository;
    }

    public RentingTransaction RentingTransaction { get; set; } = default!;
    public List<RentingDetail> Details { get; set; } = new();

    public IActionResult OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rentingtransaction = _rentingRepository.GetTransactionById(id.Value);
        if (rentingtransaction == null)
        {
            return NotFound();
        }
        else 
        {
            RentingTransaction = rentingtransaction;
            Details = _rentingRepository.GetRentingDetailsByTransactionId(id.Value).ToList();
        }
        return Page();
    }
}
