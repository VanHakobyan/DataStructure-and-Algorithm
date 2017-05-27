using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One100
{
    class Program
    {
        static int sum(int x)
        {
            if (x < 10)
            {
                return 1;
            }
            return 1 + sum(x / 10);
        }
        static void Main(string[] args)
        {
            Console.WriteLine(sum(200));
        }
    }
}
