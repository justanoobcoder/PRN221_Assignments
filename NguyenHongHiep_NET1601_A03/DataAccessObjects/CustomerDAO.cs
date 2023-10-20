using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;

public class CustomerDAO
{
    private static CustomerDAO? instance = null;
    private static readonly object instanceLock = new();
    private CustomerDAO() { }
    public static CustomerDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                instance ??= new();
                return instance;
            }
        }
    }

    public async Task<IQueryable<Customer>> GetCustomersAsync()
    {
        var context = new FucarRentingManagementContext();
        var customers = await Task.Run(() => context.Customers);
        return customers;
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var context = new FucarRentingManagementContext();
        var customer = await Task.Run(() => context.Customers.Find(id));
        return customer;
    }

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        var context = new FucarRentingManagementContext();
        var customer = await context.Customers.SingleOrDefaultAsync(c => c.Email == email);
        return customer;
    }

    public async Task<Customer?> GetCustomerByEmailAndPasswordAsync(string email, string password)
    {
        var context = new FucarRentingManagementContext();
        var customer = await context.Customers.SingleOrDefaultAsync(c => c.Email == email && c.Password == password);
        return customer;
    }

    public async Task<IQueryable<Customer>> GetCustomersByName(string name)
    {
        var context = new FucarRentingManagementContext();
        var customers = await Task.Run(() => context.Customers
        .Where(c => c.CustomerName!.ToLower().Contains(name.ToLower())));
        return customers;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var context = new FucarRentingManagementContext();
        var customer = await context.Customers.SingleOrDefaultAsync(c => c.Email == email);
        return customer != null;
    }

    public async Task<bool> ExistsByPhoneAsync(string phone)
    {
        var context = new FucarRentingManagementContext();
        var customer = await context.Customers.SingleOrDefaultAsync(c => c.Telephone == phone);
        return customer != null;
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        var context = new FucarRentingManagementContext();
        await context.Customers.AddAsync(customer);
        await context.SaveChangesAsync();
        return customer;
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        var context = new FucarRentingManagementContext();
        var customerToUpdate = await context.Customers.FindAsync(customer.CustomerId);
        context.Entry(customerToUpdate!).CurrentValues.SetValues(customer);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var context = new FucarRentingManagementContext();
        var customer = await context.Customers
            .Include(c => c.RentingTransactions)
            .SingleOrDefaultAsync(c => c.CustomerId == id);
        context.Entry(customer!).State = EntityState.Detached;
        if (customer!.RentingTransactions.Count != 0)
        {
            customer.CustomerStatus = 0;
            context.Update(customer);
        }
        else
        {
            context.Remove(customer);
        }
        await context.SaveChangesAsync();
    }
}
