using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace NguyenHongHiepSignalR.Pages.Admin.CarManagement;

public class IndexModel : PageModel
{
    private readonly ICarRepository _carRepository;

    public IndexModel(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public IList<CarInformation> CarInformation { get;set; } = default!;

    public async Task OnGetAsync(string? name)
    {
        if (string.IsNullOrEmpty(name))
            CarInformation = (await _carRepository.GetCarsAsync()).ToList();
        else
            CarInformation = (await _carRepository.GetCarsByNameAsync(name)).ToList();
    }
}
