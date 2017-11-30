using System;

namespace GroupAlgorithm
{
    public class Squares
    {
        private static double[] squareRoots = new double[100];

        static Squares()
        {
            for (int i = 0; i < squareRoots.Length; i++)
                squareRoots[i] = Math.Sqrt((double) i);
        }

        // Rest of class
        public static void Main(string[] args)
        {
            Console.WriteLine("View first five entries");

            for (int i = 0; i < 5 && i < squareRoots.Length; i++)
                Console.WriteLine(squareRoots[i]);
        }
    }
}