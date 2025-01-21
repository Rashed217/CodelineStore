using System.ComponentModel.DataAnnotations;

namespace CodelineStore.DTOs.CategoryDTOs
{
    public class GategoryInpot
    {
        
        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; }
    }
}

