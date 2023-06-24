using System.Collections.Generic;
using System;
using System.IO;
using System.Linq.Expressions;

namespace AlgorithmsDataStructures
{
    public class ArrayOfBits
    {
        private int[] data;
        private const int BitsPerInt = 32;

        public ArrayOfBits(int size)
        { 
            int arraySize = (size + BitsPerInt - 1) / BitsPerInt;
            data = new int[arraySize];
        }

        public bool Get(int index)
        {
            int intIndex = index / BitsPerInt;
            int bitOffset = index % BitsPerInt;

            return ((data[intIndex] >> bitOffset) & 1) != 0;
        }

        public void Set(int index, bool value)
        {
            int intIndex = index / BitsPerInt;
            int bitOffset = index % BitsPerInt;

            if (value)
            {
                data[intIndex] |= (1 << bitOffset);
            }
            else
            {
                data[intIndex] &= ~(1 << bitOffset);
            }
        }
    }

    public class BloomFilter
    {
        
        public int filter_len;
        public ArrayOfBits array;

        public BloomFilter(int f_len)
        {
            filter_len = 32;
            array = new ArrayOfBits(filter_len);
        }

        public int Hash1(string str1)
        {
            const int MULTIPLIER = 17;
            ulong sum = 0;

            for (int i = 0; i < str1.Length; i++)
            {
                ulong code = (ulong)str1[i];
                sum *= MULTIPLIER;
                sum += code;
            }
            return (int)(sum % (ulong)filter_len);
        }
        public int Hash2(string str1)
        {
            const int MULTIPLIER = 223;
            ulong sum = 0;

            for (int i = 0; i < str1.Length; i++)
            {
                ulong code = (ulong)str1[i];
                sum *= MULTIPLIER;
                sum += code;
            }
            return (int)(sum % (ulong)filter_len);
        }

        public void Add(string str1)
        {
            array.Set(Hash1(str1), true);
            array.Set(Hash2(str1), true);
        }

        public bool IsValue(string str1)
        {
            if (array.Get(Hash1(str1)) && array.Get(Hash2(str1))) return true;
            return false;
        }
    }
}