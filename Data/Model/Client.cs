using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodelineStore.Data.Model
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; } // Matches UserId in User
        public User User { get; set; } // One-to-one relationship with User

        public virtual ICollection<Order> Orders { get; set; } // Navigation property for orders
        public virtual ICollection<ProductReview> ProductReviews { get; set; } // Navigation property for reviews
        public virtual ICollection<ProductReport> ProductReports { get; set; } // Navigation property for reports
    }
}

