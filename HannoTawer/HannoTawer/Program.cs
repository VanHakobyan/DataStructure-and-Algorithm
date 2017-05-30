using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HannoTower
{
    class Program
    {
        static void Tower(int n, List<int> sourse, List<int> dest, List<int> bridge)
        {
            if (n == 1)
            {
                dest.Add(sourse[sourse.Count - 1]);
                sourse.RemoveAt(sourse.Count - 1);

            }
            else
            {
                Tower(n - 1, sourse, bridge, dest);
                Tower(1, sourse, dest, bridge);
                Tower(n - 1, bridge, dest, sourse);
            }
        }



        static void ShowList(List<int> list)
        {
            if (list.Count == 0)
                Console.WriteLine("empty");
            foreach (var item in list)
            {
                Console.Write(item+" ,");
            }

        }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("enter number ");
                int n = int.Parse(Console.ReadLine());
                List<int> A = new List<int>();
                Console.WriteLine();
                for (int i = 0; i < n; i++)
                {
                    A.Add(n - i);
                    Console.Write(A[i] + "\t");

                }
                Console.WriteLine();

                List<int> C = new List<int>();
                List<int> B = new List<int>();
                Tower(n, A, B, C);
                //ShowList(C);
                //ShowList(A);
                ShowList(B);
            }
        }
    }
}
