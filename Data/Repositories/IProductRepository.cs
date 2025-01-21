using CodelineStore.Data.Model;

namespace CodelineStore.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<ProductImages> CreateProductImagesAsync(ProductImages productImages);
        Task<bool> DeleteProductAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> UpdateProductAsync(Product product);
        Task<ProductImages> UpdateProductImagesAsync(ProductImages productImages);
    }
}