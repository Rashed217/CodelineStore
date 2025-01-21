using CodelineStore.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CodelineStore.Data.Repositories
{
    public class ClientRepository
    {
        private readonly ApplicationDbContext _context;
        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _context.Clients;
        }

        public Client GetClientsById(int id)
        {
            return _context.Clients.Find(id);
        }



        public Client AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return client;
        }

        public Client UpdateClient(Client client)
        {
            _context.Clients.Update(client);
            _context.SaveChanges();
            return client;
        }

        public void DeletClient(Client client)
        {
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }


        public bool IsValidRole(string roleName)
        {
            var validRoles = new List<string> { "Client", "Seller" };
            return validRoles.Contains(roleName);
        }


        public bool EmailExists(string email)
        {
            try
            {
                return _context.Users.Any(u => u.Email == email);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }
        public Client GetClientByName(string ClientName)
        {
            if (string.IsNullOrEmpty(ClientName))
            {
                throw new ArgumentException("client name cannot be null or empty.", nameof(ClientName));
            }

            try
            {
                // Use Include to join User with Doctor and filter by UserName
                var client = _context.Clients
                    .Include(d => d.User) // Join with User table
                    .FirstOrDefault(d => d.User.Username == ClientName);

                return client;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while accessing the database.", ex);
            }
        }
    }
}
