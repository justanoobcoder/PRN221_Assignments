using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface ICarInformationRepository
{
    CarInformation? GetCar(int id);
    IQueryable<CarInformation> GetAll();
    IQueryable<Manufacturer> GetAllManufacturers();
    IQueryable<Supplier> GetAllSuppliers();
    CarInformation Create(CarInformation car);
    void Update(CarInformation car);
    void Delete(CarInformation car);
}
