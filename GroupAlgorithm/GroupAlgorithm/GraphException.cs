
using System;

namespace GroupAlgorithm
{
    public class GraphException : ApplicationException
    {
        public GraphException(string msg) : base(msg)
        {
        }
    }

    class TestException
    {
        static void Foo()
        {
            throw new GraphException("oops");
        }

        static void Main(string[] args)
        {
            try
            {
                Foo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
