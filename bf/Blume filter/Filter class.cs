using System.Collections.Generic;
using System;
using System.IO;
using System.Linq.Expressions;

namespace AlgorithmsDataStructures
{
    public class BloomFilter
    {
        public int filter_len;
        public byte[] array;

        public BloomFilter(int f_len)
        {
            filter_len = f_len;
            array = new byte[filter_len];
            for (int i = 0; i < filter_len; ++i)
            {
                array[i] = 0;
            }
        }

        public int Hash1(string str1)
        {
            const int multiplier = 17;
            int sum = 0;

            for (int i = 0; i < str1.Length; i++)
            {
                int code = (int)str1[i];
                sum *= multiplier;
                sum += code;
            }
            return sum % filter_len;
        }
        public int Hash2(string str1)
        {
            const int multiplier = 223;
            int sum = 0;

            for (int i = 0; i < str1.Length; i++)
            {
                int code = (int)str1[i];
                sum *= multiplier;
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