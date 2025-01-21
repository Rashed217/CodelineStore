using CodelineStore.Data.Model;
using CodelineStore.Data.Repositories;

namespace CodelineStore.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailsRepository _orderDetailsRepository;

        public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public List<OrderDetail> GetOrderProducts(int orderId)
        {
            var products = _orderDetailsRepository.GetOrderProducts(orderId).ToList();
            if (products == null || !products.Any())
            {
                throw new KeyNotFoundException("Could not find products of this order");
            }

            return products;
        }

        public void AddOrderProducts(List<OrderDetail> orderDetails)
        {
            _orderDetailsRepository.AddOrderDetails(orderDetails);
        }
    }
}
