using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages.Admin.CarManagement;

public class CreateModel : PageModel
{
    private readonly ICarRepository _carRepository;

    public CreateModel(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public IActionResult OnGet()
    {
        var manufacturers = _carRepository.GetAllManufactuers().ToList();
        var suppliers = _carRepository.GetAllSuppliers().ToList();
        ViewData["ManufacturerId"] = new SelectList(manufacturers, "ManufacturerId", "ManufacturerName");
        ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "SupplierName");
        return Page();
    }

    [BindProperty]
    public CarInformation CarInformation { get; set; } = default!;
    

    public IActionResult OnPost()
    {
        _carRepository.Insert(CarInformation);

        return RedirectToPage("./Index");
    }
}
