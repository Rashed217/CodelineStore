namespace CodelineStore.Services
{
    public class UserState
    {
        public string? UserName { get; set; }

        public bool IsLoggedIn => !string.IsNullOrEmpty(UserName);

        public void LogIn(string userName)
        {
            UserName = userName;
        }

        public void LogOut()
        {
            UserName = null;
        }
    }
}
