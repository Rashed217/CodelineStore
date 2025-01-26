using CodelineStore.Data.Model;
using CodelineStore.Data.Repositories;
using CodelineStore.DTOs.UserDTOs;

namespace CodelineStore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;


        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;


        }

        public void AddUser(User user)
        {
            _userRepository.AddUser(user);
        }
        public void DeleteUser(int uid)
        {
            var user = _userRepository.GetUserById(uid);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {uid} not found.");

            _userRepository.DeleteUser(user);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUserById(int uid)
        {
            var user = _userRepository.GetUserById(uid);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {uid} not found.");
            return user;
        }
        public void UpdateUser(User user)
        {
            var existingUser = _userRepository.GetUserById(user.UserId);
            if (existingUser == null)
                throw new KeyNotFoundException($"User with ID {user.UserId} not found.");

            _userRepository.UpdateUser(user);
        }
        public User GetUserByName(string userName)
        {
            var user = _userRepository.GetUserByName(userName);
            if (user == null)
                throw new KeyNotFoundException($"User with Name {userName} not found.");
            return user;
        }


        public UserOutput GetUserData(string? userName, int? uid)
        {
            User user = null;

            // Validate that at least one parameter is provided
            if (string.IsNullOrWhiteSpace(userName) && !uid.HasValue)
                throw new ArgumentException("Either username or user ID must be provided.");

            // Retrieve user based on username 
            if (!string.IsNullOrEmpty(userName))
                user = GetUserByName(userName);

            // Retrieve user based on UID
            if (uid.HasValue)
                user = GetUserById(uid.Value);


            if (user == null)
                throw new KeyNotFoundException($"User not found.");

            var outputData = new UserOutput
            {
                UserId = user.UserId,
                UserName = user.Username,
                Email = user.Email,
                Role = user.Role,

            };

            return (outputData);
        }

        public IEnumerable<UserOutput> GetUserByRole(string roleName)
        {
            var users = _userRepository.GetUserByRole(roleName);
            if (users == null)
                throw new KeyNotFoundException($"No Users found");

            List<UserOutput> output = new List<UserOutput>();

            foreach (var user in users)
            {
                // Transform active users into DTOs
                output.Add(new UserOutput
                {
                    UserId = user.UserId,
                    UserName = user.Username,
                    Email = user.Email,
                    Role = user.Role,

                });
            }
            return (output);
        }

        public User Login(string email, string password)
        {
            return _userRepository.Login(email, password);
        }

        public bool EmailExists(string email)
        {
            return _userRepository.EmailExists(email);
        }
        public void AddAdmin(UserInput InputUser)
        {


            //check if there is any active supper admin
            var existingAdmins = _userRepository.GetUserByRole(InputUser.Role);
            if (existingAdmins != null)
            {
                throw new InvalidOperationException("Only one active super admin is allowed in the system.");
            }
            else
            {
                // Default password and email generation
                String defaultPassword = "Admin1234";
                string sanitizedUserName = InputUser.Username.Replace(" ", "");
                string generatedEmail = $"{sanitizedUserName}@CodelineHospital.com";



                // Create new  admin user
                var newAdmin = new User
                {
                    Username = InputUser.Username,
                    Email = generatedEmail,

                    Role = InputUser.Role,

                };
                // Email subject and body
                string subject = "Hospital System Signing In";
                string body = $"Dear {InputUser.Username},\n\nYour  Admin account has been created successfully.\n\nYour default password is: " +
               $"{defaultPassword}\nPlease change your password after logging in.\n\nBest Regards,\nYour System Team";

                // Add the new super admin to the repository
                _userRepository.AddUser(newAdmin);
                // Send email
                //_email.SendEmailAsync("hospitalproject2025@outlook.com", subject, body);
            }

        }

        public async Task AddClientAndSeller(UserInput InputUser)
        {


            const string defaultPassword = "Pass1234";
            // Sanitize the UserName to remove spaces
            string sanitizedUserName = InputUser.Username.Replace(" ", "");

            Random random = new Random();
            int randomNumber;
            string generatedEmail;

            // Ensure unique email generation
            do
            {
                randomNumber = random.Next(1000, 9999);
                generatedEmail = $"{sanitizedUserName}{randomNumber}@CodelineHospital.com";
            } while (_userRepository.EmailExists(generatedEmail));



            var newStaff = new User
            {
                Username = InputUser.Username,
                Email = generatedEmail,


                Role = InputUser.Role,

            };

            // Email subject and body
            string subject = "Hospital System";
            string body = $"Dear {InputUser.Username},\n\nYour account has been created successfully for Amazon App.\n\n" +
                          $"Email: {generatedEmail}\nYour default password is: {defaultPassword}\n" +
                          $"Please change your password after logging in.\n\nBest Regards,\nYour  Admin";

            // Send email asynchronously
            //await _email.SendEmailAsync("hospitalproject2025@outlook.com", subject, body);

            // Add user to the database
            _userRepository.AddUser(newStaff);
        }

    }
}
