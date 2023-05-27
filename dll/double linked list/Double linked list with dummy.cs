using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsDataStructures
{
    public class DummyNode : Node
    {
        public DummyNode() : base()
        {
            next = null;
            prev = null;
        }
    }

    public class DummyLinkedList
    {
        public DummyNode head;
        public DummyNode tail;

        public DummyLinkedList()
        {
            head = new DummyNode();
            tail = new DummyNode();
            head.next = tail;
            tail.prev = head;
        }

        public void AddInTail(Node _item)
        {
            _item.prev      = tail.prev;
            tail.prev.next  = _item;
            tail.prev       = _item;
            _item.next      = tail;
        }

        public Node Find(int _value)
        {
            Node node = head.next;
            while (node != tail)
            {
                if (node.value == _value) return node; 
                node = node.next;
            }
            return null;
        }

        public List<Node> FindAll(int _value)
        {
            List<Node> nodes = new List<Node>();
            Node node = head.next;
            while (node != tail)
            {
                if (node.value == _value) nodes.Add(node);
                node = node.next;
            }
            return nodes;
        }

        public bool Remove(int _value)
        {
            Node first = head;
            Node second = head.next;
            while (second != tail && second.value != _value)
            {
                second = second.next;
                first = first.next;
            }
            if (second == tail) return false;
            first.next = second.next;
            second.next.prev = first;
            return true;
        }

        public void RemoveAll(int _value)
        {
            Node node = head.next;

            while (node != tail && node.value == _value)
            {
                head.next = head.next.next;
                if (head.next == tail) tail.prev = head;
                else head.next.prev = null;
                node = head.next;
            }

            Node before = null;

            while (node != tail)
            {
                while (node != tail && node.value != _value)
                {
                    before = node;
                    node = node.next;
                }

                if (node == tail) break;

                before.next = node.next;
                node = node.next;
                if (node == tail) tail.prev = before;
                else node.prev = before;
            }
        }

        public void Clear()
        {
            head.next = tail;
            tail.prev = head;
        }

        public int Count()
        {
            Node node = head.next;
            int count = 0;

            while (node != tail)
            {
                ++count;
                node = node.next;
            }
            return count; 
        }

        public void InsertAfter(Node _nodeAfter, Node _nodeToInsert)
        {
            Node node = head.next;

            if (_nodeAfter == null)
            {
                head.next.prev = _nodeToInsert;
                _nodeToInsert.prev = head;
                _nodeToInsert.next = head.next;
                head.next = _nodeToInsert;
            }
            else if (_nodeAfter == tail.next) AddInTail(_nodeToInsert);
            else if (node != tail)
            {
                while (node.next != tail)
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
