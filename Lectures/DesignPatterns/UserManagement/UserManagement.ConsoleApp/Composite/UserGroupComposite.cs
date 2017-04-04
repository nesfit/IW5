using System.Collections.Generic;

namespace UserManagement.ConsoleApp.Composite
{
    public class UserGroupComposite : IUserComponent
    {
        public string Name { get; set; }
        public IList<IUserComponent> Members { get; } = new List<IUserComponent>();

        public void IncreaseSalary(int percent)
        {
            foreach (var member in Members)
            {
                member.IncreaseSalary(percent);
            }
        }
    }
}