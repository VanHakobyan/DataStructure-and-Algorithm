using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayLibrary
{
    public static class Arrays
    {
        public static int[] CreateArray()
        {
            Random rn = new Random();
            int[] array = new int[rn.Next(10, 100)];
            for (int i = 0; i < array.Length - 1; i++)
            {
                array[i] = rn.Next(0, 100);
            }
            return array;
        }
        public static void print(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.Write(array[i] + " , ");
            }
        }
        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        
    }
}
