using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl;

public class CustomerRepository : ICustomerRepository
{
    public Task<IQueryable<Customer>> GetCustomersAsync()
        => CustomerDAO.Instance.GetCustomersAsync();

    public Task<Customer?> GetCustomerByIdAsync(int id)
        => CustomerDAO.Instance.GetCustomerByIdAsync(id);

    public Task<Customer?> GetCustomerByEmailAsync(string email)
        => CustomerDAO.Instance.GetCustomerByEmailAsync(email);

    public Task<Customer?> GetCustomerByEmailAndPasswordAsync(string email, string password)
        => CustomerDAO.Instance.GetCustomerByEmailAndPasswordAsync(email, password);

    public Task<IQueryable<Customer>> GetCustomersByName(string name)
        => CustomerDAO.Instance.GetCustomersByName(name);

    public Task<bool> ExistsByEmailAsync(string email)
        => CustomerDAO.Instance.ExistsByEmailAsync(email);

    public Task<bool> ExistsByPhoneAsync(string phone)
        => CustomerDAO.Instance.ExistsByPhoneAsync(phone);

    public Task<Customer> AddCustomerAsync(Customer customer)
        => CustomerDAO.Instance.AddCustomerAsync(customer);

    public Task UpdateCustomerAsync(Customer customer)
        => CustomerDAO.Instance.UpdateCustomerAsync(customer);

    public Task DeleteCustomerAsync(int id)
        => CustomerDAO.Instance.DeleteCustomerAsync(id);
}
