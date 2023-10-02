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
    private static CustomerDAO instance = null;
    private static readonly object instanceLock = new object();

    private CustomerDAO() { }
    public static CustomerDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new CustomerDAO();
                }
                return instance;
            }
        }
    }

    public IQueryable<Customer> GetAll()
    {
        var context = new FucarRentingManagementContext();
        var customers = context.Customers;
        return customers;
    }

    public Customer? GetById(int id)
    {
        var context = new FucarRentingManagementContext();
        var customer = context.Customers.Find(id);
        return customer;
    }

    public Customer? GetByEmail(string email)
    {
        var context = new FucarRentingManagementContext();
        var customer = context.Customers.SingleOrDefault(c => c.Email == email);
        return customer;
    }

    public Customer? GetByEmailAndPassword(string email, string password)
    {
        var context = new FucarRentingManagementContext();
        var customer = context.Customers.SingleOrDefault(c => c.Email == email && c.Password == password);
        return customer;
    }

    public IQueryable<Customer> GetByName(string name)
    {
        var context = new FucarRentingManagementContext();
        var customers = context.Customers
            .Where(c => c.CustomerName!.ToLower().Contains(name.ToLower()));
        return customers;
    }

    public bool ExistsByEmail(string email)
    {
        var context = new FucarRentingManagementContext();
        var customer = context.Customers.SingleOrDefault(c => c.Email == email);
        return customer != null;
    }

    public bool ExistsByTelephone(string telephone)
    {
        var context = new FucarRentingManagementContext();
        var customer = context.Customers.SingleOrDefault(c => c.Telephone == telephone);
        return customer != null;
    }

    public Customer Insert(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException("Customer is null");
        var context = new FucarRentingManagementContext();
        if (ExistsByEmail(customer.Email))
            throw new ArgumentException("Customer with this email already exists");
        if (ExistsByTelephone(customer.Telephone!))
            throw new ArgumentException("Customer with this telephone already exists");
        context.Customers.Add(customer);
        context.SaveChanges();
        return customer;
    }

    public void Update(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException("Customer is null");
        var context = new FucarRentingManagementContext();
        var customerToUpdate = context.Customers.Find(customer.CustomerId);
        if (customerToUpdate == null)
            throw new ArgumentException("Customer with this id does not exist");
        if (ExistsByEmail(customer.Email) && customerToUpdate.Email != customer.Email)
            throw new ArgumentException("Customer with this email already exists");
        if (ExistsByTelephone(customer.Telephone!) && customerToUpdate.Telephone != customer.Telephone)
            throw new ArgumentException("Customer with this telephone already exists");
        context.Entry(customerToUpdate).CurrentValues.SetValues(customer);
        context.SaveChanges();
    }

    public void Delete(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException("Customer is null");
        var context = new FucarRentingManagementContext();
        Customer? c = context.Customers
            .Include(c => c.RentingTransactions)
            .SingleOrDefault(c => c.CustomerId == customer.CustomerId);
        if (c == null)
            throw new Exception("Customer not found");
        context.Entry(c).State = EntityState.Detached;
        if (c.RentingTransactions.Count != 0)
        {
            customer.CustomerStatus = 0;
            context.Update(customer);
        }
        else
        {
            context.Remove(customer);
        }
        context.SaveChanges();
    }
}
