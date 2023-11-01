using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface IRentingRepository
{
    Task<IQueryable<RentingTransaction>> GetAllTransactionsAsync();
    Task<RentingTransaction?> GetTransactionByIdAsync(int id);
    Task<IQueryable<RentingDetail>> GetRentingDetailsByTransactionIdAsync(int id);
    Task<IQueryable<RentingTransaction>> GetTransactionsByCustomerEmailAsync(string email);
    Task<RentingTransaction> CreateTransactionAsync(RentingTransaction transaction);
    Task<bool> CanCarBeRentedAsync(int carId, DateTime startDate, DateTime endDate);
    Task UpdateAsync(int id, byte status);
}
