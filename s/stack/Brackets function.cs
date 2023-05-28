using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsDataStructures
{
    public partial class Stack<T>
    {
        public static bool BalancedBrackets(string brackets)
        {
            Stack<char> stack = new Stack<char>();
            char[] chars = brackets.ToCharArray();

            for (int i = 0; i < chars.Length; ++i)
            {
                stack.Push(chars[i]);
            }

            if (stack.Size() % 2 != 0) return false;

            return BracketsBalancer(stack);
        }

        private static bool BracketsBalancer(Stack<char> stack)
        {
            while (stack.Size() > 0)
            {
                if (stack.Pop() == '(') return false;
                else
                {
                    if (!BracketsBalancer(stack) && stack.Size() == 0) return true;
                }
            }
            return false;
        }
    }
}
