using System;

namespace Fibonacci
{
    class Program
    {
        static long fib(int n)
        {
            if (n <= 2)
                return 1;
            return fib(n - 1) + fib(n - 2);
        }
        static void Main(string[] args)
        {
            Console.WriteLine(fib(45));
        }
    }
}
