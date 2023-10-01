using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations;

public class CarInformationRepository : ICarInformationRepository
{
    public CarInformation Create(CarInformation car) => CarInformationDAO.Instance.Create(car);

    public void Delete(CarInformation car) => CarInformationDAO.Instance.Delete(car);

    public IQueryable<CarInformation> GetAll() => CarInformationDAO.Instance.GetAll();

    public CarInformation? GetCar(int id) => CarInformationDAO.Instance.GetCar(id);

    public IQueryable<Manufacturer> GetAllManufacturers() => CarInformationDAO.Instance.GetAllManufacturers();

    public IQueryable<Supplier> GetAllSuppliers() => CarInformationDAO.Instance.GetAllSuppliers();

    public void Update(CarInformation car) => CarInformationDAO.Instance.Update(car);
}
