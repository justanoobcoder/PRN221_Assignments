using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface ICarRepository
{
    IQueryable<CarInformation> GetAll();
    IQueryable<CarInformation> GetByName(string name);
    IQueryable<Manufacturer> GetAllManufactuers();
    IQueryable<Supplier> GetAllSuppliers();
    CarInformation? GetById(int id);
    CarInformation Insert(CarInformation car);
    void Update(CarInformation car);
    void Delete(CarInformation car);
}
