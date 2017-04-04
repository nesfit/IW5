using UserManagement.ConsoleApp.Composite;

namespace UserManagement.ConsoleApp.Repository
{
    public sealed class EmployeeStorage
    {
        public UserGroupComposite Developers { get; }
        public UserGroupComposite Employees { get; }
        public UserGroupComposite Designers { get; }
        public UserGroupComposite PythonTeam { get; }
        public UserGroupComposite DotNetTeam { get; }
        
        public EmployeeStorage()
        {
            var roman = new UserComponent { Name = "Roman", Email = "roman@super-company.com" };
            var tibor = new UserComponent { Name = "Tibor", Email = "tibor@super-company.com" };
            var martin = new UserComponent { Name = "Martin", Email = "martin@super-company.com" };
            var adam = new UserComponent { Name = "Adam", Email = "adam@super-company.com" };
            var joe = new UserComponent { Name = "Joe", Email = "joe@super-company.com" };
            var jake = new UserComponent { Name = "Jake", Email = "jake@super-company.com" };
            var emily = new UserComponent { Name = "Emily", Email = "emily@super-company.com" };
            var sophia = new UserComponent { Name = "Sophia", Email = "sophia@super-company.com" };
            var brian = new UserComponent { Name = "Brian", Email = "brian@super-company.com" };
            var bob = new UserComponent { Name = "Bob", Email = "bob@super-company.com" };

            DotNetTeam = new UserGroupComposite
            {
                Name = ".NET team",
                Members = { roman, tibor, martin, adam }
            };

            PythonTeam = new UserGroupComposite
            {
                Name = "Python team",
                Members = { emily, sophia, brian, bob }
            };

            Designers = new UserGroupComposite
            {
                Name = "Design team",
                Members = { joe, jake }
            };

            Developers = new UserGroupComposite
            {
                Name = "Developers",
                Members = { DotNetTeam, PythonTeam }
            };

            Employees = new UserGroupComposite
            {
                Name = "Developers",
                Members = { Developers, Designers }
            };
        }
    }
}