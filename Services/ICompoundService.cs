using CodelineStore.Data.Model;

namespace CodelineStore.Services
{
    public interface ICompoundService
    {
        int AddProduct(Product product, string ImgePath);
        int CreateOrder(List<(int productId, int quantity, decimal price)> productsIds, int clientId);
        Task<bool> DeleteProduct(Product product);
    }
}