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
           return value.GetHashCode() % size;
        }

        public int SeekSlot(string value)
        {
            int index = HashFun(value);
            int visitedAmount = 0;

            while (visitedAmount < size)
            {
                if (slots[index] is null) return index;

                ++visitedAmount;
                index += step;
                if (index > size) index -= size;
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
            int index = SeekSlot(value);

            if (index == -1) return -1;

            if (slots[index] == value) return index;
            return -1;
        }
    }

}