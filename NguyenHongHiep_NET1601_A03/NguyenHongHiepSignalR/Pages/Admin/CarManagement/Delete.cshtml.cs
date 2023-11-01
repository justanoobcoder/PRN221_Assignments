using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;
using Microsoft.AspNetCore.SignalR;
using NguyenHongHiepSignalR.Hubs;

namespace NguyenHongHiepSignalR.Pages.Admin.CarManagement;

public class DeleteModel : PageModel
{
    private readonly ICarRepository _carRepository;
    private readonly IHubContext<CarHub, ICarHub> _hubContext;

    public DeleteModel(ICarRepository carRepository, IHubContext<CarHub, ICarHub> hubContext)
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
        else 
        {
            CarInformation = carinformation;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var carinformation = await _carRepository.GetCarByIdAsync(id.Value);

        if (carinformation != null && carinformation.CarStatus != 0)
        {
            CarInformation = carinformation;
            bool result = await _carRepository.DeleteCarAsync(id.Value);
            await _hubContext.Clients.All.ReceiveDeletedCar(id.Value, result);
        }
        else return NotFound();

        return RedirectToPage("./Index");
    }
}
