using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;

public class RentingDAO
{
    private static RentingDAO? _instance = null;
    private static readonly object _lock = new();

    private RentingDAO()
    {
    }
    public static RentingDAO Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new RentingDAO();
                }
                return _instance;
            }
        }
    }

    public IQueryable<RentingTransaction> GetAllTransactions()
    {
        var context = new FucarRentingManagementContext();
        return context.RentingTransactions
            .Include(r => r.Customer)
            .Include(r => r.RentingDetails);
    }
}
