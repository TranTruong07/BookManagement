using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOrderRepository
    {
        public List<Order> GetAllOrder();
        public int AddOrder(Order order);
        public int UpdateOrder(Order order);
    }
}
