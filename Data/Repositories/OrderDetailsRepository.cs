using CodelineStore.Data.Model;
using Microsoft.EntityFrameworkCore;


namespace CodelineStore.Data.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderDetail> GetOrderProducts(int orderId)
        {
            return _context.OrderDetails.Include(o => o.order)
                .Include(o => o.product)
                .Where(o => o.order.OId == orderId);
        }

        public void AddOrderDetails(List<OrderDetail> orderDetails)
        {
            _context.OrderDetails.AddRange(orderDetails);
        }
    }
}
