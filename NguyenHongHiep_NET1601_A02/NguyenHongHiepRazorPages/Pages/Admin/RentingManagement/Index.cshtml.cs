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

    public void OnGet()
    {
        RentingTransaction = _rentingRepository.GetAllTransactions().ToList();
    }

    public IActionResult OnPostDone(int id)
    {
        var rentingTransaction = _rentingRepository.GetTransactionById(id);
        if (rentingTransaction == null)
        {
            return NotFound();
        }
        _rentingRepository.Update(id, 1);
        return RedirectToPage("./Index");
    }
}
