using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NguyenHongHiepRazorPages.Models;
using NguyenHongHiepRazorPages.Utils;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages.Admin.RentingManagement;

[BindProperties]
public class ChooseCarsModel : PageModel
{
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;

    public ChooseCarsModel(ICarRepository carRepository, ICustomerRepository customerRepository)
    {
        _carRepository = carRepository;
        _customerRepository = customerRepository;
    }

    public Customer Customer { get; set; } = default!;
    public int CarId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public List<RentingDetailModel> RentingCars { get; set; } = new();

    public IActionResult OnGet()
    {
        TransactionModel? transaction = SessionHelper.GetObjectFromJson<TransactionModel>(HttpContext.Session, Constants.Contants.TransactionKey);
        if (transaction == null)
            return BadRequest();
        var c = _customerRepository.GetById(transaction.CustomerId);
        if (c == null || c.CustomerStatus == 0)
            return NotFound();
        Customer = c;

        var carIds = transaction.Details.Select(d => d.CarId);
        var cars = _carRepository.GetAll()
                .Where(c => c.CarStatus != 0 && !carIds.Contains(c.CarId))
                .ToList();
        ViewData["CarId"] = new SelectList(cars, "CarId", "CarName");
        RentingCars = transaction.Details;

        return Page();
    }

    public IActionResult OnPost()
    {
        TransactionModel? transaction = SessionHelper.GetObjectFromJson<TransactionModel>(HttpContext.Session, Constants.Contants.TransactionKey);
        if (transaction == null)
            return BadRequest();
        var car = _carRepository.GetById(CarId);
        if (car == null || car.CarStatus == 0)
            return NotFound();
        if (StartDate > EndDate)
            return BadRequest();
        TimeSpan difference = EndDate - StartDate;
        int numberOfDays = difference.Days + 1;
        transaction.Details.Add(new RentingDetailModel
        {
            CarId = car.CarId,
            CarName = car.CarName,
            StartDate = StartDate,
            EndDate = EndDate,
            Price = car.CarRentingPricePerDay * numberOfDays,
        });
        SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.Contants.TransactionKey, transaction);

        return RedirectToPage("./ChooseCars");
    }

    public IActionResult OnPostDelete(int? id)
    {
        if (id == null)
            return BadRequest();
        TransactionModel? transaction = SessionHelper.GetObjectFromJson<TransactionModel>(HttpContext.Session, Constants.Contants.TransactionKey);
        if (transaction == null)
            return BadRequest();
        var detail = transaction.Details.SingleOrDefault(d => d.CarId == id);
        if (detail == null)
            return NotFound();
        transaction.Details.Remove(detail);
        SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.Contants.TransactionKey, transaction);
        return RedirectToPage("./ChooseCars");
    }
}
