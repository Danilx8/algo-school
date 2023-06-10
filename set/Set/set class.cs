using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;

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

    public class PowerSet<T>: HashTable<T>
    {
        public PowerSet()
        {
            size = 20_000;
            step = 3;
            slots = new T[size];
        }

        public int Size()
        {
            return count;
        }

        public bool Get(T value)
        {
            if (Find(value) != -1) return true;
            return false;
        }

        public bool Remove(T value)
        {
            int index = Find(value);
            
            if (index == -1) return false;
            slots[index] = default;
            --count;
            return true;
        }

        public PowerSet<T> Intersection(PowerSet<T> set2)
        {
            PowerSet<T> resultSet = new PowerSet<T>();

            for (int i = 0; i < size; ++i)
            {
                if (slots[i] != null && slots[i].Equals(set2.slots[i])) resultSet.Put(slots[i]);
            }

            return resultSet;
        }

        public PowerSet<T> Union(PowerSet<T> set2)
        {
            PowerSet<T> resultSet = new PowerSet<T>();

            for (int i = 0; i < size; ++i)
            {
                if (slots[i] != null) resultSet.Put(slots[i]);
                if (set2.slots[i] != null) resultSet.Put(set2.slots[i]);
            }

            return resultSet;
        }

        public PowerSet<T> Difference(PowerSet<T> set2)
        {
            PowerSet<T> resultSet = new PowerSet<T>();
            List<T> firstValues = new List<T>();

            for (int i = 0; i < size; ++i)
            {
                if (slots[i] != null) firstValues.Add(slots[i]);
            }

            for (int j = 0; j < size; ++j)
            {
                if (set2.slots[j] != null && firstValues.Contains(set2.slots[j])) firstValues.Remove(set2.slots[j]);
            }

            foreach(T value in firstValues)
            {
                resultSet.Put(value);
            }

            return resultSet;
        }

        public bool IsSubset(PowerSet<T> set2)
        {
            foreach (T value in set2.slots)
            {
                if (value != null && Find(value) == -1) return false;
            }

            return true;
        }
    }
}
