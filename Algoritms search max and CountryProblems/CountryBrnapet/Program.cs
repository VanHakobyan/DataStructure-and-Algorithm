using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryBrnapet
{
    class Program
    {
        static int CountryArdz(int n, int k)
        {
            if (k > n) return 0;
            if (k == n || k == 0) return 1;
            else
                return CountryArdz(n - 1, k - 1) +
                 CountryArdz(n - 1, k);
        }
        static void Main(string[] args)
        {
            Console.WriteLine(CountryArdz(3,2));
        }
    }
}
