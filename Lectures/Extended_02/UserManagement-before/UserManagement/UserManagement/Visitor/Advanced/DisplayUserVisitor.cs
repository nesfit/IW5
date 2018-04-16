using System;
using UserManagement.Composite;

namespace UserManagement.Visitor.Advanced
{
    public class DisplayUserVisitor : UserVisitorBase
    {
        private int indent;
        public void Print(IContactComponent contactComponent)
        {
            indent = 0;
            base.Visit(contactComponent);
        }

        protected override void Visit(ContactComponent contactComponent)
        {
            PrintIndentedText($"User: {contactComponent.Name}");
        }

        protected override void Visit(ContactGroupComposite contactGroupComposite)
        {
            PrintIndentedText($"Group: {contactGroupComposite.Name}");
            indent++;
            foreach (var member in contactGroupComposite.Members)
            {
                Visit(member);
            }
            indent--;
        }

        private void PrintIndentedText(string text)
        {
            var indentTabs = new string('\t', indent);
            Console.WriteLine(indentTabs + text);
        }
    }

}