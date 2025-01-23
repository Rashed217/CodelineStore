using System.ComponentModel.DataAnnotations;

namespace CodelineStore.DTOs.CategoryDTOs
{
    public class GategoryInput
    {
        
        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; }
    }
}

