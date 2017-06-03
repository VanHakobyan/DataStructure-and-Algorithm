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
                for (int j = i + 1; j > mid; j--)
                {
                    a[j] = a[j - 1];
                }
                a[mid] = temp;
            }
        }
        static public void MainMerge(int[] numbers, int left, int mid, int right)
        {
            int[] temp = new int[25];
            int i, eol, num, pos;

            eol = (mid - 1);
            pos = left;
            num = (right - left + 1);

            while ((left <= eol) && (mid <= right))
            {
                if (numbers[left] <= numbers[mid])
                    temp[pos++] = numbers[left++];
                else
                    temp[pos++] = numbers[mid++];
            }

            while (left <= eol)
                temp[pos++] = numbers[left++];

            while (mid <= right)
                temp[pos++] = numbers[mid++];

            for (i = 0; i < num; i++)
            {
                numbers[right] = temp[right];
                right--;
            }
        }

        static public void SortMerge(int[] numbers, int left, int right)
        {
            int mid;

            if (right > left)
            {
                mid = (right + left) / 2;
                SortMerge(numbers, left, mid);
                SortMerge(numbers, (mid + 1), right);

                MainMerge(numbers, left, (mid + 1), right);
            }
        }
        static void Main(string[] args)
        {
            do
            {
                int[] a = { 5,14,5,12,13,18,26,2,0,69,75};
                Arrays.print(a);
                Console.WriteLine();
                //InsertionSort(a);
                //binSort(a);
                SortMerge(a, 0, a.Length-1);
                //foreach (var item in a)
                //{
                //    Console.Write(item+" ");
                //}
                Arrays.print(a);
                Console.ReadKey();
            } while (true);
        }
    }
}
