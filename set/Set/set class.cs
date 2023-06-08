using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;

namespace AlgorithmsDataStructures
{

    // наследуйте этот класс от HashTable
    // или расширьте его методами из HashTable
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
