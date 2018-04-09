namespace Contacts.DAL.Entities
{
    public class ContactEntity : IEntity
    {
        public string Firstname { get; set; }
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
    }
}