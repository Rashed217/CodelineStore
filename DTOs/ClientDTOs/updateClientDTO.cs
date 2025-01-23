using CodelineStore.DTOs.UserDTOs;

namespace CodelineStore.DTOs.ClientDTOs
{
    public class updateClientDTO : UserInput
    {
        public int? ClientId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public UserInput User { get; set; }
    }
}
