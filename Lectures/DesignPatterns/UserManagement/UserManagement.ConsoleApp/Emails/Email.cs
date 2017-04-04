namespace UserManagement.ConsoleApp.Emails
{
    public struct Email
    {
        public string Subject { get; }
        public string Body { get; }

        public Email(string subject, string body)
        {
            Subject = subject;
            Body = body;
        }
    }
}