using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Node<T>
    {
        public T value;
        public Node<T> next;
        public Node<T> prev;

        public Node() { }

        public Node(T _value) { value = _value; }
    }

    public class DummyNode<T> : Node<T>
    {
        public DummyNode()
        {
            next = null;
            prev = null;
        }
    }

    public class Deque<T>
    {
        DummyNode<T> edge;

        public Deque()
        {
            edge = new DummyNode<T>();
            edge.next = edge;
            edge.prev = edge;
        }

        public void AddFront(T item)
        {
            Node<T> node = new Node<T>(item);
            
            node.next = edge.next;
            edge.next.prev = node;
            edge.next = node;
            node.prev = edge;
        }

        public void AddTail(T item)
        {
            Node<T> node = new Node<T>(item);

            node.prev = edge.prev;
            edge.prev.next = node;
            edge.prev = node;
            node.next = edge;
        }

        public T RemoveFront()
        {
            if (edge.next is DummyNode<T> ) return default(T);

            T value = edge.next.value;
            edge.next.next.prev = edge.next;
            edge.next = edge.next.next;
            return value;
        }

        public T RemoveTail()
        {
            if (edge.prev is DummyNode<T>) return default(T);

            T value = edge.prev.value;
            edge.prev.prev.next = edge.prev;
            edge.prev = edge.prev.prev;
            return value;
        }

        public int Size()
        {
            Node<T> node = edge.next;
            int count = 0;

            while (!(node is DummyNode<T>))
            {
                ++count;
                node = node.next;
            }
            return count;
        }
    }
}