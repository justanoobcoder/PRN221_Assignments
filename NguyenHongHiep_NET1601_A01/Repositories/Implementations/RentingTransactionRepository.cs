using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations;

public class RentingTransactionRepository : IRentingTransactionRepository
{
    public bool CanRentCar(int carId, DateTime startDate, DateTime endDate) => RentingTransactionDAO.Instance.CanRentCar(carId, startDate, endDate);

    public RentingDetail CreateRentingDetail(RentingDetail rentingDetail) => RentingTransactionDAO.Instance.CreateRentingDetail(rentingDetail);

    public RentingTransaction CreateTransaction(RentingTransaction rentingTransaction) => RentingTransactionDAO.Instance.CreateTransaction(rentingTransaction);

    public IQueryable<RentingTransaction> GetAll() => RentingTransactionDAO.Instance.GetAll();

    public IQueryable<RentingTransaction> GetAllBetween(DateTime startDate, DateTime endDate)
        => RentingTransactionDAO.Instance.GetAllBetween(startDate, endDate);

    public RentingTransaction? GetById(int id) => RentingTransactionDAO.Instance.GetById(id);
}
