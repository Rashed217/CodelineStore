using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodelineStore.Data.Model
{
    public class Order
    {
        [Key]
        public int OId { get; set; }

        [ForeignKey("client")]
        public int ClientId { get; set; }
        public Client client { get; set; } // Foreign key to Client

        [Required(ErrorMessage = "Order date is required.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Total amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than 0.")]
        public decimal TotalAmount { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } // One-to-Many relationship
    }
}
