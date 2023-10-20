using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface ICustomerRepository
{
    Task<IQueryable<Customer>> GetCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer?> GetCustomerByEmailAsync(string email);
    Task<Customer?> GetCustomerByEmailAndPasswordAsync(string email, string password);
    Task<IQueryable<Customer>> GetCustomersByName(string name);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByPhoneAsync(string phone);
    Task<Customer> AddCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(int id);
}
