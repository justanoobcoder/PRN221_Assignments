using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NguyenHongHiepRazorPages.Models;
using NguyenHongHiepRazorPages.Utils;
using Repositories;

namespace NguyenHongHiepRazorPages.Pages.Member.Renting;

[BindProperties]
public class ChooseCarsModel : PageModel
{
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IRentingRepository _rentingRepository;

    public ChooseCarsModel(ICarRepository carRepository, ICustomerRepository customerRepository, IRentingRepository rentingRepository)
    {
        _carRepository = carRepository;
        _customerRepository = customerRepository;
        _rentingRepository = rentingRepository;
    }

    public Customer Customer { get; set; } = default!;
    public int CarId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public List<RentingDetailModel> RentingCars { get; set; } = new();
    public string? Error { get; set; } = default!;

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
        if (!_rentingRepository.CanCarBeRented(car.CarId, StartDate, EndDate))
        {
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
            ModelState.AddModelError(nameof(Error), "This car is not available in this period");
            return Page();
        }
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

    public IActionResult OnPostRent()
    {
        TransactionModel? transaction = SessionHelper.GetObjectFromJson<TransactionModel>(HttpContext.Session, Constants.Contants.TransactionKey);
        if (transaction == null)
            return BadRequest();
        if (transaction.Details.Count == 0)
            return BadRequest();
        RentingTransaction rentingTransaction = new()
        {
            CustomerId = transaction.CustomerId,
            RentingDate = DateTime.Now,
            TotalPrice = transaction.Details.Sum(d => d.Price),
            RentingStatus = 0,
        };
        foreach (var detail in transaction.Details)
        {
            var car = _carRepository.GetById(detail.CarId);
            if (car == null || car.CarStatus == 0)
                return NotFound();
            rentingTransaction.RentingDetails.Add(new RentingDetail
            {
                CarId = detail.CarId,
                StartDate = detail.StartDate,
                EndDate = detail.EndDate,
                Price = car.CarRentingPricePerDay,
            });
        }
        _rentingRepository.CreateTransaction(rentingTransaction);
        HttpContext.Session.Remove(Constants.Contants.TransactionKey);
        return RedirectToPage("./Index");
    }
}
