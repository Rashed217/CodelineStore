using CodelineStore.Data.Model;
using CodelineStore.DTOs.ProductDTO;

namespace CodelineStore.Data.Repositories
{
    public interface IProductRepository
    {
        Product CreateProductAsync(Product product);
        ProductImages CreateProductImagesAsync(ProductImages productImages);
        Task<bool> DeleteProductAsync(int id);
        List<Product> GetAllProductsAsync();
        Task<ProductDto> GetProductDetailsAsync(int productId);
        Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product> GetProductByIdAsync(int productId);
        Task UpdateProductAsync(Product product);
        Task<ProductImages> UpdateProductImagesAsync(ProductImages productImages);
    }
}