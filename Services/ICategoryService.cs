using CodelineStore.Data.Model;
using CodelineStore.DTOs.CategoryDTOs;

namespace CodelineStore.Services
{
    public interface ICategoryService
    {
        int AddCategory(GategoryInput categoryInput);
        void DeleteCategory(int id);
        List<CategoryOutput> GetAllCategories();
        List<Category> GetAllCategoriesWithRelatedData();
        CategoryOutput GetCategoryById(int id);
        Category GetCategoryByIdWithRelatedData(int id);
        int UpdateCategory(GategoryInput categoryInput, int id);
    }
}