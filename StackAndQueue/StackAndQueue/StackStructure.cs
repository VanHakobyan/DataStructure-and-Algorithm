using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAndQueue
{
    public class StackStructure
    {
        public void Demo()
        {
            var items = Convert.ToInt16(Console.ReadLine());
            for (var i = 0; i < items; i++)
            {
                Console.Write($"Enter the  + {i} +  Value :");
                Push(Convert.ToInt16(Console.ReadLine()));
            }
            for (var i = 0; i < items; i++)
            {
                Console.WriteLine($"poped the  {i}  Value :" + Pop());
                Console.ReadLine();
            }
            Console.ReadLine();
        }
        public StackStructure(int value)
        {
            stack.Top = -1;
            stack.Items = new int[value];
        }
        Stack<int> stack;
        public int Pop()
        {
            stack.Top--;
            if (stack.Items.Length <= 0)
            {
                Console.WriteLine("The Stack is Empty");
            }
            return stack.Items[stack.Top + 1];
        }
        public void Push(int item)
        {
            stack.Top++;
            stack.Items[stack.Top] = item;
        }
    }
}
