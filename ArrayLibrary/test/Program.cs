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
        public static void InsertionSort(int [] a)
        {
            for (int i = 0; i < a.Length-1; i++)
            {
                int temp = a[i];
                int index = i;
                for (; index >=0 &&temp<a[index-1];index--)
                {
                    a[index] = a[index - 1];
                }
                a[index] = temp;
            }
        }
        static void Main(string[] args)
        {
            do
            {
                int[] a = Arrays.CreateArray();
                int x = 100;
                int y = 200;
                Arrays.Swap(ref x, ref y);

                Console.WriteLine("x="+x+" , "+"y="+y);
                Console.ReadKey();
            } while (true);
        }
    }
}
