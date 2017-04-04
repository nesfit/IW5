using System;
using UserManagement.ConsoleApp.Emails;
using UserManagement.ConsoleApp.Repository;
using UserManagement.ConsoleApp.Visitor;

namespace UserManagement.ConsoleApp
{
    class UserManagementApp
    {
        private readonly DisplayUserVisitor displayUserVisitor;
        private readonly NewsletterFactory newsletterFactory;
        private readonly SendMailUserVisitor sendMailUserVisitor;

        public UserManagementApp(DisplayUserVisitor displayUserVisitor, NewsletterFactory newsletterFactory, SendMailUserVisitor sendMailUserVisitor)
        {
            this.displayUserVisitor = displayUserVisitor;
            this.newsletterFactory = newsletterFactory;
            this.sendMailUserVisitor = sendMailUserVisitor;
        }


        public void Start()
        {
            displayUserVisitor.Print(EmployeeStorage.Instance.Employees);

            Console.WriteLine(new string('-', 100));

            var newsletter = newsletterFactory.CreateNewsletter();
            sendMailUserVisitor.SendEmail(EmployeeStorage.Instance.Developers, newsletter);
        }
    }
}
