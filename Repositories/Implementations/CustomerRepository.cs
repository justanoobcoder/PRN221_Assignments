using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations;

public class CustomerRepository : ICustomerRepository
{
    public IQueryable<Customer> GetAll() => CustomerDAO.Instance.GetAll();

    public Customer? GetCustomer(int id) => CustomerDAO.Instance.GetCustomer(id);

    public Customer? GetCustomer(string email) => CustomerDAO.Instance.GetCustomer(email);

    public Customer? GetCustomer(string email, string password) => CustomerDAO.Instance.GetCustomer(email, password);
}
