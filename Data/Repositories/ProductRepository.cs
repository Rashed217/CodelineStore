using CodelineStore.Data.Model;
using CodelineStore.DTOs.ProductDTO;
using Microsoft.EntityFrameworkCore;

namespace CodelineStore.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProductsAsync()
        {
            return _context.Products
                .Include(p => p.Seller)
                .Include(p => p.Category)
                .ToList();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            // Fetch products that belong to the specified category
            return await _context.Products
                .Include(p => p.Image) // Include related product images
                .Where(p => p.CategoryId == categoryId) // Filter by categoryId
                .ToListAsync(); // Execute query and return the list
        }

        public async Task<ProductDto> GetProductDetailsAsync(int productId)
        {
            // Query the database for product details and map to ProductDto
            var product = await _context.Products
                .Where(p => p.PId == productId)
                .Select(p => new ProductDto
                {
                    ProductId = p.PId,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image
                })
                .FirstOrDefaultAsync();

            return product; // Return the mapped ProductDto
        }

        public ProductImages CreateProductImagesAsync(ProductImages productImages)
        {
            _context.ProductImages.Add(productImages);
            return productImages;
        }
        public async Task<ProductImages> UpdateProductImagesAsync(ProductImages productImages)
        {
            _context.ProductImages.Add(productImages);
            await _context.SaveChangesAsync();
            return productImages;
        }

        public Product CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);  // Ensure the product is updated
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
