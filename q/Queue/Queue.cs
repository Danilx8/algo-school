using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Queue<T>
    {
        int headIndex;
        List<T> values;

        public Queue()
        {
            headIndex = 0;
            values = new List<T>();
        }

        public void Enqueue(T item)
        {
            values.Insert(0, item);
            ++headIndex;
        }

        public T Dequeue()
        {
            if (headIndex == 0) return default(T);
            T requiredValue = values[headIndex - 1];
            values.RemoveAt(headIndex - 1);
            --headIndex;
            return (requiredValue);
        }

        public int Size()
        {
            return headIndex;
        }
    }
}