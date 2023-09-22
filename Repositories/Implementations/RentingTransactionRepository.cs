﻿using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations;

public class RentingTransactionRepository : IRentingTransactionRepository
{
    public IQueryable<RentingTransaction> GetAll() => RentingTransactionDAO.Instance.GetAll();

    public RentingTransaction? GetById(int id) => RentingTransactionDAO.Instance.GetById(id);
}