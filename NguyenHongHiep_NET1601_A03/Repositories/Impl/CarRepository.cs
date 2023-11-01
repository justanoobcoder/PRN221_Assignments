using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl;

public class CarRepository : ICarRepository
{
    public Task<IQueryable<CarInformation>> GetCarsAsync()
        => CarDAO.Instance.GetCarsAsync();

    public Task<CarInformation?> GetCarByIdAsync(int id)
        => CarDAO.Instance.GetCarByIdAsync(id);

    public Task<IQueryable<CarInformation>> GetCarsByNameAsync(string name)
        => CarDAO.Instance.GetCarsByNameAsync(name);

    public Task<IQueryable<Manufacturer>> GetManufacturersAsync()
        => CarDAO.Instance.GetManufacturersAsync();

    public Task<IQueryable<Supplier>> GetSuppliersAsync()
        => CarDAO.Instance.GetSuppliersAsync();

    public Task<CarInformation> AddCarAsync(CarInformation CarInformation)
        => CarDAO.Instance.AddCarAsync(CarInformation);

    public Task UpdateCarAsync(CarInformation CarInformation)
        => CarDAO.Instance.UpdateCarAsync(CarInformation);

    public Task<bool> DeleteCarAsync(int id)
        => CarDAO.Instance.DeleteCarAsync(id);
}
