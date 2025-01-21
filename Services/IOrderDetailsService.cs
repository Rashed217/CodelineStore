using CodelineStore.Data.Model;

namespace CodelineStore.Services
{
    public interface IOrderDetailsService
    {
        void AddOrderProducts(List<OrderDetail> orderDetails);
        List<OrderDetail> GetOrderProducts(int orderId);
    }
}