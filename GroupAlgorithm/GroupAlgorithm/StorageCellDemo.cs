using System;

namespace GroupAlgorithm
{
    class StorageCell
    {
        public object Get()
        {
            return m.Read();
        }

        public void Put(object x)
        {
            m.Write(x);
        }

        MemoryCell m = new MemoryCell();
    }

    class StorageCellDemo
    {
        public static void Main(string[] args)
        {
            StorageCell m = new StorageCell();
            m.Put("Hello");
            Console.WriteLine(m.Get());
        }
    }
}