using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AlgorithmsDataStructures2
{
    public class Heap
    {

        public int[] HeapArray; // хранит неотрицательные числа-ключи
        private int tailIndex;

        public Heap() { HeapArray = null; }

        public void MakeHeap(int[] a, int depth)
        {
            // создаём массив кучи HeapArray из заданного
            // размер массива выбираем на основе глубины depth
            // ...

            HeapArray = new int[(int)Math.Pow(2, depth + 1) - 1];
            tailIndex = 0;
            for (int i = 0; i < a.Length; ++i)
            {
                HeapArray[tailIndex] = a[i];

                int currentIndex = tailIndex;
                int parentIndex = (currentIndex - 1) / 2;
                while (currentIndex > 0 && HeapArray[parentIndex] < HeapArray[currentIndex])
                {
                    (HeapArray[parentIndex], HeapArray[currentIndex]) = (HeapArray[currentIndex], HeapArray[parentIndex]);
                    currentIndex = parentIndex;
                    parentIndex = (currentIndex - 1) / 2;
                }
                ++tailIndex;
            }
            --tailIndex;
        }

        public int GetMax()
        {
            if (HeapArray == null || HeapArray == Array.Empty<int>())
            {
                return -1; // если куча пуста
            }

            int max = HeapArray[0];

            HeapArray[0] = HeapArray[tailIndex];
            HeapArray[tailIndex] = 0;
            int currentIndex = 0;

            while (currentIndex * 2 + 2 <= tailIndex)
            {
                int leftChildIndex = currentIndex * 2 + 1;
                int rightChildIndex = currentIndex * 2 + 2;
                int biggestChildIndex;

                if (HeapArray[rightChildIndex] > HeapArray[leftChildIndex])
                {
                    biggestChildIndex = rightChildIndex;
                } else
                {
                    biggestChildIndex = leftChildIndex;
                }

                if (HeapArray[currentIndex] > HeapArray[biggestChildIndex])
                {
                    break;
                }

                (HeapArray[currentIndex], HeapArray[biggestChildIndex]) = (HeapArray[biggestChildIndex], HeapArray[currentIndex]);
                currentIndex = biggestChildIndex;
            }

            --tailIndex;

            return max;
        }

        public bool Add(int key)
        {
            if (tailIndex == HeapArray.Length - 1)
            {
                return false; // если куча вся заполнена
            }

            int currentIndex = ++tailIndex;
            HeapArray[currentIndex] = key;
            int parentIndex = (currentIndex- 1) / 2;

            while (HeapArray[currentIndex] > HeapArray[parentIndex] && currentIndex > 0)
            {
                (HeapArray[currentIndex], HeapArray[parentIndex]) = (HeapArray[parentIndex], HeapArray[currentIndex]);
                currentIndex = parentIndex;
                parentIndex = (currentIndex - 1) / 2;
            }

            return true;
        }

    }
}