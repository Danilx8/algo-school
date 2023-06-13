using System.Collections.Generic;
using System;
using System.IO;
using System.Linq.Expressions;

namespace AlgorithmsDataStructures
{
    public class ArrayOfBits
    {
        public bool[] Bits { get; set; }

        public ArrayOfBits(int size)
        {
            Bits = new bool[size];
        }
    }

    public class BloomFilter
    {
        public int filter_len;
        public ArrayOfBits array;

        public BloomFilter(int f_len)
        {
            filter_len = f_len;
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
            array.Bits[Hash1(str1)] = array.Bits[Hash2(str1)] = true;
        }

        public bool IsValue(string str1)
        {
            if (array.Bits[Hash1(str1)] && array.Bits[Hash2(str1)]) return true;
            return false;
        }
    }
}