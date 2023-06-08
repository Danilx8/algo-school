using AlgorithmsDataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associative_array
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NativeDictionary<int> dict = new NativeDictionary<int>(19);

            dict.Put("first", 1);
            Console.WriteLine(dict.IsKey("first"));
            dict.Put("second", 2);
            dict.Put("third", 3);
            dict.Put("fourth", 4);
            dict.Put("fivth", 5);

            Console.WriteLine(dict.IsKey("first"));
            for (int i = 0; i < 19; ++i)
            {
                if (!(dict.slots[i] is null)) Console.Write(dict.slots[i] + "-" + i + " ");
            }
            Console.WriteLine();
            
            for (int j = 0; j < 19; ++j)
            {
                if (dict.values[j] != default) Console.Write(dict.values[j] + "-" + j + " ");
            }

            Console.WriteLine(dict.IsKey("first"));

            Console.ReadKey();
        }
    }
}
