using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortingGeneric;

namespace TestSort
{
    class Program
    {
        private const int NUM_ITEMS = 1000;
        private static int theSeed = 1;

        // Randomly rearrange an array arr.
        // The random numbers used depend on the time and day.
        public static void Permute<AnyType>(AnyType[] arr)
        {
            Random r = new Random();

            for (int j = 1; j < arr.Length; j++)
                Sort.Swap(ref arr[j], ref arr[r.Next(0, j)]);
        }

        public static void CheckSort(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
                if (a[i] != i)
                    Console.WriteLine("Error at " + i);
            Console.WriteLine("Finished checksort");
        }

        public static void Main(string[] args)
        {
            int[] a = new int[NUM_ITEMS];
            for (int i = 0; i < a.Length; i++)
                a[i] = i;

            for (theSeed = 0; theSeed < 20; theSeed++)
            {
                Permute(a);
                Sort.InsertionSort(a);
                CheckSort(a);

                Permute(a);
                Sort.Heapsort(a);
                CheckSort(a);

                Permute(a);
                Sort.Shellsort(a);
                CheckSort(a);

                Permute(a);
                Sort.MergeSort(a);
                CheckSort(a);

                Permute(a);
                Sort.Quicksort(a);
                CheckSort(a);

                Permute(a);
                Sort.QuickSelect(a, NUM_ITEMS / 2);
                Console.WriteLine(a[NUM_ITEMS / 2 - 1] + " is " + NUM_ITEMS / 2 + "th smallest");
            }
        }
    }
}
