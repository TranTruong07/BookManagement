using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookManagementContext context;
        public OrderRepository()
        {
            this.context = new BookManagementContext();
        }

        public int AddOrder(Order order)
        {
            context.Orders.Add(order);
            return context.SaveChanges();
        }

        public List<Order> GetAllOrder()
        {
            return context.Orders.Include(x => x.Customer).Include(x => x.Book).ToList();
        }

        public int UpdateOrder(Order order)
        {
            context.Orders.Update(order);
            return context.SaveChanges();
        }
    }
}
