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
    IQueryable<Customer> GetByName(string name);
    Customer? GetById(int id);
    Customer? GetByEmail(string email);
    Customer? GetByEmailAndPassword(string email, string password);
    bool ExistsByEmail(string email);
    bool ExistsByTelephone(string telephone);
    Customer Insert(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
}
