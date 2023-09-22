using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface IRentingTransactionRepository
{
    RentingTransaction? GetById(int id);
    IQueryable<RentingTransaction> GetAll();
    IQueryable<RentingTransaction> GetAllBetween(DateTime startDate, DateTime endDate);
    bool CanRentCar(int carId, DateTime startDate, DateTime endDate);
    RentingTransaction CreateTransaction(RentingTransaction rentingTransaction);
    RentingDetail CreateRentingDetail(RentingDetail rentingDetail);
}
