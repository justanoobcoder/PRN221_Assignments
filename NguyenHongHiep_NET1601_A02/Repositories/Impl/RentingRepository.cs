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

    public RentingTransaction CreateTransaction(RentingTransaction rentingTransaction) 
        => RentingDAO.Instance.CreateTransaction(rentingTransaction);
}
