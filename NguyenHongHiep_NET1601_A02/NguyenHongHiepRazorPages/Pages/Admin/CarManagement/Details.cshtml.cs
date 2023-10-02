using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages.Admin.CarManagement;

public class DetailsModel : PageModel
{
    private readonly ICarRepository _carRepository;

    public DetailsModel(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public CarInformation CarInformation { get; set; } = default!; 

    public IActionResult OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carinformation = _carRepository.GetById(id.Value);
        if (carinformation == null || carinformation.CarStatus == 0)
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
