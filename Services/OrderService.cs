using CodelineStore.Data.Model;
using CodelineStore.Data.Repositories;

namespace CodelineStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders()
        {
            var orders = _orderRepository.GetAllOrders().ToList();
            if (orders == null || !orders.Any())
            {
                throw new InvalidOperationException("Could not find orders");
            }

            return orders;
        }

        public List<Order> GetClientOrders(int clientId)
        {
            var orders = _orderRepository.GetClientOrders(clientId).ToList();
            if (orders == null || !orders.Any())
            {
                throw new InvalidOperationException("Could not find orders");
            }

            return orders;
        }

        public Order GetOrder(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                throw new KeyNotFoundException("Could not find order");
            }

            return order;
        }

        public int AddOrder(Order order)
        {
            if (order.ClientId == 0)
            {
                throw new ArgumentOutOfRangeException("Client ID not found.");
            }

            if (order.TotalAmount <= 0)
            {
                throw new InvalidOperationException("Order amount must be greater than 0");
            }

            return _orderRepository.AddOrder(order);
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrder(order);
        }

        public void DeleteOrder(int id)
        {
            var order = GetOrder(id);
            _orderRepository.DeleteOrder(order);
        }
    }
}
