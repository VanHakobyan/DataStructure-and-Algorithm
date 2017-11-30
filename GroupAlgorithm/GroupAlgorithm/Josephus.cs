using System;
using System.Collections.Generic;

namespace GroupAlgorithm
{

    public class Josephus
    {
        // Recursively construct a perfectly balanced BinarySearchTreeWithRank
        // by repeated insertions in O( N log N ) time.
        // t should be empty on the initial call.
        public static void BuildTree(BinarySearchTreeWithRank<int> t,
            int low, int high)
        {
            int center = (low + high) / 2;

            if (low <= high)
            {
                t.Insert(center);

                BuildTree(t, low, center - 1);
                BuildTree(t, center + 1, high);
            }
        }

        // Return the winner in the Josephus problem.
        // Search Tree implementation.
        public static int Jos2(int people, int passes)
        {
            BinarySearchTreeWithRank<int> t = new BinarySearchTreeWithRank<int>();

            BuildTree(t, 1, people);

            int rank = 1;
            while (people > 1)
            {
                rank = (rank + passes) % people;
                if (rank == 0)
                    rank = people;

                t.Remove(t.FindKth(rank));
                people--;
            }

            return t.FindKth(1);
        }


        // Return the winner in the Josephus problem.
        // (Array) List implementation.
        public static int Jos1(int people, int passes)
        {
            List<int> lst = new List<int>();

            for (int i = 1; i <= people; i++)
                lst.Add(i);

            int rank = 0;
            while (people > 1)
            {
                rank = (rank + passes) % people;

                lst.RemoveAt(rank);
                people--;
            }

            return lst[0];
        }

        // Quickie main to test
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length == 2)
                {
                    int arg1 = int.Parse(args[0]);
                    int arg2 = int.Parse(args[1]);
                    Console.WriteLine("JOS2: " + Jos2(arg1, arg2));
                    Console.WriteLine("JOS1: " + Jos1(arg1, arg2));
                }
                else
                    Console.Error.WriteLine("Usage: Josephus People Passes");
            }
            catch (FormatException)
            {
                Console.Error.WriteLine("Usage: Josephus People Passes");
            }
        }
    }
}s