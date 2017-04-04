namespace UserManagement.ConsoleApp.Composite
{
    public class UserComponent : IUserComponent
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public decimal Salary { get; set; }

        public void IncreaseSalary(int percent)
        {
            Salary *= 1 + percent/(decimal)100;
        }

    }
}