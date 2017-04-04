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
        private readonly EmployeeStorage employeeStorage;

        public UserManagementApp(DisplayUserVisitor displayUserVisitor, NewsletterFactory newsletterFactory, SendMailUserVisitor sendMailUserVisitor, EmployeeStorage employeeStorage)
        {
            this.displayUserVisitor = displayUserVisitor;
            this.newsletterFactory = newsletterFactory;
            this.sendMailUserVisitor = sendMailUserVisitor;
            this.employeeStorage = employeeStorage;
        }


        public void Start()
        {
            displayUserVisitor.Print(employeeStorage.Employees);

            Console.WriteLine(new string('-', 100));

            var newsletter = newsletterFactory.CreateNewsletter();
            sendMailUserVisitor.SendEmail(employeeStorage.Developers, newsletter);
        }
    }
}
