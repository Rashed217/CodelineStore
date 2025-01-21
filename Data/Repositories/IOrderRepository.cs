using CodelineStore.Data.Model;

namespace CodelineStore.Data.Repositories
{
    public interface IOrderRepository
    {
        int AddOrder(Order order);
        void DeleteOrder(Order order);
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetClientOrders(int clientId);
        Order GetOrderById(int id);
        void UpdateOrder(Order order);
    }
}