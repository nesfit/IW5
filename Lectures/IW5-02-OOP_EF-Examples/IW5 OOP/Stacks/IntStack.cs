namespace IW5_OOP.Stacks
{
    public class IntStack
    {
        private readonly int[] _data = new int[100];
        private int _position;

        public void Push(int number)
        {
            _data[_position++] = number;
        }

        public int Pop()
        {
            return _data[--_position];
        }
    }
}