using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;

namespace NguyenHongHiepRazorPages.Pages.Admin.CarManagement
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObjects.FucarRentingManagementContext _context;

        public DetailsModel(BusinessObjects.FucarRentingManagementContext context)
        {
            _context = context;
        }

      public CarInformation CarInformation { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CarInformations == null)
            {
                return NotFound();
            }

            var carinformation = await _context.CarInformations.FirstOrDefaultAsync(m => m.CarId == id);
            if (carinformation == null)
            {
                return NotFound();
            }
            else 
            {
                CarInformation = carinformation;
            }
            return Page();
        }
    }
}
