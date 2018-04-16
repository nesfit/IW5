using UserManagement.Composite;

namespace UserManagement.Singleton
{
    public class EmployeeStorage : IEmployeeStorage
    {
        public ContactGroupComposite Developers { get; }
        public ContactGroupComposite Employees { get; }
        public ContactGroupComposite Designers { get; }
        public ContactGroupComposite PythonTeam { get; }
        public ContactGroupComposite DotNetTeam { get; }

        public static IEmployeeStorage Instance { get; } = new EmployeeStorage();

        private EmployeeStorage()
        {
            var roman = new ContactComponent { Firstname = "Roman", EmailAddress = "roman@super-company.com" };
            var tibor = new ContactComponent { Firstname = "Tibor", EmailAddress = "tibor@super-company.com" };
            var martin = new ContactComponent { Firstname = "Martin", EmailAddress = "martin@super-company.com" };
            var adam = new ContactComponent { Firstname = "Adam", EmailAddress = "adam@super-company.com" };
            var joe = new ContactComponent { Firstname = "Joe", EmailAddress = "joe@super-company.com" };
            var jake = new ContactComponent { Firstname = "Jake", EmailAddress = "jake@super-company.com" };
            var emily = new ContactComponent { Firstname = "Emily", EmailAddress = "emily@super-company.com" };
            var sophia = new ContactComponent { Firstname = "Sophia", EmailAddress = "sophia@super-company.com" };
            var brian = new ContactComponent { Firstname = "Brian", EmailAddress = "brian@super-company.com" };
            var bob = new ContactComponent { Firstname = "Bob", EmailAddress = "bob@super-company.com" };

            DotNetTeam = new ContactGroupComposite
            {
                Name = ".NET team",
                Members = { roman, tibor, martin, adam }
            };

            PythonTeam = new ContactGroupComposite
            {
                Name = "Python team",
                Members = { emily, sophia, brian, bob }
            };

            Designers = new ContactGroupComposite
            {
                Name = "Design team",
                Members = { joe, jake }
            };

            Developers = new ContactGroupComposite
            {
                Name = "Developers",
                Members = { DotNetTeam, PythonTeam }
            };

            Employees = new ContactGroupComposite
            {
                Name = "Employees",
                Members = { Developers, Designers }
            };
        }
    }
}