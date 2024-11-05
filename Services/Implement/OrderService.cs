using DataAccess.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public int AddOrder(Order order)
        {
            return orderRepository.AddOrder(order);
        }

        public List<Order> GetAllOrder()
        {
            return orderRepository.GetAllOrder();
        }

        public int UpdateOrder(Order order)
        {
            return orderRepository.UpdateOrder(order);
        }
    }
}
