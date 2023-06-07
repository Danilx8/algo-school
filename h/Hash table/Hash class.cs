using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    public class HashTable
    {
        public int size;
        public int step;
        public string[] slots;

        public HashTable(int sz, int stp)
        {
            size = sz;
            step = stp;
            slots = new string[size];
            for (int i = 0; i < size; i++) slots[i] = null;
        }

        public int HashFun(string value)
        {
           return Math.Abs(value.GetHashCode()) % size;
        }

        public int SeekSlot(string value)
        {
            int index = HashFun(value);
            int visitedAmount = 0;

            while (visitedAmount < size)
            {
                if (slots[index] is null) return index;

                ++visitedAmount;
                index = (index + step) % size;
            }

            return -1;
        }

        public int Put(string value)
        {
            int index = SeekSlot(value);

            if (index != -1)
            {
                slots[index] = value;
                return index;
            }
            return -1;
        }

        public int Find(string value)
        {
            for (int i = 0; i < size; ++i)
            {
                if (slots[i] == value) return i;
            }
            return -1;
        }
    }

}