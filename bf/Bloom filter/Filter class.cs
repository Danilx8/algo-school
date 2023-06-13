using System.Collections.Generic;
using System;
using System.IO;
using System.Linq.Expressions;

namespace AlgorithmsDataStructures
{
    public class BloomFilter
    {
        public UInt64 filter_len;
        public byte[] array;

        public BloomFilter(UInt64 f_len)
        {
            filter_len = f_len;
            array = new byte[filter_len];
        }

        public UInt64 Hash1(string str1)
        {
            const int MULTIPLIER = 17;
            UInt64 sum = 0;

            for (int i = 0; i < str1.Length; i++)
            {
                UInt64 code = (UInt64)str1[i];
                sum *= MULTIPLIER;
                sum += code;
            }
            return sum % filter_len;
        }
        public UInt64 Hash2(string str1)
        {
            const int MULTIPLIER = 223;
            UInt64 sum = 0;

            for (int i = 0; i < str1.Length; i++)
            {
                UInt64 code = (UInt64)str1[i];
                sum *= MULTIPLIER;
                sum += code;
            }
            return sum % filter_len;
        }

        public void Add(string str1)
        {
            array[Hash1(str1)] = array[Hash2(str1)] = 1;
        }

        public bool IsValue(string str1)
        {
            if (array[Hash1(str1)] == 1 && array[Hash2(str1)] == 1) return true;
            return false;
        }
    }
}