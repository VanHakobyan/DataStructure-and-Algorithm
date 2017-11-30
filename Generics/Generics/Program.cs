using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generics.Class;

namespace Generics
{
    class Program
    {
        public static void Main(string[] args)
        {
            Shape[] sh1 =
            {
                new Circle(2.0),
                new Square(3.0),
                new Rectangle(3.0, 4.0)
            };

            Circle[] c1 =
            {
                new Circle(2.0),
                new Circle(3.0),
                new Circle(1.0)
            };

            string[] st1 = { "Joe", "Bob", "Bill", "Zeke" };
            int[] i1 = { 1, 4, 2, 3 };

            Shape maxShape =Square.GenericFindMaxDemo.findMax(sh1);
            string maxString = Square.GenericFindMaxDemo.findMax(st1);
            Circle maxCircle = Square.GenericFindMaxDemo.findMax(c1);
            int maxInt = Square.GenericFindMaxDemo.findMax(i1);

            Console.WriteLine(maxShape);
            Console.WriteLine(maxString);
            Console.WriteLine(maxCircle);
            Console.WriteLine(maxInt);
        }
    }
}
