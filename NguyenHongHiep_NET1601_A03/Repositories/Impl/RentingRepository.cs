using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl;

public class RentingRepository : IRentingRepository
{
    public Task<IQueryable<RentingTransaction>> GetAllTransactionsAsync()
        => RentingDAO.Instance.GetAllTransactionsAsync();

    public Task<RentingTransaction?> GetTransactionByIdAsync(int id)
        => RentingDAO.Instance.GetTransactionByIdAsync(id);

    public Task<IQueryable<RentingDetail>> GetRentingDetailsByTransactionIdAsync(int id)
        => RentingDAO.Instance.GetRentingDetailsByTransactionIdAsync(id);

    public Task<IQueryable<RentingTransaction>> GetTransactionsByCustomerEmailAsync(string email)
        => RentingDAO.Instance.GetTransactionsByCustomerEmailAsync(email);

    public Task<RentingTransaction> CreateTransactionAsync(RentingTransaction transaction)
        => RentingDAO.Instance.CreateTransactionAsync(transaction);

    public Task<bool> CanCarBeRentedAsync(int carId, DateTime startDate, DateTime endDate)
        => RentingDAO.Instance.CanCarBeRentedAsync(carId, startDate, endDate);

    public Task UpdateAsync(int id, byte status)
        => RentingDAO.Instance.UpdateAsync(id, status);
}
