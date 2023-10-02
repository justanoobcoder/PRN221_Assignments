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

public class DeleteModel : PageModel
{
    private readonly ICarRepository _carRepository;

    public DeleteModel(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    [BindProperty]
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

    public IActionResult OnPost(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var carinformation = _carRepository.GetById(id.Value);

        if (carinformation != null && carinformation.CarStatus != 0)
        {
            CarInformation = carinformation;
            _carRepository.Delete(CarInformation);
        }
        else return NotFound();

        return RedirectToPage("./Index");
    }
}
