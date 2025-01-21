using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodelineStore.Data.Model
{
    public class Seller
    {
        [Key]
        public int SId { get; set; } // Changed from SId to SellerId for consistency

        [ForeignKey(nameof(User))]
        public int UserId { get; set; } // Matches UserId in User
        public User User { get; set; } // One-to-one relationship with User

        public int SellerRating { get; set; }

        public string ProfileImagePath { get; set; } // Optional: Seller profile image path

        public virtual ICollection<Product> Products { get; set; } // Navigation property for products sold by the seller
    }
}
