using CodelineStore.DTOs.UserDTOs;

namespace CodelineStore.DTOs.ClientDTOs
{
    public class ClientInput : UserInput
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public UserInput User { get; set; }
    }
}
