using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures
{

    public class NativeDictionary<T>
    {
        public int size;
        public string[] slots;
        public T[] values;

        public NativeDictionary(int sz)
        {
            size = sz;
            slots = new string[size];
            values = new T[size];
        }

        public int HashFun(string key)
        {
            return Math.Abs(key.GetHashCode()) % size;
        }

        public bool IsKey(string key)
        {
            if (slots.Contains(key)) return true;
            return false;
        }

        public void Put(string key, T value)
        {
            values[HashFun(key)] = value;
        }

        public T Get(string key)
        {
            int index = HashFun(key);
            if (index > size) return default(T);
            return values[index];
        }
    }
}
