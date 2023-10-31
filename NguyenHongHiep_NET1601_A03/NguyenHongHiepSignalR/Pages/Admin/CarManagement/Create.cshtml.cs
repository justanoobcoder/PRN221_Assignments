using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories;
using Microsoft.AspNetCore.SignalR;
using NguyenHongHiepSignalR.Hubs;

namespace NguyenHongHiepSignalR.Pages.Admin.CarManagement;

public class CreateModel : PageModel
{
    private readonly ICarRepository _carRepository;
    private readonly IHubContext<CarHub, ICarHub> _hubContext;

    public CreateModel(ICarRepository carRepository, IHubContext<CarHub, ICarHub> hubContext)
    {
        _carRepository = carRepository;
        _hubContext = hubContext;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var manufacturers = (await _carRepository.GetManufacturersAsync()).ToList();
        var suppliers = (await _carRepository.GetSuppliersAsync()).ToList();
        ViewData["ManufacturerId"] = new SelectList(manufacturers, "ManufacturerId", "ManufacturerName");
        ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "SupplierName");
        return Page();
    }

    [BindProperty]
    public CarInformation CarInformation { get; set; } = default!;
    

    public async Task<IActionResult> OnPostAsync()
    {
        var car = await _carRepository.AddCarAsync(CarInformation);
        string manufacturer = (await _carRepository.GetManufacturersAsync())
            .Single(m => m.ManufacturerId == car.ManufacturerId).ManufacturerName;
        string supplier = (await _carRepository.GetSuppliersAsync())
            .Single(s => s.SupplierId == car.SupplierId).SupplierName;
        await _hubContext.Clients.All.ReceiveCreatedCar(CarInformation, manufacturer, supplier);

        return RedirectToPage("./Index");
    }
}
