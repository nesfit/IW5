using UserManagement.Models;

namespace UserManagement.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, Email email);
    }
}