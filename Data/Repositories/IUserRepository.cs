using CodelineStore.Data.Model;

namespace CodelineStore.Data.Repositories
{
    public interface IUserRepository
    {
        User AddUser(User user);
        void DeleteUser(User user);
        bool EmailExists(string email);
        IEnumerable<User> GetAllUsers();
        User GetUserByEmail(string email);
        User GetUserById(int id);
        User GetUserByName(string userName);
        IEnumerable<User> GetUserByRole(string roleName);
        bool IsValidRole(string roleName);
        User UpdateUser(User user);
    }
}