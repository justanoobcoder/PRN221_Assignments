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
    public IQueryable<CarInformation> GetAll() => CarDAO.Instance.GetAll();

    public IQueryable<CarInformation> GetByName(string name) => CarDAO.Instance.GetByName(name);

    public IQueryable<Manufacturer> GetAllManufactuers() => CarDAO.Instance.GetAllManufactuers();

    public IQueryable<Supplier> GetAllSuppliers() => CarDAO.Instance.GetAllSuppliers();

    public CarInformation? GetById(int id) => CarDAO.Instance.GetById(id);

    public CarInformation Insert(CarInformation car) => CarDAO.Instance.Insert(car);

    public void Update(CarInformation car) => CarDAO.Instance.Update(car);

    public void Delete(CarInformation car) => CarDAO.Instance.Delete(car);
}
