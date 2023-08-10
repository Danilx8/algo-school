﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures2
{
    public class SimpleTreeNode<T>
    {
        public T NodeValue; // значение в узле
        public SimpleTreeNode<T> Parent; // родитель или null для корня
        public List<SimpleTreeNode<T>> Children; // список дочерних узлов или null
        public int NodeLevel;

        public SimpleTreeNode(T val, SimpleTreeNode<T> parent)
        {
            NodeValue = val;
            Parent = parent;
            Children = null;
        }
    }

    public class SimpleTree<T>
    {
        public SimpleTreeNode<T> Root; // корень, может быть null

        private List<SimpleTreeNode<T>> AccumulateAllChildren(SimpleTreeNode<T> node)
        {
            List<SimpleTreeNode<T>> children = new List<SimpleTreeNode<T>>();
            if (!(node is null || node.Children is null))
            {
                for (int i = 0; i < node.Children.Count; ++i)
                {
                    children.AddRange(AccumulateAllChildren(node.Children[i]));
                }
                children.Add(node);
            } else
            {
                children.Add(node);
            }

            return children;
        }

        public SimpleTree(SimpleTreeNode<T> root)
        {
            Root = root;
        }

        public void AddChild(SimpleTreeNode<T> ParentNode, SimpleTreeNode<T> NewChild)
        {
            NewChild.Parent = ParentNode;
            if (ParentNode is null)
            {
                Root = NewChild;
            } else if (ParentNode.Children is null)
            {
                ParentNode.Children = new List<SimpleTreeNode<T>>()
                {
                    NewChild
                };
            } else
            {
                ParentNode.Children.Add(NewChild);
            }
        }

        public void DeleteNode(SimpleTreeNode<T> NodeToDelete)
        {
            NodeToDelete.Parent.Children.Remove(NodeToDelete);
        }

        public List<SimpleTreeNode<T>> GetAllNodes()
        {
            return AccumulateAllChildren(Root);
        }

        public List<SimpleTreeNode<T>> FindNodesByValue(T val)
        {
            List<SimpleTreeNode<T>> result = new List<SimpleTreeNode<T>>();
            List<SimpleTreeNode<T>> children = AccumulateAllChildren(Root);
            for (int i = children.Count - 1; i >= 0; --i)
            {
                T currentValue;
                if (children[i] is null)
                {
                    currentValue = default;
                } else
                {
                    currentValue = children[i].NodeValue;
                }
                
                if (!(val.Equals(default)) && val.Equals(currentValue))
                {
                    result.Add(children[i]);
                }
            }

            return result;
        }

        public void MoveNode(SimpleTreeNode<T> OriginalNode, SimpleTreeNode<T> NewParent)
        {
            OriginalNode.Parent = NewParent;
            if (NewParent.Children is null)
            {
                NewParent.Children = new List<SimpleTreeNode<T>>()
                {
                    OriginalNode
                };
            } else
            {
                NewParent.Children.Add(OriginalNode);
            }
        }

        public int Count()
        {
            return AccumulateAllChildren(Root).Count;
        }

        public int LeafCount()
        {
            int leavesAmount = 0;
            List<SimpleTreeNode<T>> children = AccumulateAllChildren(Root); 
            for (int i = 0; i < children.Count; ++i)
            {
                if (children[i].Children is null)
                {
                    ++leavesAmount;
                }
            }

            return leavesAmount;
        }

    }

}