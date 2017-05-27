using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch
{
    class Program
    {
        static int BinSearch(int[] a, int begin, int end, int n)
        {
            if (end < begin)
            {
                return -1;
            }
            int mid = begin + (end - begin) / 2;
            if (a[mid] == n)
            {
                return mid;
            }
            if (a[mid] > n)
            {
                return BinSearch(a, begin, mid - 1, n);
            }
            else
            {
                return BinSearch(a, mid + 1,end, n);
            }
        }
        static void Main(string[] args)
        {
            Random r = new Random();
            int size = r.Next(6, 24);
            int[] a = new int[size];
            Console.WriteLine("Size=" + size);
            for (int i = 0; i < size; i++)
            {
                a[i] = r.Next(-99, 100);
            }
            Array.Sort(a);
            foreach (var item in a)
            {
                Console.Write(item + "\t");
            }
            do
            {
                Console.WriteLine("enter num");
                int n = int.Parse(Console.ReadLine());
                Console.WriteLine(BinSearch(a, 0, a.Length - 1, n));
            } while (true);
        }
    }
}
