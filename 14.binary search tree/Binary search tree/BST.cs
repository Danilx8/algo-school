using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class BSTNode<T>
    {
        public int NodeKey; // ключ узла
        public T NodeValue; // значение в узле
        public BSTNode<T> Parent; // родитель или null для корня
        public BSTNode<T> LeftChild; // левый потомок
        public BSTNode<T> RightChild; // правый потомок	

        public BSTNode(int key, T val, BSTNode<T> parent)
        {
            NodeKey = key;
            NodeValue = val;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }

    public class BSTNode {
        public int NodeKey; // ключ узла
        public int NodeValue; // значение в узле
        public BSTNode Parent; // родитель или null для корня
        public BSTNode LeftChild; // левый потомок
        public BSTNode RightChild; // правый потомок	

        public BSTNode(int key, int val, BSTNode parent)
        {
            NodeKey = key;
            NodeValue = val;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }

    public enum Options
    {
        PREORDER,
        INORDER,
        POSTORDER
    }

    // промежуточный результат поиска
    public class BSTFind<T>
    {
        // null если в дереве вообще нету узлов
        public BSTNode<T> Node;

        // true если узел найден
        public bool NodeHasKey;

        // true, если родительскому узлу надо добавить новый левым
        public bool ToLeft;

        public BSTFind() { Node = null; }
    }

    public class BST<T>
    {
        BSTNode<T> Root; // корень дерева, или null

        public BST(BSTNode<T> node)
        {
            Root = node;
        }

        public BSTFind<T> FindNodeByKey(int key)
        {
            return IterateThroughNodes(Root, key);
        }
        
        private BSTFind<T> IterateThroughNodes(BSTNode<T> node, int key)
        {
            if (node == null)
            {
                return new BSTFind<T>();
            }

            int comparisonResult = key.CompareTo(node.NodeKey);
            
            switch(comparisonResult)
            {
                case -1:
                    if (node.LeftChild == null)
                    {
                        BSTFind<T> nodePlaceholder = new BSTFind<T>();
                        nodePlaceholder.Node = node;
                        nodePlaceholder.NodeHasKey = false;
                        nodePlaceholder.ToLeft = true;
                        return nodePlaceholder;
                    }

                    return IterateThroughNodes(node.LeftChild, key);
                case 0:
                    BSTFind<T> formattedNode = new BSTFind<T>();
                    formattedNode.Node = node;
                    formattedNode.NodeHasKey = true;

                    return formattedNode;
                case 1:
                    if (node.RightChild == null)
                    {
                        BSTFind<T> nodePlaceholder = new BSTFind<T>();
                        nodePlaceholder.Node = node;
                        nodePlaceholder.NodeHasKey = false;
                        nodePlaceholder.ToLeft = false;
                        return nodePlaceholder;
                    }

                    return IterateThroughNodes(node.RightChild, key);
            }
            return new BSTFind<T>();
        }

        public bool AddKeyValue(int key, T val)
        {
            BSTFind<T> searchResult = IterateThroughNodes(Root, key);
            if (searchResult.Node == null)
            {
                Root = new BSTNode<T>(key, val, null);
                return true;
            }

            if (searchResult.NodeHasKey)
            {
                return false; // если ключ уже есть
            }

            if (key < searchResult.Node.NodeKey)
            {
                searchResult.Node.LeftChild = new BSTNode<T>(key, val, searchResult.Node);
            } else
            {
                searchResult.Node.RightChild = new BSTNode<T>(key, val, searchResult.Node);
            }

            return true;
        }

        public BSTNode<T> FinMinMax(BSTNode<T> FromNode, bool FindMax)
        {
            BSTNode<T> node = FromNode;

            if (!FindMax)
            {
                while (node.LeftChild != null)
                {
                    node = node.LeftChild;
                }
            } else
            {
                while (node.RightChild != null)
                {
                    node = node.RightChild;
                }
            }
            return node;
        }

        public bool DeleteNodeByKey(int key)
        {
            BSTFind<T> searchResult = FindNodeByKey(key);

            if (!searchResult.NodeHasKey)
            {
                return false; // если узел не найден
            }

            BSTNode<T> ancestorNode = null;
            if (searchResult.Node.RightChild != null)
            {
                ancestorNode = searchResult.Node.RightChild;
                while (ancestorNode.LeftChild != null)
                {
                    ancestorNode = ancestorNode.LeftChild;
                }
            }
            else if (searchResult.Node.LeftChild != null)
            {
                ancestorNode = searchResult.Node.LeftChild;
            } else //удаление листа
            {
                AttachNewChildToParent(searchResult.Node, ancestorNode, searchResult.Node.Parent);
                return true;
            }

            ancestorNode.Parent.LeftChild = ancestorNode.RightChild;
            ancestorNode.Parent = searchResult.Node.Parent;

            if (ancestorNode != searchResult.Node.LeftChild)
            {
                ancestorNode.LeftChild = searchResult.Node.LeftChild;
            }
            if (searchResult.Node.LeftChild != null)
            {
                searchResult.Node.LeftChild.Parent = ancestorNode;
            }

            if (ancestorNode != searchResult.Node.RightChild)
            {
                ancestorNode.RightChild = searchResult.Node.RightChild;
            }
            if (searchResult.Node.RightChild != null)
            {
                searchResult.Node.RightChild.Parent = ancestorNode;
            }

            AttachNewChildToParent(searchResult.Node, ancestorNode, searchResult.Node.Parent);
            return true;
        }

        private void AttachNewChildToParent(BSTNode<T> oldNode, BSTNode<T> newNode, BSTNode<T> Parent)
        {
            if (Parent == null)
            {
                Root = newNode;
                return;
            }

            if (oldNode == Parent.LeftChild)
            {
                Parent.LeftChild = newNode;
            }
            else
            {
                Parent.RightChild = newNode;
            }
        }

        public int Count()
        {
            return InDepthTraversing(Root);
        }

        private int InDepthTraversing(BSTNode<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            int leftCount = InDepthTraversing(node.LeftChild);
            int rightCount = InDepthTraversing(node.RightChild);

            return 1 + leftCount + rightCount;
        }

        public List<BSTNode> WideAllNodes()
        {
            List<BSTNode> result = new List<BSTNode>();

            if (Root == null)
            {
                return result;
            }

            Queue<BSTNode> nodes = new Queue<BSTNode>();
            nodes.Enqueue(Root as BSTNode);

            while (nodes.Count != 0)
            {
                BSTNode currentElement = nodes.Dequeue();
                result.Add(currentElement);

                if (currentElement.LeftChild != null)
                {
                    nodes.Enqueue(currentElement.LeftChild);
                }

                if (currentElement.RightChild != null)
                {
                    nodes.Enqueue(currentElement.RightChild);
                }
            }

            return result;
        }

        public List<BSTNode> DeepAllNodes(int option)
        {
            List<BSTNode> result = new List<BSTNode>();

            if (Root == null)
            {
                return result;
            }

            switch (option) {
                case (int)Options.PREORDER:
                    PreOrderTraversal(result, Root as BSTNode);
                    break;
                case (int)Options.INORDER:
                    InOrderTraversal(result, Root as BSTNode);
                    break;
                case (int)Options.POSTORDER:
                    PostOrderTraversal(result, Root as BSTNode);
                    break;
            }

            return result;
        }

        private void PreOrderTraversal(List<BSTNode> result, BSTNode node)
        {
            if (node != null)
            {
                result.Add(node);
                PreOrderTraversal(result, node.LeftChild);
                PreOrderTraversal(result, node.RightChild);
            }
        }

        private void InOrderTraversal(List<BSTNode> result, BSTNode node)
        {
            if (node != null)
            {
                InOrderTraversal(result, node.LeftChild);
                result.Add(node);
                InOrderTraversal(result, node.RightChild);
            }
        }

        private void PostOrderTraversal(List<BSTNode> result, BSTNode node)
        {
            if (node != null)
            {
                PostOrderTraversal(result, node.LeftChild);
                PostOrderTraversal(result, node.RightChild);
                result.Add(node);
            }
        }
    }
}