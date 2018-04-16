using System;
using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Composite
{
    public class ContactGroupComposite : IContactComponent
    {
        public string Name { get; set; }
        public IList<IContactComponent> Members { get; } = new List<IContactComponent>();
    }
}