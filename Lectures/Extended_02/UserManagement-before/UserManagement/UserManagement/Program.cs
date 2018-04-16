using Castle.MicroKernel.Registration;
using Castle.Windsor;
using UserManagement.Decorator;
using UserManagement.Visitor.Advanced;
using UserManagement.Factory;
using UserManagement.Models;
using UserManagement.Services;
using UserManagement.Singleton;

namespace UserManagement
{
    class Program
    {
        static void Main()
        {
            //var program = new UserManagementApp(new DisplayUserVisitor(), new NewsletterFactory(), new SendMailUserVisitor(new LoggedEmailService(new MailerService())));
            //program.Start();

            var container = CreateContainer();
            var app = container.Resolve<UserManagementApp>();
            app.Start();
        }

        private static WindsorContainer CreateContainer()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<NewsletterFactory>().LifestyleTransient());
            container.Register(Component.For<UserManagementApp>().LifestyleTransient());
            container.Register(Component.For<DisplayUserVisitor>().LifestyleTransient());
            container.Register(Component.For<SendMailUserVisitor>().LifestyleTransient());
            container.Register(Component.For<IEmployeeStorage>().Instance(EmployeeStorage.Instance));//.ImplementedBy<EmployeeStorage>().LifestyleSingleton());
            container.Register(Component.For<IEmailService>().ImplementedBy<EmailService>().LifestyleTransient().Interceptors<LoggingInterceptor>());
            container.Register(Component.For<LoggingInterceptor>().LifestyleTransient());

            return container;
        }
    }
}