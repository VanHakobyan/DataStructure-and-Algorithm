namespace GroupAlgorithm
{
    // IQueue interface
    //
    // ******************PUBLIC OPERATIONS*********************
    // void enqueue( x )      --> Insert x
    // AnyType getFront( )    --> Return least recently inserted item
    // AnyType dequeue( )     --> Return and remove least recent item
    // boolean isEmpty( )     --> Return true if empty; else false
    // void makeEmpty( )      --> Remove all items
    // ******************ERRORS********************************
    // getFront or dequeue on empty queue

    public interface IQueue<AnyType>
    {
        // Insert x into the queue.
        void Enqueue( AnyType x );

        // Returns the most recently inserted item in the queue.
        // Throws UnderflowException if the queue is empty.
        AnyType GetFront( );

        // Returns and removes the most recently inserted item from the queue.
        // Throws UnderflowException if the queue is empty.
        AnyType Dequeue( );

        // Returns true if empty, false otherwise.
        bool IsEmpty( );

        // Makes the queue logically empty.
        void MakeEmpty( );
    }
}
