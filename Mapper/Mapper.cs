using AutoMapper;
using CodelineStore.Data.Model;
using CodelineStore.DTOs.ClientDTOs;
using CodelineStore.DTOs.SellerDTOs;
using CodelineStore.DTOs.UserDTOs;

namespace CodelineStore.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserInput, User>();
            CreateMap<User, UserOutput>();

            CreateMap<ClientInput, Client>();
            CreateMap<Client, ClientOutput>();

            CreateMap<SellerInput, Seller>();
            CreateMap<Seller, SellerOutput>();
        }
    }
}
