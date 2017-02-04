namespace IW5_OOP.Generics
{
    public class CommonStack<T>
    {
        private readonly T[] _data = new T[100];
        private int _position;

        public void Push(T obj)
        {
            _data[_position++] = obj;
        }

        public T Pop()
        {
            return _data[--_position];
        }
    }
}