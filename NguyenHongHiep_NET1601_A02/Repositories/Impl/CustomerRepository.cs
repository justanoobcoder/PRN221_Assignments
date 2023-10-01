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
    public IQueryable<Customer> GetAll() => CustomerDAO.Instance.GetAll();

    public IQueryable<Customer> GetByName(string name) => CustomerDAO.Instance.GetByName(name);

    public Customer? GetById(int id) => CustomerDAO.Instance.GetById(id);

    public Customer? GetByEmail(string email) => CustomerDAO.Instance.GetByEmail(email);

    public Customer? GetByEmailAndPassword(string email, string password)
        => CustomerDAO.Instance.GetByEmailAndPassword(email, password);

    public bool ExistsByEmail(string email) => CustomerDAO.Instance.ExistsByEmail(email);

    public bool ExistsByTelephone(string telephone) 
        => CustomerDAO.Instance.ExistsByTelephone(telephone);

    public Customer Insert(Customer customer) => CustomerDAO.Instance.Insert(customer);

    public void Update(Customer customer) => CustomerDAO.Instance.Update(customer);

    public void Delete(Customer customer) => CustomerDAO.Instance.Delete(customer);
}
