using UserManagement.Services;

namespace UserManagement.Models
{
    public class Contact
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Name => $"{Firstname} {Lastname}";
        public string EmailAddress { get; set; }

        public void SendMail(Email email)
        {
            var emailService = new EmailService();
            emailService.SendEmail(EmailAddress, email);
        }
    }
}