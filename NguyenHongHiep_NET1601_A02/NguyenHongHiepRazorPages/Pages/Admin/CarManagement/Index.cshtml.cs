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

public class IndexModel : PageModel
{
    private readonly ICarRepository _carRepository;

    public IndexModel(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public IList<CarInformation> CarInformation { get;set; } = default!;

    public void OnGet(string? name)
    {
        if (string.IsNullOrEmpty(name))
            CarInformation = _carRepository.GetAll().ToList();
        else
            CarInformation = _carRepository.GetByName(name).ToList();
    }
}
