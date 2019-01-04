using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using UserManagement.Composite;
using UserManagement.Decorator;
using UserManagement.Factory;
using UserManagement.Models;
using UserManagement.Services;
using UserManagement.Singleton;
using UserManagement.Visitor;

namespace UserManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            //var userManagementApp = new UserManagementApp(new ServiceLocator.ServiceLocator().GetEmployeeStorage(), new NewsletterFactory(), new SendMailUserVisitor(ServiceLocator.ServiceLocator.GetEmailService()), new DisplayContactVisitor());

            var container = CreateContainer();
            var userManagementApp = container.Resolve<UserManagementApp>();
            userManagementApp.Run();
        }

        private static WindsorContainer CreateContainer()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<UserManagementApp>().LifestyleTransient());
            container.Register(Component.For<IEmployeeStorage>().ImplementedBy<EmployeeStorage>().LifestyleSingleton());
            container.Register(Component.For<NewsletterFactory>().LifestyleTransient());
            container.Register(Component.For<DisplayContactVisitor>().LifestyleTransient());//.Interceptors<LoggingInterceptor>());
            container.Register(Component.For<SendMailUserVisitor>().LifestyleTransient());
            container.Register(Component.For<LoggingInterceptor>().LifestyleTransient());
            container.Register(Component.For<IEmailService>().ImplementedBy<EmailService>().Interceptors<LoggingInterceptor>().LifestyleTransient());


            return container;
        }
    }
}