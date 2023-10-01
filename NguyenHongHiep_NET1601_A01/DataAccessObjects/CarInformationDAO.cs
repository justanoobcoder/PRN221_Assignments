using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;

public sealed class CarInformationDAO
{
    private static CarInformationDAO instance = null;
    private static readonly object locker = new object();

    private CarInformationDAO() {}
    public static CarInformationDAO Instance
    {
        get
        {
            lock (locker)
            {
                if (instance == null)
                {
                    instance = new();
                }
                return instance;
            }
        }
    }

    public CarInformation? GetCar(int id)
    {
        try
        {
            using (var context = new FUCarRentingManagementContext())
            {
                return context.CarInformations
                    .Where(c => c.CarId == id)
                    .Include(c => c.Manufacturer)
                    .Include(c => c.Supplier)
                    .Include(c => c.RentingDetails)
                    .SingleOrDefault();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public IQueryable<CarInformation> GetAll()
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            return context.CarInformations
                .Include(c => c.Manufacturer)
                .Include(c => c.Supplier);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public IQueryable<Supplier> GetAllSuppliers()
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            return context.Suppliers;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public IQueryable<Manufacturer> GetAllManufacturers()
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            return context.Manufacturers;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public CarInformation Create(CarInformation car)
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            car.CarStatus = 1;
            context.CarInformations.Add(car);
            context.SaveChanges();
            return car;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Update(CarInformation car)
    {
        if (car == null)
            throw new Exception("Car is required");
        try
        {
            CarInformation? c = GetCar(car.CarId);
            if (c == null)
                throw new Exception("Car not found");

            var context = new FUCarRentingManagementContext();
            context.Entry(c).State = EntityState.Detached;
            context.Update(car);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public bool IsCarNotBeingRented(CarInformation car)
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            var rentingDetails = context.RentingDetails
                .Where(rd => rd.CarId == car.CarId)
                .Include(rd => rd.RentingTransaction)
                .ToList();
            return rentingDetails.All(rd => rd.RentingTransaction.RentingStatus == 0);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Delete(CarInformation car)
    {
        if (car == null)
            throw new Exception("Car is required");
        try
        {
            CarInformation? c = GetCar(car.CarId);
            if (c == null)
                throw new Exception("Car not found");

            var context = new FUCarRentingManagementContext();
            if (c.RentingDetails.Count != 0)
            {
                context.Entry(c).State = EntityState.Detached;
                car.CarStatus = 0;
                context.Update(car);
            }
            else
            {
                context.Entry(c).State = EntityState.Detached;
                context.Remove(car);
            }
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
