using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface ICustomerRepository
{
    IQueryable<Customer> GetAll();
    Customer? GetCustomer(int id);
    Customer? GetCustomer(string email);
    Customer? GetCustomer(string email, string password);
    Customer Create(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
}
