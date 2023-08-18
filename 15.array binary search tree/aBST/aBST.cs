using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace AlgorithmsDataStructures2
{
    public class aBST
    {
        public int?[] Tree; // массив ключей

        public aBST(int depth)
        {
            // правильно рассчитайте размер массива для дерева глубины depth:
            int tree_size = 0;
            tree_size = CalculateTreeSize(depth, tree_size);
            Tree = new int?[tree_size];
            for (int i = 0; i < tree_size; i++) Tree[i] = null;
        }

        private int CalculateTreeSize(int depth, int treeSize)
        {
            treeSize += Convert.ToInt32(Math.Pow(2, depth));
            --depth;
            if (depth < 0)
            {
                return treeSize;
            }
            return CalculateTreeSize(depth, treeSize);
        }

        public int? FindKeyIndex(int key)
        {
            int childIndex = 1;
            for (int i = 0; i < Tree.Length; i = i * 2 + childIndex)
            {
                if (Tree[i] == null)
                {
                    return -i;
                }

                int difference = key.CompareTo(Tree[i]);

                switch (difference)
                {
                    case -1:
                        childIndex = 1;
                        break;
                    case 0:
                        return i;
                    case 1:
                        childIndex = 2;
                        break;
                }
            }
            return null; // не найден
        }

        public int AddKey(int key)
        {
            int? foundIndex = FindKeyIndex(key);
            if (foundIndex == null)
            {
                return -1;
            }

            if (foundIndex == 0 && Tree[0] == null || foundIndex < 0)
            {
                foundIndex = -foundIndex;
                Tree[(int)foundIndex] = key;
                return (int)foundIndex;
            }

            return (int)foundIndex;
        }
    }
}