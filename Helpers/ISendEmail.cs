
namespace CodelineStore.Helper
{
    public interface ISendEmail
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}