using Microsoft.AspNetCore.Identity;
using CodelineStore.DTOs.UserDTOs;

namespace CodelineStore.DTOs.SellerDTOs
{
    public class SellerInput : UserInput
    {
        public string ProfileImagePath { get; set; }
        public SellerInput()
        {
            Role = "Seller"; // Default role for sellers
            User = new UserInput(); // Ensure User is initialized
        }
        public UserInput User { get; set; }
    }

}
