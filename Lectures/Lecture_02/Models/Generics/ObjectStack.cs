namespace OOP.Generics
{
    public class ObjectStack
    {
        private int _position;
        private readonly object[] _data = new object[10];
        public void Push(object obj)
        {
            this._data[this._position++] = obj;
        }
        public object Pop()
        {
            return this._data[--this._position];
        }
    }
}