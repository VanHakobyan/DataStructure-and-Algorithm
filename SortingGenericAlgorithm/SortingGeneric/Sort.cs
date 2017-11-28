using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingGeneric
{

    // A class that contains several sorting routines,
    // implemented as static methods.
    // Arrays are rearranged with smallest item first,
    // using compares.
    public class Sort
    {
        public static void Swap<AnyType>(ref AnyType x, ref AnyType y)
        {
            AnyType tmp = x;
            x = y;
            y = tmp;
        }

        // Simple insertion sort.
        public static void InsertionSort<AnyType>(AnyType[] a) where AnyType : IComparable<AnyType>
        {
            for (int p = 1; p < a.Length; p++)
            {
                AnyType tmp = a[p];
                int j = p;

                for (; j > 0 && tmp.CompareTo(a[j - 1]) < 0; j--)
                    a[j] = a[j - 1];
                a[j] = tmp;
            }
        }

        // Shellsort, using a sequence suggested by Gonnet.
        public static void Shellsort<AnyType>(AnyType[] a) where AnyType : IComparable<AnyType>
        {
            for (int gap = a.Length / 2; gap > 0;
                         gap = gap == 2 ? 1 : (int)(gap / 2.2))
                for (int i = gap; i < a.Length; i++)
                {
                    AnyType tmp = a[i];
                    int j = i;

                    for (; j >= gap && tmp.CompareTo(a[j - gap]) < 0; j -= gap)
                        a[j] = a[j - gap];
                    a[j] = tmp;
                }
        }

        // Standard heapsort.
        public static void Heapsort<AnyType>(AnyType[] a) where AnyType : IComparable<AnyType>
        {
            for (int i = a.Length / 2; i >= 0; i--)  /* buildHeap */
                PercDown(a, i, a.Length);
            for (int i = a.Length - 1; i > 0; i--)
            {
                Swap(ref a[0], ref a[i]);           /* deleteMax */
                PercDown(a, 0, i);
            }
        }

        // Internal method for heapsort.
        // i is the index of an item in the heap.
        // returns the index of the left child.
        private static int LeftChild(int i)
        {
            return 2 * i + 1;
        }

        // Internal method for heapsort that is used in
        // deleteMax and buildHeap.
        // a is an array of AnyType items.
        // i is the position from which to percolate down.
        // n is the logical size of the binary heap.
        private static void PercDown<AnyType>(AnyType[] a, int i, int n) where AnyType : IComparable<AnyType>
        {
            int child;
            AnyType tmp;

            for (tmp = a[i]; LeftChild(i) < n; i = child)
            {
                child = LeftChild(i);
                if (child != n - 1 && a[child].CompareTo(a[child + 1]) < 0)
                    child++;
                if (tmp.CompareTo(a[child]) < 0)
                    a[i] = a[child];
                else
                    break;
            }
            a[i] = tmp;
        }

        // Mergesort algorithm.
        public static void MergeSort<AnyType>(AnyType[] a) where AnyType : IComparable<AnyType>
        {
            AnyType[] tmpArray = new AnyType[a.Length];
            MergeSort(a, tmpArray, 0, a.Length - 1);
        }

        // Internal method that makes recursive calls.
        // a is an array of AnyType items.
        // tmpArray is an array to place the merged result.
        // left is the left-most index of the subarray.
        // right is the right-most index of the subarray.
        private static void MergeSort<AnyType>(AnyType[] a, AnyType[] tmpArray,
                   int left, int right) where AnyType : IComparable<AnyType>
        {
            if (left < right)
            {
                int center = (left + right) / 2;
                MergeSort(a, tmpArray, left, center);
                MergeSort(a, tmpArray, center + 1, right);
                Merge(a, tmpArray, left, center + 1, right);
            }
        }

        // Internal method that merges two sorted halves of a subarray.
        // a is an array of AnyType items.
        // tmpArray is an array to place the merged result.
        // leftPos is the left-most index of the subarray.
        // rightPos is the index of the start of the second half.
        // rightEnd is the right-most index of the subarray.
        private static void Merge<AnyType>(AnyType[] a, AnyType[] tmpArray,
                                   int leftPos, int rightPos, int rightEnd) where AnyType : IComparable<AnyType>
        {
            int leftEnd = rightPos - 1;
            int tmpPos = leftPos;
            int numElements = rightEnd - leftPos + 1;

            // Main loop
            while (leftPos <= leftEnd && rightPos <= rightEnd)
                if (a[leftPos].CompareTo(a[rightPos]) <= 0)
                    tmpArray[tmpPos++] = a[leftPos++];
                else
                    tmpArray[tmpPos++] = a[rightPos++];

            while (leftPos <= leftEnd)    // Copy rest of first half
                tmpArray[tmpPos++] = a[leftPos++];

            while (rightPos <= rightEnd)  // Copy rest of right half
                tmpArray[tmpPos++] = a[rightPos++];

            // Copy tmpArray back
            for (int i = 0; i < numElements; i++, rightEnd--)
                a[rightEnd] = tmpArray[rightEnd];
        }

        // Quicksort algorithm.
        public static void Quicksort<AnyType>(AnyType[] a) where AnyType : IComparable<AnyType>
        {
            Quicksort(a, 0, a.Length - 1);
        }

        private const int CUTOFF = 10;

        // Internal quicksort method that makes recursive calls.
        // Uses median-of-three partitioning and a cutoff of 10.
        // a is an array of AnyType  items.
        // low is the left-most index of the subarray.
        // high is the right-most index of the subarray.
        private static void Quicksort<AnyType>(AnyType[] a, int low, int high) where AnyType : IComparable<AnyType>
        {
            if (low + CUTOFF > high)
                InsertionSort(a, low, high);
            else
            {
                // Sort low, middle, high
                int middle = (low + high) / 2;
                if (a[middle].CompareTo(a[low]) < 0)
                    Swap(ref a[low], ref a[middle]);
                if (a[high].CompareTo(a[low]) < 0)
                    Swap(ref a[low], ref a[high]);
                if (a[high].CompareTo(a[middle]) < 0)
                    Swap(ref a[middle], ref a[high]);

                // Place pivot at position high - 1
                Swap(ref a[middle], ref a[high - 1]);
                AnyType pivot = a[high - 1];

                // Begin partitioning
                int i, j;
                for (i = low, j = high - 1; ;)
                {
                    while (a[++i].CompareTo(pivot) < 0)
                        ;
                    while (pivot.CompareTo(a[--j]) < 0)
                        ;
                    if (i >= j)
                        break;
                    Swap(ref a[i], ref a[j]);
                }

                // Restore pivot
                Swap(ref a[i], ref a[high - 1]);

                Quicksort(a, low, i - 1);    // Sort small elements
                Quicksort(a, i + 1, high);   // Sort large elements
            }
        }

        // Internal insertion sort routine for subarrays
        // that is used by quicksort.
        // a is an array of IComparable items.
        // low is the left-most index of the subarray.
        // n the number of items to sort.
        private static void InsertionSort<AnyType>(AnyType[] a, int low, int high) where AnyType : IComparable<AnyType>
        {
            for (int p = low + 1; p <= high; p++)
            {
                AnyType tmp = a[p];
                int j;

                for (j = p; j > low && tmp.CompareTo(a[j - 1]) < 0; j--)
                    a[j] = a[j - 1];
                a[j] = tmp;
            }
        }

        // Quick selection algorithm.
        // Places the kth smallest item in a[k-1].
        // a is an array of AnyType items.
        // k is the desired rank (1 is minimum) in the entire array. 
        public static void QuickSelect<AnyType>(AnyType[] a, int k) where AnyType : IComparable<AnyType>
        {
            QuickSelect(a, 0, a.Length - 1, k);
        }

        // Internal selection method that makes recursive calls.
        // Uses median-of-three partitioning and a cutoff of 10.
        // Places the kth smallest item in a[k-1].
        // a is an array of AnyType items.
        // low is the left-most index of the subarray.
        // high is the right-most index of the subarray.
        // k is the desired rank (1 is minimum) in the entire array.
        private static void QuickSelect<AnyType>(AnyType[] a, int low, int high, int k) where AnyType : IComparable<AnyType>
        {
            if (low + CUTOFF > high)
                InsertionSort(a, low, high);
            else
            {
                // Sort low, middle, high
                int middle = (low + high) / 2;
                if (a[middle].CompareTo(a[low]) < 0)
                    Swap(ref a[low], ref a[middle]);
                if (a[high].CompareTo(a[low]) < 0)
                    Swap(ref a[low], ref a[high]);
                if (a[high].CompareTo(a[middle]) < 0)
                    Swap(ref a[middle], ref a[high]);

                // Place pivot at position high - 1
                Swap(ref a[middle], ref a[high - 1]);
                AnyType pivot = a[high - 1];

                // Begin partitioning
                int i, j;
                for (i = low, j = high - 1; ;)
                {
                    while (a[++i].CompareTo(pivot) < 0);
                    while (pivot.CompareTo(a[--j]) < 0);
                    if (i >= j)
                        break;
                    Swap(ref a[i], ref a[j]);
                }

                // Restore pivot
                Swap(ref a[i], ref a[high - 1]);

                // Recurse; only this part changes
                if (k <= i)
                    QuickSelect(a, low, i - 1, k);
                else if (k > i + 1)
                    QuickSelect(a, i + 1, high, k);
            }
        }
    }

}
