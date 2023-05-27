using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AlgorithmsDataStructures
{
    public class CircledLinkedList
    {
        public DummyNode edge;
        
        public CircledLinkedList() 
        {
            edge = new DummyNode();
            edge.next = edge;
            edge.prev = edge;
        }

        public void AddInTail(Node _item)
        {
            _item.next = edge;
            edge.prev.next = _item;
            edge.prev = _item;
            _item.prev = edge.prev;
        }

        public Node Find(int _value)
        {
            Node node = edge.next;
            while (node != edge)
            {
                if (node.value == _value) return node;
                node = node.next;
            }
            return null;
        }

        public List<Node> FindAll(int _value)
        {
            List<Node> nodes = new List<Node>();
            Node node = edge.next;
            while (node != edge)
            {
                if (node.value == _value) nodes.Add(node);
                node = node.next;
            }
            return nodes;
        }

        public bool Remove(int _value)
        {
            Node first = edge;
            Node second = edge.next;
            while (second != edge && second.value != _value)
            {
                second = second.next;
                first = first.next;
            }
            if (second == edge) return false;
            first.next = second.next;
            second.next.prev = first;
            return true;
        }

        public void RemoveAll(int _value)
        {
            Node node = edge.next;

            while (node != edge && node.value == _value)
            {
                edge.next = edge.next.next;
                if (edge.next == edge) edge.prev = edge;
                else edge.next.prev = edge;
                node = edge.next;
            }

            Node before = null;

            while (node != edge)
            {
                while (node != edge && node.value != _value)
                {
                    before = node;
                    node = node.next;
                }

                if (node == edge) break;

                before.next = node.next;
                node = node.next;
                if (node == edge) edge.prev = before;
                else node.prev = before;
            }
        }

        public void Clear()
        {
            edge.next = edge;
            edge.prev = edge;
        }

        public int Count()
        {
            Node node = edge.next;
            int count = 0;

            while (node != edge)
            {
                ++count;
                node = node.next;
            }
            return count;
        }

        public void InsertAfter(Node _nodeAfter, Node _nodeToInsert)
        {
            Node node = edge.next;

            if (_nodeAfter == null)
            {
                edge.next.prev = _nodeToInsert;
                _nodeToInsert.prev = edge;
                _nodeToInsert.next = edge.next;
                edge.next = _nodeToInsert;
            }
            else if (_nodeAfter == edge.next) AddInTail(_nodeToInsert);
            else if (node != edge)
            {
                while (node.next != edge)
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
