using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface IRentingRepository
{
    IQueryable<RentingTransaction> GetAllTransactions();
    RentingTransaction? GetTransactionById(int id);
    IQueryable<RentingDetail> GetRentingDetailsByTransactionId(int id);
    RentingTransaction CreateTransaction(RentingTransaction rentingTransaction);
    bool CanCarBeRented(int carId, DateTime startDate, DateTime endDate);
}
