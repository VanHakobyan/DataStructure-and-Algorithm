
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchIterative
{
    class Program
    {
        static int BinSearch(int[] a, int n)
        {

            int first = 0;
            int midle = a.Length / 2;
            int last = a.Length - 1;
            while (last >= first)
            {
                if (n == a[midle])
                {
                    return midle;
                }
                if (n > a[midle])
                {
                    first = midle + 1;
                }
                else
                {
                    last = midle - 1;
                }
                midle = first + (last - first) / 2;
            }
            return -1;
        }
        static void Main(string[] args)
        {
            int[] a = { 2, 5, 15, 48, 125, 131, 185 };
            Console.WriteLine(BinSearch(a, 185));//last
            Console.WriteLine(BinSearch(a, 2));//first
            Console.WriteLine(BinSearch(a, 48));//midle
            Console.WriteLine(BinSearch(a, 8));//404

        }
    }
}
