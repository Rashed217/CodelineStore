﻿using CodelineStore.Data.Model;
using CodelineStore.DTOs.ProductDTO;

namespace CodelineStore.Services
{
    public interface IProductService
    {
        Product CreateProductAsync(Product product);
        ProductImages CreateProductImagesAsync(ProductImages productImages);
        Task<bool> DeleteProductAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<Product> GetProductByIdAsync(int id);
        Task UpdateProductAsync(Product product);
        //Task<ProductImages> UpdateProductImagesAsync(ProductImages productImages);
    }
}