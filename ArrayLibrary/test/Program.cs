using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArrayLibrary;
namespace test
{
    class Program
    {
        public static void InsertionSort(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                int temp = a[i];
                int index = i;
                for (; index >= 0 && temp < a[index - 1]; index--)
                {
                    a[index] = a[index - 1];
                }
                a[index] = temp;
            }
        }
        public static void binSort(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                int first = 0;
                int last = i;
                int mid = last / 2;
                while (last >= first)
                {
                    if (a[i + 1] == a[mid])
                        break;
                    if (a[i + 1] > a[mid])
                        first = mid - 1;
                    else
                        last = mid - 1;
                    mid = first + (last - first) / 2;
                }
                int temp = a[i + 1];
                for (int j = i+1; j >mid; j--)
                {
                    a[j] = a[j-1];
                }
                a[mid] = temp;
            }
        }

        static void Main(string[] args)
        {
            do
            {
                int[] a = Arrays.CreateArray();
                Arrays.print(a);
                //InsertionSort(a);
                binSort(a);
               
                Arrays.print(a);
                Console.ReadKey();
            } while (true);
        }
    }
}
