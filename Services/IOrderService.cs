using CodelineStore.Data.Model;

namespace CodelineStore.Services
{
    public interface IOrderService
    {
        int AddOrder(Order order);
        void DeleteOrder(int id);
        List<Order> GetAllOrders();
        List<Order> GetClientOrders(int clientId);
        Order GetOrder(int id);
        void UpdateOrder(Order order);
    }
}