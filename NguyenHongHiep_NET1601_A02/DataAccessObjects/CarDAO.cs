using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;

public class CarDAO
{
    private static CarDAO instance = null;
    private static readonly object instanceLock = new object();

    private CarDAO() { }
    public static CarDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new CarDAO();
                }
                return instance;
            }
        }
    }

    public IQueryable<CarInformation> GetAll()
    {
        var context = new FucarRentingManagementContext();
        var cars = context.CarInformations
            .Include(c => c.Manufacturer)
            .Include(c => c.Supplier);
        return cars;
    }

    public IQueryable<CarInformation> GetByName(string name)
    {
        var context = new FucarRentingManagementContext();
        var cars = context.CarInformations
            .Include(c => c.Manufacturer)
            .Include(c => c.Supplier)
            .Where(c => c.CarName.ToLower().Contains(name.ToLower()));
        return cars;
    }

    public IQueryable<Manufacturer> GetAllManufactuers()
    {
        var context = new FucarRentingManagementContext();
        return context.Manufacturers;
    }

    public IQueryable<Supplier> GetAllSuppliers()
    {
        var context = new FucarRentingManagementContext();
        return context.Suppliers;
    }

    public CarInformation? GetById(int id)
    {
        var context = new FucarRentingManagementContext();
        var car = context.CarInformations
            .Include(c => c.Manufacturer)
            .Include(c => c.Supplier)
            .Include(c => c.RentingDetails)
            .SingleOrDefault(c => c.CarId == id);
        return car;
    }

    public CarInformation Insert(CarInformation car)
    {
        var context = new FucarRentingManagementContext();
        car.CarStatus = 1;
        context.CarInformations.Add(car);
        context.SaveChanges();
        return car;
    }

    public void Update(CarInformation car)
    {
        var context = new FucarRentingManagementContext();
        CarInformation? c = context.CarInformations
            .SingleOrDefault(c => c.CarId == car.CarId);
        if (c == null)
            throw new Exception("Car not found");

        context.Entry(c).CurrentValues.SetValues(car);
        context.SaveChanges();
    }

    public void Delete(CarInformation car)
    {
        var context = new FucarRentingManagementContext();
        CarInformation? c = context.CarInformations
            .Include(c => c.RentingDetails)
            .SingleOrDefault(c => c.CarId == car.CarId);

        if (c == null)
            throw new Exception("Car not found");

        if (c.RentingDetails.Count != 0)
        {
            car.CarStatus = 0;
            context.Entry(c) .CurrentValues.SetValues(car);
        }
        else
        {
            context.Remove(c);
        }
        context.SaveChanges();
    }
}
