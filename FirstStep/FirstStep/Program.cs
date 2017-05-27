using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstStep
{
    class Program
    {
        static int factorial(int n)
        {
            if (n < 2) return 1;
            return n * factorial(n - 1);
        }
        static void Main(string[] args)
        {
            do
            {
                int n = int.Parse(Console.ReadLine());

                Console.WriteLine(factorial(n));
            } while (true);
        }
    }
}
