using System;
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
            }
            else
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
            }
            else if (ParentNode.Children is null)
            {
                ParentNode.Children = new List<SimpleTreeNode<T>>()
                {
                    NewChild
                };
            }
            else
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
                }
                else
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
            OriginalNode.Parent?.Children.Remove(OriginalNode);
            OriginalNode.Parent = NewParent;
            if (NewParent.Children is null)
            {
                NewParent.Children = new List<SimpleTreeNode<T>>()
                {
                    OriginalNode
                };
            }
            else
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
                if (children[i].Children is null || children[i].Children.Count == 0)
                {
                    ++leavesAmount;
                }
            }

            return leavesAmount;
        }

        public void RankNodes()
        {
            Root.NodeLevel = 0;
            RankNodesChildren(Root);
        }

        private void RankNodesChildren(SimpleTreeNode<T> parent)
        {
            if (!(parent.Children is null))
            {
                for (int i = 0; i < parent.Children.Count; ++i)
                {
                    parent.Children[i].NodeLevel = parent.NodeLevel + 1;
                    RankNodesChildren(parent.Children[i]);
                }
            }
        }

        public List<T> EvenTrees()
        {
            Dictionary<SimpleTreeNode<T>, int> nodesBonds = new Dictionary<SimpleTreeNode<T>, int>();
            List<T> result = new List<T>();

            if (Root != null)
            {
                PostOrderDepthTraversal(Root, nodesBonds);
            }

            if (nodesBonds.Count % 2 == 0 && nodesBonds.Count != 0)
            {
                foreach (var bond in nodesBonds)
                {
                    if (bond.Key.Parent != null && bond.Value % 2 == 0)
                    {
                        result.Add(bond.Key.Parent.NodeValue);
                        result.Add(bond.Key.NodeValue);
                    }
                }
            }

            return result;
        }

        private void PostOrderDepthTraversal(SimpleTreeNode<T> node, Dictionary<SimpleTreeNode<T>, int> weights)
        {
            if (node.Children != null)
            {
                foreach (SimpleTreeNode<T> child in node.Children)
                {
                    PostOrderDepthTraversal(child, weights);
                }

                weights.Add(node, 1 + weights.Sum(current =>
                {
                    if (node.Children.Contains(current.Key))
                    {
                        return current.Value;
                    }
                    else
                    {
                        return 0;
                    }
                }));
            } else
            {
                weights.Add(node, 1);
            }
        }
    }
}