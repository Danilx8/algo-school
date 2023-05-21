using AlgorithmsDataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linked_list
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList identicalList = new LinkedList();
            Node node = new Node(0);
            identicalList.InsertAfter(null, node);
            Console.ReadKey();
        }
    }
}

