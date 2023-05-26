using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Policy;

namespace AlgorithmsDataStructures
{
    public class Node<T>
    {
        public T value;
        public Node<T> next;
        public Node<T> prev;

        public Node(T _value) { value = _value; }
    }

    public class Stack<T>
    {
        public Node<T> head;
        public Node<T> tail;

        public Stack()
        {
            head = null;
            tail = null;
        }

        public int Size()
        {
            Node<T> node = head;
            int count = 0;

            while(node != null)
            {
                ++count;
                node = node.next;
            }
            return count;
        }

        public T Pop()
        {
            if (tail == null) return default(T); 
            else if (tail == head)
            {
                T value = tail.value;
                head = null;
                tail = null;
                return value;
            }
            else
            {
                T value = tail.value;
                tail.prev.next = null;
                tail = tail.prev;
                return value;
            }
        }

        public void Push(T val)
        {
            Node<T> newNode = new Node<T>(val);

            if (head == null)
            {
                head = newNode;
                head.next = null;
                head.prev = null;
            }
            else
            {
                tail.next = newNode;
                newNode.prev = tail;
            }
            tail = newNode;
        }

        public T Peek()
        {
            if (head == null) return default(T); 
            else return tail.value;
        }
    }

}