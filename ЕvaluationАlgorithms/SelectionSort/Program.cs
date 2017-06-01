using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectionSort
{
    class Program
    {
        static int Max(int[] a, int t)
        {
            int maxIndex = 0;
            for (int i = 0; i < a.Length - t; i++)
            {
                if (a[maxIndex] < a[i])
                {
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        static void Swap(ref int a, ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }

        static void Sort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int maxElem = Max(array, i);
                Swap(ref array[maxElem], ref array[array.Length - 1 - i]);
            }
        }
        static void Main(string[] args)
        {
            int[] array = { 1, 5, 14, 2, 19, 54 };
            Sort(array);
            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.Write(array[i]+",");
            }
        }
    }
}
