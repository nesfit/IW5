using UserManagement.Factory;
using UserManagement.Singleton;
using UserManagement.Visitor;

namespace UserManagement
{
    public class UserManagementApp
    {
        private readonly IEmployeeStorage employeeStorage;
        private readonly NewsletterFactory newsletterFactory;
        private readonly SendMailUserVisitor sendMailUserVisitor;
        private readonly DisplayContactVisitor displayContactVisitor;

        public UserManagementApp(IEmployeeStorage employeeStorage, NewsletterFactory newsletterFactory, SendMailUserVisitor sendEmailUserVisitor, DisplayContactVisitor displayContactVisitor)
        {
            this.employeeStorage = employeeStorage;
            this.newsletterFactory = newsletterFactory;
            sendMailUserVisitor = sendEmailUserVisitor;
            this.displayContactVisitor = displayContactVisitor;
        }

        public void Run()
        {
            var newsletter = newsletterFactory.CreateNewsletter();
            
            sendMailUserVisitor.SendEmail(employeeStorage.Developers, newsletter);

            displayContactVisitor.Print(employeeStorage.Developers);
        }
    }
}