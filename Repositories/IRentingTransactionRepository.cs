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
}
