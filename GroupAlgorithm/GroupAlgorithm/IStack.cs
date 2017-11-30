namespace GroupAlgorithm
{   // IStack interface
    //
    // ******************PUBLIC OPERATIONS*********************
    // void push( x )         --> Insert x
    // void pop( )            --> Remove most recently inserted item
    // AnyType top( )         --> Return most recently inserted item
    // AnyType topAndPop( )   --> Return and remove most recent item
    // boolean isEmpty( )     --> Return true if empty; else false
    // void makeEmpty( )      --> Remove all items
    // ******************ERRORS********************************
    // top, pop, or topAndPop on empty stack

    public interface IStack<AnyType>
    {
        // Insert x into the stack.
        void Push( AnyType x );

        // Remove the most recently inserted item from the stack.
        // Throws UnderflowException if the stack is empty.
        void Pop( );

        // Returns the most recently inserted item in the stack.
        // Throws UnderflowException if the stack is empty.
        AnyType Top( );

        // Returns and removes the most recently inserted item from the stack.
        // Throws UnderflowException if the stack is empty.
        AnyType TopAndPop( );

        // Returns true if empty, false otherwise.
        bool IsEmpty( );

        // Makes the stack logically empty.
        void MakeEmpty( );
    }
}
