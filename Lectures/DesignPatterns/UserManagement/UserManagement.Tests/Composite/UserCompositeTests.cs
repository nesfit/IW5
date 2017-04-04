using UserManagement.ConsoleApp.Composite;
using Xunit;

namespace UserManagement.Tests.Composite
{
    public class UserCompositeTests
    {
        [Fact]
        public void IncreaseSalary_DirectPerson_SalaryIncreased ()
        {
            var salary = 80000;
            var bob = new UserComponent { Name = "Bob", Email = "bob@super-company.com", Salary = salary };
            
            bob.IncreaseSalary(10);

            var increasedSalary = salary * (decimal)1.1;
            Assert.Equal(increasedSalary, bob.Salary);
        }

        [Fact]
        public void IncreaseSalary_Group_SalaryIncreased()
        {
            var salary = 80000;
            var bob = new UserComponent { Name = "Bob", Email = "bob@super-company.com", Salary = salary };
            var developers = new UserGroupComposite
            {
                Name = "Developers",
                Members = { bob }
            };

            developers.IncreaseSalary(10);

            var increasedSalary = salary * (decimal)1.1;
            Assert.Equal(increasedSalary, bob.Salary);
        }
    }
}