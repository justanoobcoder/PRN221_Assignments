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
using Microsoft.AspNetCore.SignalR;
using NguyenHongHiepSignalR.Hubs;
using System.Runtime.ConstrainedExecution;

namespace NguyenHongHiepSignalR.Pages.Admin.CarManagement;

public class EditModel : PageModel
{
    private readonly ICarRepository _carRepository;
    private readonly IHubContext<CarHub, ICarHub> _hubContext;

    public EditModel(ICarRepository carRepository, IHubContext<CarHub, ICarHub> hubContext)
    {
        _carRepository = carRepository;
        _hubContext = hubContext;
    }

    [BindProperty]
    public CarInformation CarInformation { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carinformation = await _carRepository.GetCarByIdAsync(id.Value);
        if (carinformation == null || carinformation.CarStatus == 0)
        {
            return NotFound();
        }
        CarInformation = carinformation;
        var manufacturers = (await _carRepository.GetManufacturersAsync()).ToList();
        var suppliers = (await _carRepository.GetSuppliersAsync()).ToList();
        ViewData["ManufacturerId"] = new SelectList(manufacturers, "ManufacturerId", "ManufacturerName");
        ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "SupplierName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var oldCar = await _carRepository.GetCarByIdAsync(CarInformation.CarId);
        if (oldCar == null || oldCar.CarStatus == 0)
        {
            return NotFound();
        }
        CarInformation.CarStatus = oldCar.CarStatus;

        await _carRepository.UpdateCarAsync(CarInformation);
        string manufacturer = (await _carRepository.GetManufacturersAsync())
            .Single(m => m.ManufacturerId == CarInformation.ManufacturerId).ManufacturerName;
        string supplier = (await _carRepository.GetSuppliersAsync())
            .Single(s => s.SupplierId == CarInformation.SupplierId).SupplierName;
        await _hubContext.Clients.All.ReceiveUpdatedCar(CarInformation, manufacturer, supplier);

        return RedirectToPage("./Index");
    }
}
