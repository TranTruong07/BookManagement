using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICustomerService
    {
        public List<Customer> GetAllCustomer();
        public int AddCustomer(Customer customer);
        public int UpdateCustomer(Customer customer);
        public Customer? GetCustomerById(int id);
    }
}
