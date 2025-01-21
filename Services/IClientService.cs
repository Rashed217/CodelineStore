using CodelineStore.Data.Model;
using CodelineStore.DTOs.ClientDTOs;

namespace CodelineStore.Services
{
    public interface IClientService
    {
        void AddClient(ClientOutput input);
        bool EmailExists(string email);
        IEnumerable<Client> GetAllClients();
        Client GetClientById(int clientId);
        Client GetClientByName(string clientName);
        ClientOutput GetClientData(string? clientName, int? clientId);
        void UpdateClient(Client client);
        void UpdateClientDetails(updateClientDTO input);
    }
}