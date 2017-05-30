using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondLessons
{
    class Program
    {
        static int maxElem(int[] array)
        {
            if (array.Length == 1)
            {
                return array[0];
            }
            int i = 0;
            int[] first = new int[array.Length / 2];
            int[] second = new int[array.Length - first.Length];
            for( ; i < first.Length; i++)
            {
                first[i] = second[i];
            }
            for (int k = 0; k < second.Length; k++)
            {
                second[k] = array[i++];
            }
            return Math.Max(maxElem(first), maxElem(second));

        }
        static void Main(string[] args)
        {
            Console.WriteLine(maxElem(new int[] { 12, 51, 25, 14, 16, 10}));
        }
    }
}
