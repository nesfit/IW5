using UserManagement.Decorator;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Composite
{
    public class ContactComponent : IContactComponent
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Name => $"{Firstname} {Lastname}";
        public string EmailAddress { get; set; }
        
    }
}