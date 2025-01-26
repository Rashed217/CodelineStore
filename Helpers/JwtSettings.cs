namespace CodelineStore.Helpers
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }

        public int ExpirationInMinutes { get; set; }
    
    }
}
