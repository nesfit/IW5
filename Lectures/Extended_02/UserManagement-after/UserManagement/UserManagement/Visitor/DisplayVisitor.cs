using System;
using UserManagement.Composite;

namespace UserManagement.Visitor
{
    public class DisplayContactVisitor
    {
        public void Print(IContactComponent contactComponent)
        {
            Print(contactComponent, 0);
        }

        private void Print(IContactComponent contactComponent, int indent)
        {
            switch (contactComponent)
            {
                case ContactComponent contact:
                    Print(contact, indent);
                    break;
                case ContactGroupComposite contactGroup:
                    Print(contactGroup, indent);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private void Print(ContactComponent contactComponent, int indent)
        {
            PrintIndentedText($"User: {contactComponent.Name}", indent);
        }

        private void Print(ContactGroupComposite contactGroupComposite, int indent)
        {
            PrintIndentedText($"Group: {contactGroupComposite.Name}", indent);
            indent++;
            foreach (var member in contactGroupComposite.Members)
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