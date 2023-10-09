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
    public IQueryable<RentingTransaction> GetAllTransactions() => RentingDAO.Instance.GetAllTransactions();

    public RentingTransaction? GetTransactionById(int id) => RentingDAO.Instance.GetTransactionById(id);

    public IQueryable<RentingDetail> GetRentingDetailsByTransactionId(int id) 
        => RentingDAO.Instance.GetRentingDetailsByTransactionId(id);

    public RentingTransaction CreateTransaction(RentingTransaction rentingTransaction) 
        => RentingDAO.Instance.CreateTransaction(rentingTransaction);

    public bool CanCarBeRented(int carId, DateTime startDate, DateTime endDate) 
        => RentingDAO.Instance.CanCarBeRented(carId, startDate, endDate);
}
