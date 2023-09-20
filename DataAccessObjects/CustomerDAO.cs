using BusinessObjects;
using Microsoft.EntityFrameworkCore;
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
                .Include(c => c.RentingTransactions)
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
                .Include(c => c.RentingTransactions)
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
            if (GetCustomer(customer.CustomerId) != null)
                throw new Exception("Customer ID is already in use");
            if (GetCustomer(customer.Email) != null)
                throw new Exception("Email is already in use");
            customer.CustomerStatus = 1;
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Update(Customer customer)
    {
        if (customer == null)
            throw new Exception("Customer is required");
        try
        {
            Customer? c = GetCustomer(customer.CustomerId);
            if (c == null)
                throw new Exception("Customer not found");
            if (c.Email != customer.Email && GetCustomer(customer.Email) != null)
                throw new Exception("Email is already in use");
            var context = new FUCarRentingManagementContext();
            context.Entry(c).State = EntityState.Detached;
            context.Update(customer);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Delete(Customer customer)
    {
        if (customer == null)
            throw new Exception("Customer is required");
        try
        {
            Customer? c = GetCustomer(customer.CustomerId);
            if (c == null)
                throw new Exception("Customer not found");
            var context = new FUCarRentingManagementContext();
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
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
