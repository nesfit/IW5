namespace IW5_OOP.Stacks
{
    public class ObjectStack
    {
        private readonly object[] _data = new object[10];
        private int _position;

        public void Push(object obj)
        {
            _data[_position++] = obj;
        }

        public object Pop()
        {
            return _data[--_position];
        }
    }
}