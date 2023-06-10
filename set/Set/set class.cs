using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;

namespace AlgorithmsDataStructures
{
    public class HashNode<T>
    {
        public T value;
        public bool deleted = false;

        public HashNode(T _value)
        {
            value = _value;
        }
    }

    public class PowerSet<T>
    {
        private int size;
        private int step;
        private HashNode<T>[] slots;
        private int count;

        public PowerSet()
        {
            size = 20_000;
            step = 3;
            slots = new HashNode<T>[size];
            for (int i = 0; i < size; ++i)
            {
                HashNode<T> node = new HashNode<T>(default);
                slots[i] = node;
            }
        }

        private int HashFun(T value)
        {
            return Math.Abs(value.GetHashCode()) % size;
        }


        private int SeekSlot(T value)
        {
            int index = HashFun(value);
            int visitedAmount = 0;

            while (visitedAmount < size)
            {
                if (slots[index].value == null) return index;

                ++visitedAmount;
                index = (index + step) % size;
            }

            return -1;
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

        public int Put(T value)
        {
            if (Find(value) != -1) return -1;

            int index = SeekSlot(value);

            if (index != -1)
            {
                slots[index].value = value;
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
                if ((slots[index].value == null || !(slots[index].value.Equals(value))) && !(slots[index].deleted)) return -1;
                if (slots[index].value.Equals(value)) return index;
                index = (index + step) % size;
                ++checkedElements;
            }

            return -1;
        }

        public bool Remove(T value)
        {
            int index = Find(value);
            
            if (index == -1) return false;
            slots[index].value = default;
            slots[index].deleted = true;
            --count;
            return true;
        }

        public PowerSet<T> Intersection(PowerSet<T> set2)
        {
            PowerSet<T> resultSet = new PowerSet<T>();

            for (int i = 0; i < size; ++i)
            {
                if (slots[i].value != null && slots[i].value.Equals(set2.slots[i].value)) resultSet.Put(slots[i].value);
            }

            return resultSet;
        }

        public PowerSet<T> Union(PowerSet<T> set2)
        {
            PowerSet<T> resultSet = new PowerSet<T>();

            for (int i = 0; i < size; ++i)
            {
                if (slots[i].value != null) resultSet.Put(slots[i].value);
                if (set2.slots[i].value != null) resultSet.Put(set2.slots[i].value);
            }

            return resultSet;
        }

        public PowerSet<T> Difference(PowerSet<T> set2)
        {
            PowerSet<T> resultSet = new PowerSet<T>();
            List<T> firstValues = new List<T>();

            for (int i = 0; i < size; ++i)
            {
                if (slots[i].value != null) firstValues.Add(slots[i].value);
            }

            for (int j = 0; j < size; ++j)
            {
                if (set2.slots[j].value != null && firstValues.Contains(set2.slots[j].value)) firstValues.Remove(set2.slots[j].value);
            }

            foreach(T value in firstValues)
            {
                resultSet.Put(value);
            }

            return resultSet;
        }

        public bool IsSubset(PowerSet<T> set2)
        {
            foreach (HashNode<T> node in set2.slots)
            {
                if (node.value != null && Find(node.value) == -1) return false;
            }

            return true;
        }
    }
}
