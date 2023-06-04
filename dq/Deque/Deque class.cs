using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Deque<T>
    {
        private List<T> elements;

        public Deque()
        {
            elements = new List<T>();
        }

        public void AddFront(T item)
        {
            elements.Add(item);
        }

        public void AddTail(T item)
        {
            elements.Insert(0, item);
        }

        public T RemoveFront()
        {
            if (elements.Count > 0)
            {
                T result = elements[elements.Count - 1];
                elements.RemoveAt(elements.Count - 1);
                return result;
            }
            return default;
        }

        public T RemoveTail()
        {
            if (elements.Count > 0)
            {
                T result = elements[0];
                elements.RemoveAt(0);
                return result;
            }
            return default;
        }

        public int Count()
        {
            return elements.Count;
        }
    }
}