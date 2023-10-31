using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface ICarRepository
{
    Task<IQueryable<CarInformation>> GetCarsAsync();
    Task<CarInformation?> GetCarByIdAsync(int id);
    Task<IQueryable<CarInformation>> GetCarsByNameAsync(string name);
    Task<IQueryable<Manufacturer>> GetManufacturersAsync();
    Task<IQueryable<Supplier>> GetSuppliersAsync();
    Task<CarInformation> AddCarAsync(CarInformation CarInformation);
    Task UpdateCarAsync(CarInformation CarInformation);
    Task<bool> DeleteCarAsync(int id);
}
