using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface ICustomerRepository
{
    Customer? GetCustomer(int id);
    Customer? GetCustomer(string email);
    Customer? GetCustomer(string email, string password);
}
