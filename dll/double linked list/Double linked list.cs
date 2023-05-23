using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    public class Node
    {
        public int value;
        public Node next, prev;

        public Node(int _value)
        {
            value = _value;
            next = null;
            prev = null;
        }
    }

    public class LinkedList2
    {
        public Node head;
        public Node tail;

        public LinkedList2()
        {
            head = null;
            tail = null;
        }

        public void AddInTail(Node _item)
        {
            if (head == null)
            {
                head = _item;
                head.next = null;
                head.prev = null;
            }
            else
            {
                tail.next = _item;
                _item.prev = tail;
            }
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
            while (node != null)
            {
                if (node.value == _value) nodes.Add(node);
                node = node.next;
            }
            return nodes;
        }

        public bool Remove(int _value)
        {
            if (head == null) return false;

            if (head.value == _value)
            {
                if (head == tail) tail = tail.next;
                head = head.next;
                return true;
            }

            if (tail.value == _value)
            {
                Node subNode = head;
                while (subNode.next.next != null) subNode = subNode.next;
                subNode.next = null;
                tail = subNode;
                return true;
            }

            Node first = head;
            Node second = head.next;
            while (second != null && second.value != _value)
            {
                second = second.next;
                first = first.next;
            }
            if (second == null) return false;
            first.next = second.next;
            second.next.prev = first;
            return true;
        }

        public void RemoveAll(int _value)
        {
            Node node = head;

            while (node != null && node.value == _value)
            {
                head.prev = null;
                head = head.next;
                node = head;
                if (head == null) tail = null;
            }

            Node before = null;

            while (node != null)
            {
                while (node != null && node.value != _value)
                {
                    before = node;
                    node = node.next;
                }

                if (node == null) break;

                before.next = node.next;
                node = before.next;
                node.prev = before;
                if (before.next == null) tail = before;
            }
        }

        public void Clear()
        {
            Node current = head;
            Node pointer = head.next;
            while (pointer != null)
            {
                current.next = null;
                current.prev = null;
                current = pointer;
                pointer = pointer.next;
            }
            head = null;
            tail = null;
        }

        public int Count()
        {
            int count = 0;
            Node node = head;
            while (node != null)
            {
                ++count;
                node = node.next;
            }
            return count;
        }

        public void InsertAfter(Node _nodeAfter, Node _nodeToInsert)
        {
            Node node = head;

            if (_nodeAfter == null)
            {
                _nodeToInsert.next = head;
                head.prev = _nodeToInsert;
                head = _nodeToInsert;
            }
            else if (tail == _nodeAfter) AddInTail(_nodeToInsert);
            else if (node != null)
            {
                while (node.next != null)
                {
                    if (node == _nodeAfter)
                    {
                        _nodeToInsert.next = node.next;
                        _nodeToInsert.prev = node;
                        node.next.prev = _nodeToInsert;
                        node.next = _nodeToInsert;
                        break;
                    }
                    node = node.next;
                }
            }
        }
    }
}