using CodelineStore.Data.Model;

namespace CodelineStore.Data.Repositories
{
    public interface ICategoryRepository
    {
        int AddCategory(Category category);
        void DeleteCategory(Category category);
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        Category GetCategoryByName(string name);
        int UpdateCategory(Category category);
    }
}