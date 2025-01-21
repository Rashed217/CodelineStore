using CodelineStore.Data.Model;

namespace CodelineStore.Data.Repositories
{
    public interface IClientRepository
    {
        Client AddClient(Client client);
        void DeletClient(Client client);
        bool EmailExists(string email);
        IEnumerable<Client> GetAllClients();
        Client GetClientByName(string ClientName);
        Client GetClientsById(int id);
        bool IsValidRole(string roleName);
        Client UpdateClient(Client client);
    }
}