using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;

public class RentingDAO
{
    private static RentingDAO? instance = null;
    private static readonly object padlock = new();
    private RentingDAO() { }
    public static RentingDAO Instance
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

    public async Task<IQueryable<RentingTransaction>> GetAllTransactionsAsync()
    {
        var context = new FucarRentingManagementContext();
        var transactions = await Task.Run(() => context.RentingTransactions
            .Include(t => t.Customer)
            .Include(t => t.RentingDetails));
        return transactions;
    }

    public async Task<RentingTransaction?> GetTransactionByIdAsync(int id)
    {
        var context = new FucarRentingManagementContext();
        var transaction = await context.RentingTransactions
            .Include(t => t.Customer)
            .Include(t => t.RentingDetails)
            .SingleOrDefaultAsync(t => t.RentingTransationId == id);
        return transaction;
    }

    public async Task<IQueryable<RentingDetail>> GetRentingDetailsByTransactionIdAsync(int id)
    {
        var context = new FucarRentingManagementContext();
        var rentingDetails = await Task.Run(() => context.RentingDetails
            .Include(rd => rd.Car)
            .Where(rd => rd.RentingTransactionId == id));
        return rentingDetails;
    }

    public async Task<IQueryable<RentingTransaction>> GetTransactionsByCustomerEmailAsync(string email)
    {
        var context = new FucarRentingManagementContext();
        var transactions = await Task.Run(() => context.RentingTransactions
            .Include(r => r.Customer)
            .Include(r => r.RentingDetails)
            .Where(r => r.Customer.Email == email));
        return transactions;
    }

    private async Task<int> GetNextIdAsync()
    {
        var context = new FucarRentingManagementContext();
        int count = await context.RentingTransactions.CountAsync();
        return (count == 0) ? 1 : context.RentingTransactions.Max(e => e.RentingTransationId) + 1;
    }

    public async Task<RentingTransaction> CreateTransactionAsync(RentingTransaction transaction)
    {
        var context = new FucarRentingManagementContext();
        transaction.RentingTransationId = await GetNextIdAsync();
        await context.RentingTransactions.AddAsync(transaction);
        await context.SaveChangesAsync();
        return transaction;
    }

    public async Task<bool> CanCarBeRentedAsync(int carId, DateTime startDate, DateTime endDate)
    {
        var context = new FucarRentingManagementContext();
        var rentingDetails = await context.RentingDetails
            .Where(d => d.CarId == carId)
            .Where(d => d.StartDate <= endDate && d.EndDate >= startDate)
            .ToListAsync();
        return rentingDetails.Count == 0;
    }

    public async Task UpdateAsync(int id, byte status)
    {
        var context = new FucarRentingManagementContext();
        var rentingTransaction = await context.RentingTransactions.FindAsync(id);
        rentingTransaction!.RentingStatus = status;
        await context.SaveChangesAsync();
    }
}
