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

public class IndexModel : PageModel
{
    private readonly IRentingRepository _rentingRepository;

    public IndexModel(IRentingRepository rentingRepository)
    {
        _rentingRepository = rentingRepository;
    }

    public IList<RentingTransaction> RentingTransaction { get;set; } = default!;

    public async Task OnGetAsyn()
    {
        RentingTransaction = (await _rentingRepository.GetAllTransactionsAsync()).ToList();
    }

    public async Task<IActionResult> OnPostDone(int id)
    {
        var rentingTransaction = await _rentingRepository.GetTransactionByIdAsync(id);
        if (rentingTransaction == null)
        {
            return NotFound();
        }
        await _rentingRepository.UpdateAsync(id, 1);
        return RedirectToPage("./Index");
    }
}
