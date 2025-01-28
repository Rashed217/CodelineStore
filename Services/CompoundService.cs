using CodelineStore.Data.Model;
using CodelineStore.Data.Repositories;
using static CodelineStore.Services.CompoundService;
using CodelineStore.DTOs;
using CodelineStore.DTOs.ClientDTOs;


namespace CodelineStore.Services
{
    public class CompoundService : ICompoundService
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IOrderService _orderService;
        private readonly IClientService _clientService;
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly ApplicationDbContext _context;

        public CompoundService(IProductService productService, ICategoryService categoryService, IClientService clientService, IOrderService orderService, IOrderDetailsService orderDetailsService, ApplicationDbContext context)
        {

            _productService = productService;
            _categoryService = categoryService;
            _clientService = clientService;
            _orderService = orderService;
            _orderDetailsService = orderDetailsService;
            _context = context;

        }

        public int AddProductWithImage(Product product, string ImagePath)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Assign the image path to the Product.Image property
                    product.Image = ImagePath;

                    // Create the product using your service
                    var productOut = _productService.CreateProductAsync(product);

                    // Save changes to the database
                    _context.SaveChanges();
                    transaction.Commit();

                    // Return the product's ID after saving
                    return product.PId;
                }
                catch (Exception ex)
                {
                    // If any error occurs, rollback the transaction
                    transaction.Rollback();
                    throw new InvalidOperationException("An error occurred when adding product: " + ex.Message);
                }
            }
        }


        public async Task<bool> DeleteProduct(Product product)
        {
            var Deleted = await _productService.DeleteProductAsync(product.PId);
            return Deleted;
        }

        public int CreateOrder(List<(int productId, int quantity, decimal price)> productsIds, int clientId)
        {
            var client = _clientService.GetClientById(clientId);
            if (client == null)
            {
                throw new KeyNotFoundException("Could not find client");
            }
            decimal TotalAmount = 0;
            for (int i = 0; i < productsIds.Count; i++)
            {
                TotalAmount += productsIds[i].price * productsIds[i].quantity;
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var order = new Order
                    {
                        ClientId = client.ClientId,
                        OrderDate = DateTime.Now,
                        TotalAmount = TotalAmount,
                    };

                    var orderId = _orderService.AddOrder(order);
                    var orderDetails = new List<OrderDetail>();


                    for (int i = 0; i < productsIds.Count; i++)
                    {
                        var product = _productService.GetProductByIdAsync(productsIds[i].productId);
                        if (product == null)
                        {
                            throw new KeyNotFoundException("Could not find product to purchase");
                        }
                        TotalAmount += productsIds[i].price * productsIds[i].quantity;
                        orderDetails.Add(new OrderDetail
                        {
                            order = order,
                            OrderId = orderId,
                            ProductId = product.Result.PId,
                            product = product.Result,
                            Quantity = productsIds[i].quantity
                        });
                    }


                    _orderDetailsService.AddOrderProducts(orderDetails);
                    _context.SaveChanges();
                    transaction.Commit();

                    return orderId;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
