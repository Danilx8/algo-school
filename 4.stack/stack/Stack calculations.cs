using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsDataStructures
{
    public partial class Stack<T>
    {
        public static int StacksCalculations(string expression)
        {
            Stack<object> firstStack = new Stack<object>();
            Stack<int> secondStack = new Stack<int>();
            char[] chars = expression.ToCharArray();

            bool numberContinues = false;
            string number = "";
            for (int i = chars.Length - 1; i >= 0; --i)
            {
                if (numberContinues && char.IsDigit(chars[i])) number = chars[i] + number;
                else if (numberContinues)
                {
                    firstStack.Push(int.Parse(number));
                    number = "";
                    numberContinues = false;
                }
                else if (char.IsDigit(chars[i]))
                {
                    numberContinues = true;
                    number += chars[i];
                }
                else if (chars[i] != ' ') firstStack.Push(chars[i]);
            }

            firstStack.Push(int.Parse(number));

            while (firstStack.Size() > 0)
            {
                object currentObject = firstStack.Pop();
                char.TryParse(currentObject.ToString(), out char currentElement);

                if (currentObject is int) secondStack.Push((int)currentObject);
                else if (currentElement == '=') return secondStack.Pop();
                else
                {
                    int secondNumber = secondStack.Pop();
                    int firstNumber = secondStack.Pop();
                    switch (currentElement)
                    {
                        case '+':
                            secondStack.Push(firstNumber + secondNumber);
                            break;
                        case '*':
                            secondStack.Push(firstNumber * secondNumber);
                            break;
                        case '-':
                            secondStack.Push(firstNumber - secondNumber);
                            break;
                        case '/':
                            secondStack.Push(firstNumber / secondNumber);
                            break;
                        default:
                            throw new ArgumentException("Wrong input expression");
                    }
                }
            }
            return 0;
        }
    }
}
