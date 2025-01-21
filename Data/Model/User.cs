using System.ComponentModel.DataAnnotations;

namespace CodelineStore.Data.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; } // Changed from UId to UserId for consistency

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; }

        public string Country { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; } // e.g., "Seller", "Client", or "Admin"

        public virtual Client Client { get; set; } // One-to-one relationship with Client
        public virtual Seller Seller { get; set; } // One-to-one relationship with Seller
    }
}
