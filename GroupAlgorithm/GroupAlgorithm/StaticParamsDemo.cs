using System;
// This demo illustrates that parameters are always statically deduced.
namespace GroupAlgorithm
{
    class Base
    {
        public virtual void Foo(Base x)
        {
            Console.WriteLine("Base.Base");
        }

        public virtual void Foo(Derived x)
        {
            Console.WriteLine("Base.Derived");
        }
    }

    class Derived : Base
    {
        public override void Foo(Base x)
        {
            Console.WriteLine("Derived.Base");
        }

        public override void Foo(Derived x)
        {
            Console.WriteLine("Derived.Derived");
        }
    }

    class StaticParamsDemo
    {
        public static void WhichFoo(Base arg1, Base arg2)
        {
            // It is guaranteed that we will call foo( Base )
            // Only issue is which class's version of foo( Base )
            // is called; the dynamic type of arg1 is used to decide.
            arg1.Foo(arg2);
        }

        public static void Main(string[] args)
        {
            Base b = new Base();
            Derived d = new Derived();

            WhichFoo(b, b);
            WhichFoo(b, d);
            WhichFoo(d, b);
            WhichFoo(d, d);
        }
    }
}