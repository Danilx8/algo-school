using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Xml.Linq;

namespace AlgorithmsDataStructures
{

    public class Node<T>
    {
        public T value;
        public Node<T> next, prev;

        public Node(T _value)
        {
            value = _value;
            next = null;
            prev = null;
        }
    }

    public class OrderedList<T>
    {
        public Node<T> head, tail;
        private bool _ascending;

        public OrderedList(bool asc)
        {
            head = null;
            tail = null;
            _ascending = asc;
        }

        public int Compare(T v1, T v2)
        {
            if (typeof(T) == typeof(String))
            {
                string firstNumber = Convert.ToString(v1).Trim();
                string secondNumber = Convert.ToString(v2).Trim();
                return firstNumber.CompareTo(secondNumber);
            }
            else
            {
                int firstNumber = Convert.ToInt32(v1);
                int secondNumber = Convert.ToInt32(v2);
                return firstNumber.CompareTo(secondNumber);
            }
        }

        public void Add(T value)
        {
            Node<T> node = head;
            Node<T> insertNode = new Node<T>(value);

            while (node != null)
            {


                node = node.next;
            }
        }

        public Node<T> Find(T val)
        {
            return null; // здесь будет ваш код
        }

        public void Delete(T val)
        {
            // здесь будет ваш код
        }

        public void Clear(bool asc)
        {
            _ascending = asc;
            // здесь будет ваш код
        }

        public int Count()
        {
            return 0; // здесь будет ваш код подсчёта количества элементов в списке
        }

        List<Node<T>> GetAll() // выдать все элементы упорядоченного 
                               // списка в виде стандартного списка
        {
            List<Node<T>> r = new List<Node<T>>();
            Node<T> node = head;
            while (node != null)
            {
                r.Add(node);
                node = node.next;
            }
            return r;
        }

        void Insert(Node<T> compareNode, Node<T> insertNode) 
        {
            switch (Compare(compareNode.value, insertNode.value))
            {
                case -1:

                    break;
                case 0:

                    break;
                case 1: 

                    break;
            }
        }
    }

}