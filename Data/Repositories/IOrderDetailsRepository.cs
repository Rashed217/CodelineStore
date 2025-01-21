using CodelineStore.Data.Model;

namespace CodelineStore.Data.Repositories
{
    public interface IOrderDetailsRepository
    {
        void AddOrderDetails(List<OrderDetail> orderDetails);
        IEnumerable<OrderDetail> GetOrderProducts(int orderId);
    }
}