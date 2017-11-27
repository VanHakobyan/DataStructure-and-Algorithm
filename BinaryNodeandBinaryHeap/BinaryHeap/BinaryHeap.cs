using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeap
{
    // BinaryHeap class
    // Implements a binary heap.
    // Note that all "matching" is based on the CompareTo method.
    //
    // CONSTRUCTION: empty or with initial array.
    //
    // ******************PUBLIC OPERATIONS*********************
    // void Insert( x )       --> Insert x
    // AnyType DeleteMin( )   --> Return and remove smallest item
    // AnyType FindMin( )     --> Return smallest item
    // bool IsEmpty( )        --> Return true if empty; else false
    // void MakeEmpty( )      --> Remove all items
    // ******************ERRORS********************************
    // Throws UnderflowException for FindMin and DeleteMin when empty

    public class BinaryHeap<AnyType> : IPriorityQueue<AnyType> where AnyType : IComparable<AnyType>
    {
        // Construct the binary heap.
        public BinaryHeap()
        {
            currentSize = 0;
            array = new AnyType[DEFAULT_CAPACITY + 1];
        }

        // Construct the binary heap from an array.
        // items is the inital items in the binary heap.
        public BinaryHeap(AnyType[] items)
        {
            currentSize = items.Length;
            array = new AnyType[items.Length + 1];

            for (int i = 0; i < items.Length; i++)
                array[i + 1] = items[i];
            BuildHeap();
        }

        // Insert into the priority queue.
        // Duplicates are allowed.
        // x is the item to insert.
        // Returns null, signifying that DecreaseKey cannot be used.
        public IPriorityQueuePosition<AnyType> Insert(AnyType x)
        {
            if (currentSize + 1 == array.Length)
                DoubleArray();

            // Percolate up
            int hole = ++currentSize;
            array[0] = x;

            for (; x.CompareTo(array[hole / 2]) < 0; hole /= 2)
                array[hole] = array[hole / 2];
            array[hole] = x;

            return null;
        }

        // Throws InvalidOperationException because no IPriorityQueuePosition are returned
        // by the Insert method for BinaryHeap.
        public void DecreaseKey(IPriorityQueuePosition<AnyType> p, AnyType newVal)
        {
            throw new InvalidOperationException("Cannot use decreaseKey for binary heap");
        }

        // Returns the smallest item in the priority queue.
        // Returns the smallest item.
        // Throws UnderflowException if empty.
        public AnyType FindMin()
        {
            if (IsEmpty())
                throw new Exception("Empty binary heap");
            return array[1];
        }

        // Returns  and removesthe smallest item from the priority queue.
        // Throws UnderflowException if empty.
        public AnyType DeleteMin()
        {
            AnyType minItem = FindMin();
            array[1] = array[currentSize--];
            PercolateDown(1);

            return minItem;
        }

        // Establish heap order property from an arbitrary
        // arrangement of items. Runs in linear time.
        private void BuildHeap()
        {
            for (int i = currentSize / 2; i > 0; i--)
                PercolateDown(i);
        }

        // Test if the priority queue is logically empty.
        // Returns true if empty, false otherwise.
        public bool IsEmpty()
        {
            return currentSize == 0;
        }

        // Returns current size.
        public int Size()
        {
            return currentSize;
        }

        // Makes the priority queue logically empty.
        public void MakeEmpty()
        {
            currentSize = 0;
        }

        private const int DEFAULT_CAPACITY = 100;

        private int currentSize;      // Number of elements in heap
        private AnyType[] array;    // The heap array

        // Internal method to percolate down in the heap.
        // hole is the index at which the percolate begins.
        private void PercolateDown(int hole)
        {
            int child;
            AnyType tmp = array[hole];

            for (; hole * 2 <= currentSize; hole = child)
            {
                child = hole * 2;
                if (child != currentSize &&
                        array[child + 1].CompareTo(array[child]) < 0)
                    child++;
                if (array[child].CompareTo(tmp) < 0)
                    array[hole] = array[child];
                else
                    break;
            }
            array[hole] = tmp;
        }

        // Internal method to extend array.
        private void DoubleArray()
        {
            AnyType[] newArray;

            newArray = new AnyType[array.Length * 2];
            for (int i = 0; i < array.Length; i++)
                newArray[i] = array[i];
            array = newArray;
        }
    }

    /*
    class BinaryHeapTest
    {
            // Test program
        public static void Main( string [ ] args )
        {
            int numItems = 10000;
            BinaryHeap<int> h1 = new BinaryHeap<int>( );
            int [ ] items = new int[ numItems - 1 ];
            
            int i = 37;
            int j;

            for( i = 37, j = 0; i != 0; i = ( i + 37 ) % numItems, j++ )
            {
                h1.Insert( i );
                items[ j ] = i;
            }
                
            for( i = 1; i < numItems; i++ )
                if( h1.DeleteMin( ) != i )
                    Console.WriteLine( "Oops! " + i );  
                    
            BinaryHeap<int> h2 = new BinaryHeap<int>( items );    
            for( i = 1; i < numItems; i++ )
                if( h2.DeleteMin( ) != i )
                    Console.WriteLine("Oops! " + i);     
     
            Console.WriteLine("Binary Heap Test over");
        }
    }
    */
}
