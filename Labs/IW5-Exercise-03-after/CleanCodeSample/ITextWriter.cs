namespace CleanCodeSample
{
    public interface ITextWriter
    {
        void Write(string message, MessageType type = MessageType.Normal);
    }

    public enum MessageType
    {
        Normal,
        Success,
        Fail
    }
}