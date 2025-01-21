using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodelineStore.Data.Model
{
    public class OrderDetail
    {
        [ForeignKey("order")]
        public int OrderId { get; set; }
        public Order order { get; set; } // Foreign key to Order

        [ForeignKey("product")]
        public int ProductId { get; set; }
        public Product product { get; set; } // Foreign key to Product

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0.")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Subtotal is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Subtotal must be greater than 0.")]
        public decimal Subtotal { get; set; }
    }
}
