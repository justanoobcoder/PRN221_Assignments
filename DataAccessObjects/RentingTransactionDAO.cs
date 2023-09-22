using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;

public class RentingTransactionDAO
{
    private static RentingTransactionDAO instance = null;
    private static readonly object locker = new object();

    private RentingTransactionDAO() { }
    public static RentingTransactionDAO Instance
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

    public RentingTransaction? GetById(int id)
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            return context.RentingTransactions
                .Where(rt => rt.RentingTransationId == id)
                .Include(rt => rt.Customer)
                .Include(rt => rt.RentingDetails)
                .SingleOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public IQueryable<RentingTransaction> GetAll()
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            return context.RentingTransactions
                .Include(rt => rt.Customer)
                .Include(rt => rt.RentingDetails)
                .ThenInclude(rd => rd.Car);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public IQueryable<RentingTransaction> GetAllBetween(DateTime startDate, DateTime endDate)
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            return context.RentingTransactions
                .Where(rt => rt.RentingDate >= startDate && rt.RentingDate <= endDate)
                .Include(rt => rt.Customer)
                .Include(rt => rt.RentingDetails)
                .ThenInclude(rd => rd.Car);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public bool CanRentCar(int carId, DateTime startDate, DateTime endDate)
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            List<RentingDetail> rentingDetails = context.RentingDetails
                .Where(rd => rd.CarId == carId)
                .Include(rd => rd.RentingTransaction)
                .ToList();
            return !(rentingDetails.Any(rd => startDate >= rd.StartDate && startDate <= rd.EndDate)
                || rentingDetails.Any(rd => endDate >= rd.StartDate && endDate <= rd.EndDate)
                || rentingDetails.Any(rd => startDate <= rd.StartDate && endDate >= rd.EndDate));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public RentingDetail CreateRentingDetail(RentingDetail rentingDetail)
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            context.RentingDetails.Add(rentingDetail);
            context.SaveChanges();
            return rentingDetail;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public RentingTransaction CreateTransaction(RentingTransaction rentingTransaction)
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            context.RentingTransactions.Add(rentingTransaction);
            context.SaveChanges();
            return rentingTransaction;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
