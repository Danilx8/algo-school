using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures2
{
    public static class BalancedBST
    {
        public static int[] GenerateBBSTArray(int[] a)
        {
            int treeSize = (int)Math.Pow(2, Math.Floor(Math.Log(a.Length, 2)) + 1) - 1;
            int[] genereatedTree = new int[treeSize];
            
            if (a.Length == 0)
            {
                return genereatedTree;
            }

            Array.Sort(a);
            AddNewElement(genereatedTree, a, 0);
            
            return genereatedTree;
        }

        private static void AddNewElement(int[] tree, int[] subArray, int destinationIndex)
        {
            int pivot = subArray.Length / 2;
            if (pivot % 2 == 0 && pivot != 0)
            {
                --pivot;
            }
            tree[destinationIndex] = subArray[pivot];

            int[] leftSide = new int[pivot];
            int[] rightSide = new int[subArray.Length - pivot - 1];

            Array.Copy(subArray, 0, leftSide, 0, pivot);
            Array.Copy(subArray, pivot + 1, rightSide, 0, subArray.Length - pivot - 1);

            if (leftSide.Length == 0 || leftSide == Array.Empty<int>() && rightSide.Length == 0 || rightSide == Array.Empty<int>())
            {
                return;
            }

            if (leftSide.Length != 0 || leftSide == Array.Empty<int>())
            {
                AddNewElement(tree, leftSide, destinationIndex * 2 + 1);
            }
            if (rightSide.Length != 0 || leftSide == Array.Empty<int>())
            {
                AddNewElement(tree, rightSide, destinationIndex * 2 + 2);
            } 
        }
    }
}