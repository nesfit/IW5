using UserManagement.Composite;

namespace UserManagement.Singleton
{
    public interface IEmployeeStorage
    {
        ContactGroupComposite Developers { get; }
        ContactGroupComposite Employees { get; }
        ContactGroupComposite Designers { get; }
        ContactGroupComposite PythonTeam { get; }
        ContactGroupComposite DotNetTeam { get; }
    }
}