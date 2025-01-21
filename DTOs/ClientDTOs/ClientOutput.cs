using CodelineStore.DTOs.UserDTOs;

namespace CodelineStore.DTOs.ClientDTOs
{
    public class ClientOutput: UserInput
    {
        
            public int ID { get; set; }
            public int CID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public int Orders { get; set; }
        



    }
}
