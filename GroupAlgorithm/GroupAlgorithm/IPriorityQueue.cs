namespace GroupAlgorithm
{    // IPriorityQueue interface
    // Some priority queues may support a decreaseKey operation,
    // but this is considered an advanced operation. If so,
    // an IPriorityQueuePosition is returned by insert.
    // Note that all "matching" is based on the CompareTo method.
    //
    // ******************PUBLIC OPERATIONS*********************
    // IPosition insert( x )  --> Insert x
    // Comparable deleteMin( )--> Return and remove smallest item
    // Comparable findMin( )  --> Return smallest item
    // boolean isEmpty( )     --> Return true if empty; else false
    // void makeEmpty( )      --> Remove all items
    // int size( )            --> Return size
    // void decreaseKey( p, v)--> Decrease value in p to v
    // ******************ERRORS********************************
    // Throws UnderflowException for FindMin and DeleteMin when empty
    // The IPriorityQueuePosition interface represents a type that
    // can be used for the decreaseKey operation.

    public interface IPriorityQueuePosition<AnyType>
    {
        // Returns the value stored at this position.
        AnyType GetValue( );
    }

    public interface IPriorityQueue<AnyType>
    {
        // Insert into the priority queue, maintaining heap order.
        // Duplicates are allowed.
        // x is the item to insert.
        // may return a Position useful for decreaseKey.
        IPriorityQueuePosition<AnyType> Insert( AnyType x );

        // Finds the smallest item in the priority queue.
        // Returns the smallest item.
        // Throws UnderflowException if empty.
        AnyType FindMin( );

        // Remove the smallest item from the priority queue.
        // Returns the smallest item.
        // Throws UnderflowException if empty.
        AnyType DeleteMin( );

        // Test if the priority queue is logically empty.
        // Returns true if empty, false otherwise.
        bool IsEmpty( );

        // Makes the priority queue logically empty.
        void MakeEmpty( );

        // Returns the size.
        int Size( );

        // Changes the value of the item stored in the pairing heap.
        // This is considered an advanced operation and might not
        // be supported by all priority queues. A priority queue
        // will signal its intention to not support decreaseKey by
        // having insert return null consistently.
        // p is any non-null Position returned by Insert.
        // newVal is the new value, which must be smaller
        //    than the currently stored value.
        // Throws ArgumentException if p invalid.
        // Throws InvalidOperationException if appropriate.
        void DecreaseKey( IPriorityQueuePosition<AnyType> p, AnyType newVal );
    }
}
