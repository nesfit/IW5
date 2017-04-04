using System;
using UserManagement.ConsoleApp.Composite;

namespace UserManagement.ConsoleApp.Visitor
{
    public class DisplayUserVisitor
    {
        public void Print(IUserComponent userComponent)
        {
            Print(userComponent, 0);
        }

        private void Print(IUserComponent userComponent, int indent)
        {
            if (userComponent is UserComponent)
            {
                Print((UserComponent)userComponent, indent);
            }
            else if (userComponent is UserGroupComposite)
            {
                Print((UserGroupComposite)userComponent, indent);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private void Print(UserComponent userComponent, int indent)
        {
            var formattableString = $"User: {userComponent.Name}";
            PrintIndentedText(formattableString, indent);
        }

        private void Print(UserGroupComposite userGroupComposite, int indent)
        {
            PrintIndentedText($"Group: {userGroupComposite.Name}", indent);
            indent++;
            foreach (var member in userGroupComposite.Members)
            {
                Print(member, indent);
            }
        }

        private void PrintIndentedText(string text, int indent)
        {
            var indentTabs = new string('\t', indent);
            Console.WriteLine(indentTabs + text);
        }
    }
}