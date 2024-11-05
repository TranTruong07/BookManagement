using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BookManagementContext context;
        public CustomerRepository()
        {
            this.context = new BookManagementContext();
        }

        public int AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            return context.SaveChanges();
        }

        public List<Customer> GetAllCustomer()
        {
            return context.Customers.ToList();
        }

        public Customer? GetCustomerById(int id)
        {
            return context.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
        }

        public int UpdateCustomer(Customer customer)
        {
            context.Customers.Update(customer);
            return context.SaveChanges();
        }
    }
}
