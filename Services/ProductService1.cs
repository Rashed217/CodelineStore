using CodelineStore.Data.Model;
using CodelineStore.Data.Repositories;

namespace CodelineStore.Services
{
    public class ProductService1 : IProductService1
    {
        private readonly IProductRepository1 _repository;
        public ProductService1(IProductRepository1 repository)
        {
            _repository = repository;
        }

        public List<Product> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }

        public Product GetByID(int id)
        {
            return _repository.GetByID(id);
        }

        public Product GetByName(string name)
        {
            return _repository.GetByName(name);

        }

        public List<Product> GetPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _repository.GetPriceRange(minPrice, maxPrice);
        }

        public int UpdateProduct(Product pr)
        {
            return _repository.UpdateProduct(pr);
        }

        public int DeleteProduct(int id)
        {
            return _repository.DeleteProduct(id);
        }
    }
}
