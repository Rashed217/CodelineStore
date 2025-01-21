namespace CodelineStore.DTOs.UserDTOs
{
    public class UserInput
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string Role { get; set; } = "Client"; // Default role
    } 
}
