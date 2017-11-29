using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BinarySearch
{
    // BinarySearchTree class
    // Implements an unbalanced binary search tree.
    // Note that all "matching" is based on the CompareTo method.
    //
    // CONSTRUCTION: with no initializer, or special value used to signal
    //    unsuccessful result from Find
    //
    // ******************PUBLIC OPERATIONS*********************
    // void insert( x )       --> Insert x
    // void remove( x )       --> Remove x
    // void removeMin( )      --> Remove minimum item
    // bool contains( x )     --> Return true if x in tree; else false
    // AnyType find( x )      --> Return item that matches x
    // AnyType findMin( )     --> Return smallest item
    // AnyType findMax( )     --> Return largest item
    // bool isEmpty( )        --> Return true if empty; else false
    // void makeEmpty( )      --> Remove all items
    // ******************ERRORS********************************
    // Exceptions are thrown by insert, remove, and removeMin if warranted

    public class BinarySearchTree<AnyType> where AnyType : IComparable
    {
        protected class BinaryNode
        {
            // Constructor
            public BinaryNode( AnyType theElement )
            {
                element = theElement;
                left = right = null;
            }

            public AnyType element;       // The data in the node
            public BinaryNode left;       // Left child
            public BinaryNode right;      // Right child
        }

        // Construct the tree.
        public BinarySearchTree( ) : this( AnyType.default )
        {
        }

        // Construct the tree, specifying the return value for failed Find
        public BinarySearchTree( AnyType notFound )
        {
            itemNotFound = notFound;
            root = null;
        }

       
        // Insert into the tree.
        // x is the item to insert.
        // Throws DuplicateItemException if x is already present.
        public void Insert( AnyType x )
        {
            Insert( x, ref root );
        }

        // Remove from the tree.
        // x is the item to remove.
        // Throws ItemNotFoundException if x is not found.
        public void Remove( AnyType x )
        {
            Remove( x, ref root );
        }

        // Remove minimum item from the tree.
        // Throws ItemNotFoundException if tree is empty.
        public void RemoveMin( )
        {
            RemoveMin( ref root );
        }

        // Returns smallest item or special value if empty.
        public AnyType FindMin( )
        {
            return ElementAt( FindMin( root ) );
        }

        // Returns the largest item or special value if empty.
        public AnyType FindMax( )
        {
            return ElementAt( FindMax( root ) );
        }

        // Find an item in the tree.
        // x is the item to search for.
        // return the matching item or special value if not found.
        public AnyType Find( AnyType x )
        {
            return ElementAt( Find( x, root ) );
        }

        // Returns true only if x is in the tree.
        public bool Contains( AnyType x )
        {
            return Find( x, root ) != null;
        }

        // Make the tree logically empty.
        public void MakeEmpty( )
        {
            root = null;
        }

        // Returns true if empty, false otherwise.
        public bool IsEmpty( )
        {
            return root == null;
        }

        // Internal method to get element field.
        // t is the node.
        // Returns the element field or special value if t is null.
        private AnyType ElementAt( BinaryNode t )
        {
            return t == null ? itemNotFound : t.element;
        }

        // Internal method to insert into a subtree.
        // x is the item to insert.
        // t the is node that roots the tree.
        // Throws DuplicateItemException if x is already present.
        protected virtual void Insert( AnyType x, ref BinaryNode t )
        {
            if( t == null )
                t = new BinaryNode( x );
            else if( x.CompareTo( t.element ) < 0 )
                Insert( x, ref t.left );
            else if( x.CompareTo( t.element ) > 0 )
                Insert( x, ref t.right );
            else
                throw new DuplicateItemException( x.ToString( ) );  // Duplicate
        }

        // Internal method to remove from a subtree.
        // x is the item to remove.
        // t the is node that roots the tree.
        // Throws DuplicateItemException if x is not already present.
        protected virtual void Remove( AnyType x, ref BinaryNode t )
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
                t = ( t.left != null ) ? t.left : t.right;
        }


        // Internal method to remove minimum item from a subtree.
        // t is the node that roots the tree.
        // Throws ItemNotFoundException if t is empty.
        protected virtual void RemoveMin( ref BinaryNode t )
        {
            if( t == null )
                throw new ItemNotFoundException( "RemoveMin on empty tree" );
            else if( t.left != null )
                RemoveMin( ref t.left );
            else
                t = t.right;
        }

        // Internal method to find the smallest item in a subtree.
        // t is the node that roots the tree.
        // Returns node containing the smallest item.
        protected virtual BinaryNode FindMin( BinaryNode t )
        {
            if( t != null )
                while( t.left != null )
                    t = t.left;

            return t;
        }

        // Internal method to find the largest item in a subtree.
        // t is the node that roots the tree.
        // Returns node containing the largest item.
        private BinaryNode FindMax( BinaryNode t )
        {
            if( t != null )
                while( t.right != null )
                    t = t.right;

            return t;
        }

        // Internal method to find an item in a subtree.
        // x is item to search for.
        // t is the node that roots the tree.
        // Returns node containing the matched item.
        private BinaryNode Find( AnyType x, BinaryNode t )
        {
            while( t != null )
            {
                if( x.CompareTo( t.element ) < 0 )
                    t = t.left;
                else if( x.CompareTo( t.element ) > 0 )
                    t = t.right;
                else
                    return t;    // Match
            }

            return null;         // Not found
        }

        // The tree root
        protected BinaryNode root;

        // Special return value for failed searches
        private AnyType itemNotFound;
        private object ;
        private object p;

        [Serializable]
        protected class ItemNotFoundException : Exception
        {
            public ItemNotFoundException(string v)
            {
            }

            public ItemNotFoundException(string message) : base(message)
            {
            }

            public ItemNotFoundException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected ItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }

    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string toString)
        {
            throw new NotImplementedException();
        }
    }
}