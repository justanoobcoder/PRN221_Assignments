using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages.Admin.CarManagement;

public class EditModel : PageModel
{
    private readonly ICarRepository _carRepository;

    public EditModel(ICarRepository carRepository)
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
        CarInformation = carinformation;
        var manufacturers = _carRepository.GetAllManufactuers().ToList();
        var suppliers = _carRepository.GetAllSuppliers().ToList();
        ViewData["ManufacturerId"] = new SelectList(manufacturers, "ManufacturerId", "ManufacturerName");
        ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "SupplierName");
        return Page();
    }

    public IActionResult OnPost()
    {
        var oldCar = _carRepository.GetById(CarInformation.CarId);
        if (oldCar == null || oldCar.CarStatus == 0)
        {
            return NotFound();
        }
        CarInformation.CarStatus = oldCar.CarStatus;

        _carRepository.Update(CarInformation);

        return RedirectToPage("./Index");
    }
}
