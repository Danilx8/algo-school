using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsDataStructures
{
    public partial class LinkedList
    {
        public static LinkedList Summ(LinkedList first, LinkedList second)
        {
            if (first.Count() != second.Count()) return null;

            LinkedList result = new LinkedList();
            Node firstNode = first.head;
            Node secondNode = second.head;
            while(firstNode != null)
            {
                Node summNode = new Node(firstNode.value + secondNode.value);
                result.AddInTail(summNode);
                firstNode = firstNode.next;
                secondNode = secondNode.next;
            }
            return result;
        }
    }
}
