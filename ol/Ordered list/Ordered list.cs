using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Policy;
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

            if (_ascending) AscendingInsert(node, insertNode);
            else DescendingInsert(node, insertNode);
        }

        public Node<T> Find(T val)
        {
            if (head == null) return default;
            if (_ascending && Compare(head.value, val) > 0) return default;
            if (_ascending && Compare(tail.value, val) < 0) return default;
            if (!_ascending && Compare(head.value, val) < 0) return default;
            if (!_ascending && Compare(tail.value, val) > 0) return default;

            Node<T> node = head;
            while (node != null)
            {
                if (Compare(node.value, val) == 0) return node;
                node = node.next;
            }
            return default;
        }

        public void Delete(T val)
        {
            Node<T> node = Find(val);
            bool isFirst = node == head;
            bool isLast = node == tail;

            if (isFirst && isLast)
            {
                head = tail = null;
            }
            else if (isFirst)
            {
                head = head.next;
                head.prev = null;
            }            
            else if (isLast)
            {
                tail = tail.prev;
                tail.next = null;
            }
            else if (node != default)
            {
                node.prev.next = node.next;
                node.next.prev = node.prev;
                node.next = node.next.next;
            }
        }

        public void Clear(bool asc)
        {
            _ascending = asc;

            Node<T> node = head;

            while (node != null)
            {
                head = head.next;
                node.next = null;
                node = head;

                if (node == null) tail = null;
                else head.prev = null;
            }
        }

        public int Count()
        {
            int length = 0;
            Node<T> node = head;
            
            while (node != null)
            {
                ++length;
                node = node.next;
            }
            return length;
        }

        public List<Node<T>> GetAll()
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

        private void AscendingInsert(Node<T> compareNode, Node<T> insertNode)
        {
            int comparison = 0;
            
            while (compareNode != null)
            {
                comparison = Compare(insertNode.value, compareNode.value);

                if (comparison > 0 && compareNode.next is null)
                {
                    compareNode.next = insertNode;
                    insertNode.prev = compareNode;
                    insertNode.next = null;
                    tail = insertNode;
                    break;
                }
                else if (comparison < 1 && compareNode.prev is null)
                {
                    compareNode.prev = insertNode;
                    insertNode.next = compareNode;
                    insertNode.prev = null;
                    head = insertNode;
                    break;
                } 
                else if (comparison < 1)
                {
                    insertNode.prev = compareNode.prev;
                    compareNode.prev.next = insertNode;
                    compareNode.prev = insertNode;
                    insertNode.next = compareNode;
                    break;
                }
                compareNode = compareNode.next;
            }

            if (insertNode.prev is null) head = insertNode;
            if (insertNode.next is null) tail = insertNode;
        }

        private void DescendingInsert(Node<T> compareNode, Node<T> insertNode)
        {
            int comparison = 0;
            
            while (compareNode != null)
            {
                comparison = Compare(insertNode.value, compareNode.value);

                if (comparison < 0 && compareNode.next is null)
                {
                    compareNode.next = insertNode;
                    insertNode.prev = compareNode;
                    insertNode.next = null;
                    tail = insertNode;
                    break;
                }
                else if (comparison > -1 && compareNode.prev is null)
                {
                    compareNode.prev = insertNode;
                    insertNode.next = compareNode;
                    insertNode.prev = null;
                    head = insertNode;
                    break;
                }
                else if (comparison > -1)
                {
                    insertNode.prev = compareNode.prev;
                    compareNode.prev.next = insertNode;
                    compareNode.prev = insertNode;
                    insertNode.next = compareNode;
                    break;
                }
                compareNode = compareNode.next;
            }

            if (insertNode.prev is null) head = insertNode;
            if (insertNode.next is null) tail = insertNode;
        }
    }
}