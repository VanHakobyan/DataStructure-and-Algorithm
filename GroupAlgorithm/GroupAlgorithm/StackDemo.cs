using System;
using weiss.nonstandard;

namespace GroupAlgorithm
{
    public class StackDemo
    {
        public static void stackOps(string type, IStack<int> s)
        {
            Console.WriteLine(type + " output: ");
            for (int i = 0; i < 10; i++)
                s.Push(i);

            while (!s.IsEmpty())
                Console.WriteLine(s.TopAndPop());
        }

        public static void Main(string[] args)
        {
            IStack<int> s1 = new ArrayStack<int>();
            IStack<int> s2 = new ListStack<int>();

            stackOps("ArrayStack", s1);
            stackOps("ListStack", s2);
        }
    }

}