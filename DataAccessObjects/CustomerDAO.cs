using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;

public sealed class CustomerDAO
{
    private static CustomerDAO instance = null;
    private static readonly object locker = new object();

    private CustomerDAO() { }

    public static CustomerDAO Instance
    {
        get
        {
            lock (locker)
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
        try
        {
            var context = new FUCarRentingManagementContext();
            return context.Customers;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Customer? GetCustomer(int id)
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            return context.Customers
                .Where(c => c.CustomerId == id)
                .SingleOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Customer? GetCustomer(string email)
    {
        try
        {
            var context = new FUCarRentingManagementContext();
            return context.Customers
                .Where(c => c.Email == email)
                .SingleOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Customer? GetCustomer(string email, string password)
    {
        try
        {
            var customer = GetCustomer(email);
            if (customer != null && customer.Password == password)
            {
                return customer;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Customer Create(Customer customer)
    {
        if (customer == null)
            throw new Exception("Customer is required");

        try
        {
            var context = new FUCarRentingManagementContext();
            if (GetCustomer(customer.Email) != null)
                throw new Exception("Email is already in use");
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
