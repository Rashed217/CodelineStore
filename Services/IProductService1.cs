using CodelineStore.Data.Model;

namespace CodelineStore.Services
{
    public interface IProductService1
    {
        int DeleteProduct(int id);
        List<Product> GetAllProducts();
        Product GetByID(int id);
        Product GetByName(string name);
        List<Product> GetPriceRange(decimal minPrice, decimal maxPrice);
        int UpdateProduct(Product pr);
    }
}