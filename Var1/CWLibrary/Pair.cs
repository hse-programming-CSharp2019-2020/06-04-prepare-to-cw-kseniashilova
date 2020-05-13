using System;
using System.Runtime.CompilerServices;

namespace CWLibrary
{
    [Serializable]
    public class Pair<T,U> : IComparable where T : IComparable
    {
        T item1;
        U item2;

        public T Item1 => item1;
        public U Item2 => item2;
        public Pair(T item1, U item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }

        public int CompareTo(object obj)
        {
            return ((Pair<T, U>)obj).item1.CompareTo(item1);
        }

        public override string ToString()
        {
            return $"Item1: {item1}, Item2: {item2}";
        }
    }
}
