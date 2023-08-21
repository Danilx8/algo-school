using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class BSTNode
    {
        public int NodeKey; // ключ узла
        public BSTNode Parent; // родитель или null для корня
        public BSTNode LeftChild; // левый потомок
        public BSTNode RightChild; // правый потомок	
        public int Level; // глубина узла

        public BSTNode(int key, BSTNode parent)
        {
            NodeKey = key;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }


    public class BalancedBST
    {
        public BSTNode Root; // корень дерева

        public BalancedBST()
        {
            Root = null;
        }

        public void GenerateTree(int[] a)
        {
            // создаём дерево с нуля из неотсортированного массива a
            // ...
            if (a.Length != 0)
            {
                Array.Sort(a);
                TreeAssembly(null, a);
            }
        }

        private BSTNode TreeAssembly(BSTNode parent, int[] array)
        {
            int pivot = array.Length / 2;
            if (pivot != 0 && pivot % 2 == 0)
            {
                --pivot;
            }

            BSTNode node = new BSTNode(array[pivot], parent);
            if (parent == null)
            {
                node.Level = 0;
                Root = node;
            } else
            {
                node.Level = parent.Level + 1;
            }

            int[] leftSide = new int[pivot];
            int[] rightSide = new int[array.Length - pivot - 1];

            Array.Copy(array, 0, leftSide, 0, pivot);
            Array.Copy(array, pivot + 1, rightSide, 0, array.Length - pivot - 1);
            if (leftSide.Length != 0 || leftSide == Array.Empty<int>())
            {
                node.LeftChild = TreeAssembly(node, leftSide);
            }
            if (rightSide.Length != 0 || leftSide == Array.Empty<int>())
            {
                node.RightChild = TreeAssembly(node, rightSide);
            }

            return node;
        }

        public bool IsBalanced(BSTNode root_node)
        {
            return AreSubtreesLengthsEqual(root_node); // сбалансировано ли дерево с корнем root_node
        }

        private bool AreSubtreesLengthsEqual(BSTNode node)
        {
            bool result;

            int leftLength = CalculateSubTreeDepth(node.LeftChild, 0, true);
            int rightLength = CalculateSubTreeDepth(node.RightChild, 0, false);

            if (Math.Abs(leftLength - rightLength) > 1)
            {
                return false;
            }

            if (leftLength == 0 && rightLength == 0)
            {
                return true;
            }

            result = AreSubtreesLengthsEqual(node.LeftChild) && AreSubtreesLengthsEqual(node.RightChild);

            return result;
        }

        private int CalculateSubTreeDepth(BSTNode node, int counter, bool leftDirection)
        {
            if (node == null)
            {
                return counter;
            }

            if (leftDirection && node.LeftChild != null)
            {
                CalculateSubTreeDepth(node.LeftChild, ++counter, leftDirection);
            }

            if (!leftDirection && node.RightChild != null)
            {
                CalculateSubTreeDepth(node.RightChild, ++counter, leftDirection);
            }

            return counter;
        }
    }
}