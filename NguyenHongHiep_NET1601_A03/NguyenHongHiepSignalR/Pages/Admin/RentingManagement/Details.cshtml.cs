using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace NguyenHongHiepSignalR.Pages.Admin.RentingManagement;

public class DetailsModel : PageModel
{
    private readonly IRentingRepository _rentingRepository;

    public DetailsModel(IRentingRepository rentingRepository)
    {
        _rentingRepository = rentingRepository;
    }

    public RentingTransaction RentingTransaction { get; set; } = default!;
    public List<RentingDetail> Details { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rentingtransaction = await _rentingRepository.GetTransactionByIdAsync(id.Value);
        if (rentingtransaction == null)
        {
            return NotFound();
        }
        else 
        {
            RentingTransaction = rentingtransaction;
            Details = (await _rentingRepository.GetRentingDetailsByTransactionIdAsync(id.Value)).ToList();
        }
        return Page();
    }
}
