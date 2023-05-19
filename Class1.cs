using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace AlgorithmsDataStructures
{

    public class Node
    {
        public int value;
        public Node next;
        public Node(int _value) { value = _value; }
    }

    public class LinkedList
    {
        public Node head;
        public Node tail;

        public LinkedList()
        {
            head = null;
            tail = null;
        }

        public void AddInTail(Node _item)
        {
            if (head == null) head = _item;
            else tail.next = _item;
            tail = _item;
        }

        public Node Find(int _value)
        {
            Node node = head;
            while (node != null)
            {
                if (node.value == _value) return node;
                node = node.next;
            }
            return null;
        }

        public List<Node> FindAll(int _value)
        {
            List<Node> nodes = new List<Node>();
            Node node = head;
            
            while (node.next != null)
            {
                if (node.value == _value) nodes.Add(node);
                node = node.next;
            }
            return nodes;
        }

        public bool Remove(int _value)
        {
            Node node = head;
            while (node.next != null)
            {
                if (node.next.value == _value)
                {
                    node.next = node.next.next;
                    return true;
                }
                node = node.next;
            }
            return false;
        }

        public void RemoveAll(int _value)
        {
            Node node = head;
            while (node.next != null)
            {
                if (node.next.value == _value) node.next = node.next.next;
                node = node.next;
            }
        }

        public void Clear()
        {
            head = null;
            tail = null;
        }

        public int Count()
        {
            Node node = head;
            int count = 0;

            if (node != null)
            {
                ++count;
                while (node.next != null)
                {
                    ++count;
                    node = node.next;
                }
            }
            return count; 
        }

        public void InsertAfter(Node _nodeAfter, Node _nodeToInsert)
        {
            Node node = head;

            if (tail == _nodeAfter)
            {
                tail.next = _nodeToInsert;
                tail = _nodeToInsert;
            }
            else
            {
                while (node.next != null)
                {
                    if (node == _nodeAfter)
                    {
                        _nodeToInsert.next = node.next.next;
                        node.next = _nodeToInsert;
                        break;
                    }
                    node = node.next;
                }
            }
        }

    }
}