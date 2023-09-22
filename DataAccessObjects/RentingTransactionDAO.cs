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
}
