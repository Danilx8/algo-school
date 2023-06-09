﻿using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    public class HashTable<T>
    {
        public int size;
        public int step;
        public T[] slots;
        public int count = 0;

        public HashTable() { }

        public HashTable(int sz, int stp)
        {
            size = sz;
            step = stp;
            slots = new T[size];
            for (int i = 0; i < size; i++) slots[i] = default;
        }

        public int HashFun(T value)
        {
           return Math.Abs(value.GetHashCode()) % size;
        }

        public int SeekSlot(T value)
        {
            int index = HashFun(value);
            int visitedAmount = 0;

            while (visitedAmount < size)
            {
                if (slots[index] == null) return index;

                ++visitedAmount;
                index = (index + step) % size;
            }

            return -1;
        }

        public int Put(T value)
        {
            if (Find(value) != -1) return -1;

            int index = SeekSlot(value);

            if (index != -1)
            {
                slots[index] = value;
                ++count;
                return index;
            }
            return -1;
        }

        public int Find(T value)
        {
            if (count == 0) return -1;

            int index = HashFun(value);

            int checkedElements = 0;
            while (checkedElements < size)
            {
                if (slots[index] != null && slots[index].Equals(value)) return index;
                while (slots[index] == null && checkedElements < size)
                {
                    index = (index + step) % size;
                    ++checkedElements;
                }
            }

            return -1;
        }
    }
}