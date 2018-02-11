namespace OOP.Animals
{
    public abstract class Animal
    {
        protected string Name = "Jane Doe";
        public bool  IsAlive { get; protected set; } = true;
        public abstract void DrawIt();
    }
}