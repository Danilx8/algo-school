using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class DynArray<T>
    {
        const float SHRINK_FRACTION = 0.5F;
        public T[] array;
        public int count;
        public int capacity;

        public DynArray()
        {
            count = 0;
            MakeArray(16);
        }

        public void MakeArray(int new_capacity)
        {
            T[] newArray = new T[new_capacity];
            int transferringCapacity = Math.Min(capacity, new_capacity);
            if (!(array is null) && array != Array.Empty<T>()) Array.Copy(array, newArray, transferringCapacity);
            capacity = new_capacity;
            array = newArray;
        }

        public T GetItem(int index)
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException("Index out of range.");
            return array[index];
        }

        public void Append(T itm)
        {
            if (count + 1 > capacity) MakeArray(capacity * 2);
            array[count++] = itm;
        }

        public void Insert(T itm, int index)
        {
            if (index < 0 || index > count) throw new IndexOutOfRangeException("Index out of range.");
            if (++count > capacity) MakeArray(capacity * 2);

            for (int i = index + 1; i < count; ++i)
            {
                array[i] = array[i - 1];
            }
            array[index] = itm;
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException("Index out of range.");
            for (int i = index; i < count; ++i)
            {
                array[i] = array[i + 1];
            }

            if (--count < SHRINK_FRACTION * capacity) MakeArray(Math.Max((int)(capacity / 3 * 2), 16));
        }
    }
}
