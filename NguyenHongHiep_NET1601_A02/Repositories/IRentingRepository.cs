﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface IRentingRepository
{
    IQueryable<RentingTransaction> GetAllTransactions();
    RentingTransaction CreateTransaction(RentingTransaction rentingTransaction);
}