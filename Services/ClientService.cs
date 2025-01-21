using CodelineStore.Data.Model;
using CodelineStore.Data.Repositories;
using CodelineStore.Services;
using CodelineStore.DTOs.ClientDTOs;
using CodelineStore.DTOs.UserDTOs;


namespace CodelineStore.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserService _userService;

        public ClientService(IClientRepository clientRepository, IUserService userService)
        {
            _clientRepository = clientRepository;
            _userService = userService;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clientRepository.GetAllClients();
        }

        public Client GetClientById(int clientId)
        {
            var client = _clientRepository.GetClientsById(clientId);
            if (client == null)
                throw new KeyNotFoundException($"Client with ID {clientId} not found.");
            return client;
        }

        public bool EmailExists(string email)
        {
            return _clientRepository.EmailExists(email);
        }

        public ClientOutput GetClientData(string? clientName, int? clientId)
        {
            if (string.IsNullOrWhiteSpace(clientName) && !clientId.HasValue)
                throw new ArgumentException("Either client name or client ID must be provided.");

            Client client = null;

            if (!string.IsNullOrEmpty(clientName))
                client = GetClientByName(clientName);

            if (clientId.HasValue)
                client = GetClientById(clientId.Value);

            if (client == null)
                throw new KeyNotFoundException("Client not found.");

            return new ClientOutput
            {
                CID = client.ClientId,
                Phone = client.PhoneNumber,
                Address = client.Address,

            };
        }

        public void AddClient(ClientOutput input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Client information is missing.");

            if (input.ID <= 0)
                throw new ArgumentException("Invalid client ID.");

            var user = _userService.GetUserById(input.ID);
            if (user == null)
                throw new KeyNotFoundException($"No user found with ID {input.ID}.");

            if (!user.Role.Equals("client", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("The provided user ID does not belong to a client.");

            var client = new Client
            {
                ClientId = user.UserId,
                PhoneNumber = input.Phone,
                Address = input.Address,

            };

            _clientRepository.AddClient(client);
        }

        public void UpdateClientDetails(updateClientDTO input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Client update details are required.");

            if (!input.ClientId.HasValue)
                throw new ArgumentException("Client ID is required.");

            var existingClient = _clientRepository.GetClientsById(input.ClientId.Value);
            var existingUser = _userService.GetUserById(input.ClientId.Value);

            if (existingClient == null || existingUser == null)
                throw new KeyNotFoundException("Client or associated user not found.");

            existingClient.PhoneNumber = input.PhoneNumber;
            existingClient.Address = input.Address;

            _clientRepository.UpdateClient(existingClient);
        }

        public void UpdateClient(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "Client information is required.");

            var existingClient = _clientRepository.GetClientsById(client.ClientId);
            if (existingClient == null)
                throw new KeyNotFoundException($"Client with ID {client.ClientId} not found.");

            _clientRepository.UpdateClient(client);
        }

        public Client GetClientByName(string clientName)
        {
            if (string.IsNullOrEmpty(clientName))
                throw new ArgumentException("Client name cannot be null or empty.");

            var client = _clientRepository.GetClientByName(clientName);
            if (client == null)
                throw new KeyNotFoundException($"Client with name '{clientName}' not found.");

            return client;
        }
    }
}
