namespace IW5_OOP.Pets
{
    public interface INickName
    {
        string NickName { get; }
    }

    public interface IDrawableNickname : INickName, IDraw
    {
        
    }
}