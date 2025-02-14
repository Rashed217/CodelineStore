using CodelineStore.Data.Model;

namespace CodelineStore.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        public User GetUserByName(string userName)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Username == userName);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
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

        public IEnumerable<User> GetUserByRole(string roleName)
        {
            try
            {
                return _context.Users.Where(u => u.Role == roleName == true).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        public User Login(string email, string password)
        {

            // Fetch the user by email
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null && VerifyPassword(password, user.Password))
            {
                return user; // Password matches
            }

            return null; // User not found or password mismatch
        }

        private string HashPassword(string password)
        {
            // Replace this with a secure password hashing algorithm (e.g., BCrypt, SHA256)
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Example: Compare hashed passwords (implement actual hashing logic)
            return HashPassword(enteredPassword) == storedPasswordHash;
        }
    }
}
