using System;
using System.Collections.Generic;

namespace GroupAlgorithm
{
    // SortedLinkedList class
    //
    // Linked list implementation of the list using a header node.
    // Access to the list is via LinkedListIterator.
    //
    // CONSTRUCTION: with no initializer
    //
    // ******************PUBLIC OPERATIONS*********************
    // bool IsEmpty( )        --> Return true if empty; else false
    // void MakeEmpty( )      --> Remove all items
    // LinkedListIterator Zeroth( )
    //                        --> Return position to prior to first
    // LinkedListIterator First( )
    //                        --> Return first position
    // void Insert( x )       --> Insert x
    // void Insert( x, p )    --> Insert x (ignore p)
    // void Remove( x )       --> Remove x
    // LinkedListIterator Find( x )
    //                        --> Return position that views x
    // LinkedListIterator FindPrevious( x )
    //                        --> Return position prior to x
    // ******************ERRORS********************************
    // No special errors

    public class SortedLinkedList<AnyType> : LinkedList<AnyType> where AnyType : IComparable
    {
        // Insert after p.
        // x is the item to insert.
        // p is this parameter is ignored.
        public override void Insert( AnyType x, LinkedListIterator<AnyType> p )
        {
            Insert( x );
        }

        // Insert in sorted order.
        // x is the item to insert.
        public void Insert( AnyType x )
        {
            LinkedListIterator<AnyType> prev = Zeroth( );
            LinkedListIterator<AnyType> curr = First( );

            while( curr.IsValid( ) && x.CompareTo( curr.Retrieve( ) ) > 0 )
            {
                prev.Advance( );
                curr.Advance( );
            }

            base.Insert( x, prev );
        }
    }
}
