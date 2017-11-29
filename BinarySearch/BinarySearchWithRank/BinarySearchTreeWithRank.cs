using System;
using BinarySearch;

namespace BinarySearchWithRank { 
    // BinarySearchTreeWithRank class
    // Implements an unbalanced binary search tree, with order statistics.
    // Note that all "matching" is based on the CompareTo method.
    //
    // CONSTRUCTION: with no initializer, or special value used to signal
    //    unsuccessful result from Find
    //
    // ******************PUBLIC OPERATIONS*********************
    // void Insert( x )       --> Insert x
    // void Remove( x )       --> Remove x
    // bool Contains( x )     --> Return true only if tree contains x
    // AnyType Find( x )      --> Return item that matches x
    // AnyType FindMin( )     --> Return smallest item
    // AnyType FindMax( )     --> Return largest item
    // AnyType FindKth( k )   --> Return kth smallest item
    // bool IsEmpty( )        --> Return true if empty; else false
    // void MakeEmpty( )      --> Remove all items
    // ******************ERRORS********************************
    // IllegalArgumentException thrown if k is out of bounds

    public class BinarySearchTreeWithRank<AnyType> : BinarySearchTree<AnyType> where AnyType : IComparable
    {
        protected class BinaryNodeWithSize : BinaryNode
        {
            public BinaryNodeWithSize( AnyType x ) : base( x )
            {
                size = 0;
            }

            public int size;
        }

        // Constructors
        public BinarySearchTreeWithRank( ) : base()
        {
        }

        public BinarySearchTreeWithRank( AnyType notFound ) : base( notFound )
        {
        }

        // Find the kth smallest item in the tree.
        // k is the desired rank (1 is the smallest item).
        // Returns the kth smallest item in the tree.
        // Throws ArgumentException if k is less
        //     than 1 or more than the size of the subtree.
        public AnyType FindKth( int k )
        {
            return FindKth( k, root ).element;
        }

        // Internal method to find kth smallest item in a subtree.
        // k is the desired rank (1 is the smallest item).
        // Returns the node containing the kth smallest item in the subtree.
        // Throws ArgumentException if k is less
        //    than 1 or more than the size of the subtree.
        protected BinaryNode FindKth( int k, BinaryNode t )
        {
            if( t == null )
                throw new ArgumentException( );
            int leftSize = ( t.left != null ) ? ( (BinaryNodeWithSize) t.left ).size : 0;

            if( k <= leftSize )
                return FindKth( k, t.left );
            if( k == leftSize + 1 )
                return t;
            return FindKth( k - leftSize - 1, t.right );
        }

        // Internal method to insert into a subtree.
        // x is the item to insert.
        // tt is the node that roots the tree.
        // Throws DuplicateItemException if x is already present.
        override protected void Insert( AnyType x, ref BinaryNode t )
        {
            if( t == null )
                t = new BinaryNodeWithSize( x );
            else if( x.CompareTo( t.element ) < 0 )
                Insert( x, ref t.left );
            else if( x.CompareTo( t.element ) > 0 )
                Insert( x, ref t.right );
            else
                throw new DuplicateItemException( x.ToString( ) );
            ( (BinaryNodeWithSize) t ).size++;
        }

        // Internal method to remove from a subtree.
        // x is the item to remove.
        // t is the node that roots the tree.
        // Throws ItemNotFoundException if x is not found.
        override protected void Remove( AnyType x, ref BinaryNode t )
        {
            if( t == null )
                throw new ItemNotFoundException( x.ToString( ) );
            if( x.CompareTo( t.element ) < 0 )
                Remove( x, ref t.left );
            else if( x.CompareTo( t.element ) > 0 )
                Remove( x, ref t.right );
            else if( t.left != null && t.right != null ) // Two children
            {
                t.element = FindMin( t.right ).element;
                RemoveMin( ref t.right );
            }
            else
            {
                t = ( t.left != null ) ? t.left : t.right;
                return;
            }
            ( (BinaryNodeWithSize) t ).size--;
        }


        // Internal method to remove the smallest item from a subtree,
        //     adjusting size fields as appropriate.
        // t is the node that roots the tree.
        // Throws ItemNotFoundExcetion if the subtree is empty.
        override protected void RemoveMin( ref BinaryNode t )
        {
            if( t == null )
                throw new ItemNotFoundException( "RemoveMin on empty tree" );
            if( t.left == null )
                t = t.right;
            else
            {
                RemoveMin( ref t.left );
                ( (BinaryNodeWithSize) t ).size--;
            }
        }

    }
}