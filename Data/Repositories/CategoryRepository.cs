using CodelineStore.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CodelineStore.Data.Repositories
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.CatId == id);
        }

        public Category GetCategoryByName(string name)
        {
            return _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.Name == name);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.Include(c => c.Products);
        }

        public int AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category.CatId;
        }

        public int UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();

            return category.CatId;
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
