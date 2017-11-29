using System;
using System.IO;

namespace BinarySearch
{
    public class BinarySearchRecursive
    {
        public const int NOT_FOUND = -1;

        // Performs the standard binary search
        // using two comparisons per level.
        // This is a driver that calls the recursive method.
        // Returns index where item is found or NOT_FOUND if not found.
        public static int BinarySearch<AnyType>(AnyType[] a, AnyType x) where AnyType : IComparable
        {
            return BinarySearch(a, x, 0, a.Length - 1);
        }

        // Hidden recursive routine.
        public static int BinarySearch<AnyType>(AnyType[] a, AnyType x, int low, int high) where AnyType : IComparable
        {
            if (low > high)
                return NOT_FOUND;

            int mid = (low + high) / 2;

            if (a[mid].CompareTo(x) < 0)
                return BinarySearch(a, x, mid + 1, high);
            else if (a[mid].CompareTo(x) > 0)
                return BinarySearch(a, x, low, mid - 1);
            else
                return mid;
        }
    }
}