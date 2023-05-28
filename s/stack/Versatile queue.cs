using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsDataStructures
{
    public class VersatileDynArray
    {
        const float SHRINK_FRACTION = 0.5F;
        public object[] array;
        public int count;
        public int capacity;

        public VersatileDynArray()
        {
            count = 0;
            MakeArray(16);
        }

        public void MakeArray(int new_capacity)
        {
            object[] newArray = new object[new_capacity];
            int transferringCapacity = Math.Min(capacity, new_capacity);
            if (!(array is null) && array != Array.Empty<object>()) Array.Copy(array, newArray, transferringCapacity);
            capacity = new_capacity;
            array = newArray;
        }

        public object GetItem(int index)
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException("Index out of range.");
            return array[index];
        }

        public void Append(object itm)
        {
            if (count + 1 > capacity) MakeArray(capacity * 2);
            array[count++] = itm;
        }

        public void Insert(object itm, int index)
        {
            if (index < 0 || index > count) throw new IndexOutOfRangeException("Index out of range.");
            if (++count > capacity) MakeArray(capacity * 2);

            for (int i = count - 2; i >= index; --i)
            {
                array[i + 1] = array[i];
            }
            array[index] = itm;
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException("Index out of range.");
            for (int i = index; i < count - 1; ++i)
            {
                array[i] = array[i + 1];
            }
            array[count - 1] = default;

            if (--count < SHRINK_FRACTION * capacity) MakeArray(Math.Max((int)(capacity / 1.5), 16));
        }
    }


    public class VersatileQueue
    {
        VersatileDynArray objects;

        public VersatileQueue()
        {
            objects   = new VersatileDynArray();
        }

        public int Size()
        {
            return objects.count;
        }

        public object Pop()
        {
            object result = objects.GetItem(objects.count - 1);
            objects.Remove(objects.count - 1);
            return result;
        }

        public void Push(object val)
        {
            objects.Insert(val, 0);
        }

        public object Peek()
        {
            return objects.GetItem(objects.count - 1);
        }
    }
}
