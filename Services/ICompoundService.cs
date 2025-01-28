using CodelineStore.Data.Model;

namespace CodelineStore.Services
{
    public interface ICompoundService
    {
        int AddProductWithImage(Product product, string ImagePath);
        int CreateOrder(List<(int productId, int quantity, decimal price)> productsIds, int clientId);
        Task<bool> DeleteProduct(Product product);
    }
}