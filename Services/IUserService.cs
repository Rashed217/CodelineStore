using CodelineStore.Data.Model;
using CodelineStore.DTOs.UserDTOs;

namespace CodelineStore.Services
{
    public interface IUserService
    {
        void AddAdmin(UserInput InputUser);
        Task AddClientAndSeller(UserInput InputUser);
        void AddUser(User user);
        void DeleteUser(int uid);
        bool EmailExists(string email);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int uid);
        User GetUserByName(string userName);
        IEnumerable<UserOutput> GetUserByRole(string roleName);
        UserOutput GetUserData(string? userName, int? uid);
        User Login(string email, string password);
        void UpdateUser(User user);
    }
}