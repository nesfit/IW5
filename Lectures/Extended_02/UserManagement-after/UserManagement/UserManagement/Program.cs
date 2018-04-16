using System;
using System.Collections.Generic;
using UserManagement.Factory;
using UserManagement.Models;

namespace UserManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var contacts = new List<Contact>()
            {
                new Contact { Firstname = "Roman", EmailAddress = "roman@super-company.com" },
                new Contact { Firstname = "Tibor", EmailAddress = "tibor@super-company.com" },
                new Contact { Firstname = "Martin", EmailAddress = "martin@super-company.com" },
                new Contact { Firstname = "Adam", EmailAddress = "adam@super-company.com" },
                new Contact { Firstname = "Joe", EmailAddress = "joe@super-company.com" },
                new Contact { Firstname = "Jake", EmailAddress = "jake@super-company.com" },
                new Contact { Firstname = "Emily", EmailAddress = "emily@super-company.com" },
                new Contact { Firstname = "Sophia", EmailAddress = "sophia@super-company.com" },
                new Contact { Firstname = "Brian", EmailAddress = "brian@super-company.com" },
                new Contact { Firstname = "Bob", EmailAddress = "bob@super-company.com" },
            };

            var newsletterFactory = new NewsletterFactory();
            var newsletter = newsletterFactory.CreateNewsletter();

            foreach (var contact in contacts)
            {
                contact.SendMail(newsletter);
            }
        }
    }
}