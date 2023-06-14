using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsDataStructures
{
    public class NativeCache<T>
    {
        public int size;
        public const int STEP = 3;
        public String[] slots;
        public T[] values;
        public int[] hits;

        public NativeCache(int _size)
        {
            size = _size;
            slots = new string[_size];
            values = new T[_size];
            hits = new int[_size];
        }

        public int CalculateHash(string value)
        {
            return Math.Abs(value.GetHashCode()) % size;
        }

        public bool IsKey(string key)
        {
            return slots.Contains(key);
        }

        public int FindLeastWanted() 
        {
            int minIndex = 0;
            int minValue = hits[0];

            for (int valueIndex = 0; valueIndex < size; ++valueIndex)
            {
                if (hits[valueIndex] < minValue)
                {
                    minIndex = valueIndex;
                    minValue = hits[valueIndex];
                }
            }

            return minIndex;
        }

        public void Remove(int index)
        {
            hits[index] = 0;
            values[index] = default;
            slots[index] = null;
        }
        
        public void Add(string key, T value)
        {
            int index = CalculateHash(key);
            bool unableToInsert = true;

            for (int visitedValues = 0; visitedValues < size; ++visitedValues)
            {
                if (slots[index] is null || key.Equals(slots[index]))
                {
                    slots[index] = key;
                    values[index] = value;
                    unableToInsert = false;
                    break;
                }

                index = (index + STEP) % size;
            }

            if (unableToInsert)
            {
                index = FindLeastWanted();
                Remove(index);
                Add(key, value);
            }
        }

        public T Get(string key)
        {
            int index = CalculateHash(key);

            if (index > size) return default;

            for (int visitedValues = 0; visitedValues < size; ++visitedValues)
            {
                if (key.Equals(slots[index]))
                {
                    ++hits[index];
                    return values[index];
                }

                index = (index + STEP) % size;
            }
            return default;
        }
    }
}
