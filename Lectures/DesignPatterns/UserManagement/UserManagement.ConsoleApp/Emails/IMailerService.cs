namespace UserManagement.ConsoleApp.Emails
{
    public interface IMailerService
    {
        void SendEmail(string to, Email email);
    }
}