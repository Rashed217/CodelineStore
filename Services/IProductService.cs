using CodelineStore.Data.Model;

namespace CodelineStore.Services
{
    public interface IProductService
    {
        Product CreateProductAsync(Product product);
        ProductImages CreateProductImagesAsync(ProductImages productImages);
        Task<bool> DeleteProductAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> UpdateProductAsync(Product product);
        Task<ProductImages> UpdateProductImagesAsync(ProductImages productImages);
    }
}