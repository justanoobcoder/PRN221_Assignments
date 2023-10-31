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
    private static CarDAO? instance = null;
    private static readonly object padlock = new();
    private CarDAO() { }
    public static CarDAO Instance
    {
        get
        {
            lock (padlock)
            {
                instance ??= new();
                return instance;
            }
        }
    }

    public async Task<IQueryable<CarInformation>> GetCarsAsync()
    {
        var context = new FucarRentingManagementContext();
        var cars = await Task.Run(() => context.CarInformations
            .Include(c => c.Manufacturer)
            .Include(c => c.Supplier));
        return cars;
    }

    public async Task<CarInformation?> GetCarByIdAsync(int id)
    {
        var context = new FucarRentingManagementContext();
        var car = await context.CarInformations
            .Include(c => c.Manufacturer)
            .Include(c => c.Supplier)
            .Include(c => c.RentingDetails)
            .SingleOrDefaultAsync(c => c.CarId == id);
        return car;
    }

    public async Task<IQueryable<CarInformation>> GetCarsByNameAsync(string name)
    {
        var context = new FucarRentingManagementContext();
        var cars = await Task.Run(() => context.CarInformations
            .Include(c => c.Manufacturer)
            .Include(c => c.Supplier)
            .Where(c => c.CarName.ToLower().Contains(name.ToLower())));
        return cars;
    }

    public async Task<IQueryable<Manufacturer>> GetManufacturersAsync()
    {
        var context = new FucarRentingManagementContext();
        var manufacturers = await Task.Run(() => context.Manufacturers);
        return manufacturers;
    }

    public async Task<IQueryable<Supplier>> GetSuppliersAsync()
    {
        var context = new FucarRentingManagementContext();
        var suppliers = await Task.Run(() => context.Suppliers);
        return suppliers;
    }

    public async Task<CarInformation> AddCarAsync(CarInformation car)
    {
        var context = new FucarRentingManagementContext();
        car.CarStatus = 1;
        await context.CarInformations.AddAsync(car);
        await context.SaveChangesAsync();
        return car;
    }

    public async Task UpdateCarAsync(CarInformation car)
    {
        var context = new FucarRentingManagementContext();
        var c = await context.CarInformations
            .SingleOrDefaultAsync(c => c.CarId == car.CarId);
        context.Entry(c!).CurrentValues.SetValues(car);
        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteCarAsync(int id)
    {
        bool result = false;
        var context = new FucarRentingManagementContext();
        var c = await context.CarInformations
            .Include(c => c.RentingDetails)
            .SingleOrDefaultAsync(c => c.CarId == id);

        if (c!.RentingDetails.Count != 0)
        {
            c.CarStatus = 0;
        }
        else
        {
            context.Remove(c);
            result = true;
        }
        await context.SaveChangesAsync();
        return result;
    }
}
