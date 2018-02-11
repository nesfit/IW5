namespace OOP.Animals
{
    public class Cat : UnknownCat
    {
        public Cat(string name) : base()
        {
            this.Name = name;
        }
        public Cat(string name, int livesLeft) : this(name)
        {
            this.LivesLeft = livesLeft;
        }
    }
}