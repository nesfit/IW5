using System;
using UserManagement.Factory;
using UserManagement.Singleton;
using UserManagement.Visitor.Advanced;

namespace UserManagement
{
    class UserManagementApp
    {
        private readonly DisplayUserVisitor displayUserVisitor;
        private readonly NewsletterFactory newsletterFactory;
        private readonly SendMailUserVisitor sendMailUserVisitor;
        private readonly IEmployeeStorage employeeStorage;

        public UserManagementApp(DisplayUserVisitor displayUserVisitor, NewsletterFactory newsletterFactory, SendMailUserVisitor sendMailUserVisitor, IEmployeeStorage employeeStorage)
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