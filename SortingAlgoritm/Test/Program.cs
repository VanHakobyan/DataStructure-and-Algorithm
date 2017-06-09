using SortingLibrary;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Random rn = new Random();
            int[] array = new int[rn.Next(0, 100)];
            Console.WriteLine("Before Sort \n");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rn.Next(0, 100);
                Console.Write(array[i]+" ");
            }
            Console.WriteLine("\n");

            //UnComment one items

            Bubble.BubbleSort(ref array);
            //Insertion.InsertionSort(array);
            //Merge.MegreSort(array);
            //Quick.QuickSort(array);
            //Selection.SelectionSort(array);

            Console.WriteLine("After Sort \n");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]+" ");
            }
            Console.WriteLine();
        }
    }
}
