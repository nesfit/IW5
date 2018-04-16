using System;
using UserManagement.Composite;

namespace UserManagement.Visitor.Advanced
{
    public abstract class UserVisitorBase
    {
        protected virtual void Visit(IContactComponent contactComponent)
        {
            switch (contactComponent)
            {
                case ContactComponent component:
                    Visit(component);
                    break;
                case ContactGroupComposite composite:
                    Visit(composite);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        protected abstract void Visit(ContactComponent contactComponent);
        protected abstract void Visit(ContactGroupComposite contactGroupComposite);
    }
}