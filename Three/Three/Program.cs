using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three
{
    class Program
    {
        static void Main(string[] args)
        {
            AATree<int> t = new AATree<int>(-9999);
            const int NUMS = 40000;
            const int GAP = 307;

            Console.WriteLine("Checking... (no bad output means success)");

            t.Insert(NUMS * 2);
            t.Insert(NUMS * 3);
            for (int i = GAP; i != 0; i = (i + GAP) % NUMS)
                t.Insert(i);
            Console.WriteLine("Inserts complete");

            t.Remove(t.FindMax());
            for (int i = 1; i < NUMS; i += 2)
                t.Remove(i);
            t.Remove(t.FindMax());
            Console.WriteLine("Removes complete");


            if (t.FindMin() != 2 || t.FindMax() != NUMS - 2)
                Console.WriteLine("FindMin or FindMax error!");

            for (int i = 2; i < NUMS; i += 2)
                if (t.Find(i) != i)
                    Console.WriteLine("Error: find fails for " + i);

            for (int i = 1; i < NUMS; i += 2)
                if (t.Contains(i))
                    Console.WriteLine("Error: Found deleted item " + i);
        }
    }
}
