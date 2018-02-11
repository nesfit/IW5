using System.Collections.Generic;

namespace OOP.Generics
{
    public class Stack<T>
    {
        private int _position;
        private readonly T[] _data = new T[100];

        public void Push(T obj)
        {
            this._data[this._position++] = obj;
        }

        public T Pop()
        {
            return this._data[--this._position];
        }
    }
}