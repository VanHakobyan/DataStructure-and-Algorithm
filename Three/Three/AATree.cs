using System;

namespace Three { 

    // AATree class
    //
    // Implements an AA-tree.
    // Note that all "matching" is based on the CompareTo method.
    //
    // CONSTRUCTION: with no initializer or with special value that
    //  represents result of a failed Find.
    //
    // ******************PUBLIC OPERATIONS*********************
    // void Insert( x )       --> Insert x
    // void Remove( x )       --> Remove x
    // Comparable Find( x )   --> Return item that matches x
    // Comparable FindMin( )  --> Return smallest item
    // Comparable FindMax( )  --> Return largest item
    // boolean IsEmpty( )     --> Return true if empty; else false
    // void MakeEmpty( )      --> Remove all items
    // ******************ERRORS********************************
    // Exceptions are thrown by insert and remove if warranted

/**
* Implements an AA-tree.
* Note that all "matching" is based on the compareTo method.
* @author Mark Allen Weiss
*/
    public class AATree<AnyType> where AnyType : System.Collections.Generic.IComparable<AnyType>
    {
        // Construct the tree.
        public AATree( AnyType notFound )
        {
            nullNode = new AANode( notFound, null, null );
            nullNode.left = nullNode.right = nullNode;
            nullNode.level = 0;
            root = nullNode;
            itemNotFound = notFound;
        }

        public AATree( ) : this( AnyType.default )
        {
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
            deletedNode = nullNode;
            Remove( x, ref root );
        }

        // Returns the smallest item or special value if empty.
        public AnyType FindMin( )
        {
            if( IsEmpty( ) )
                return itemNotFound;

            AANode ptr = root;

            while( ptr.left != nullNode )
                ptr = ptr.left;

            return ptr.element;
        }

        // Returns the largest item or special value if empty.
        public AnyType FindMax( )
        {
            if( IsEmpty( ) )
                return itemNotFound;

            AANode ptr = root;

            while( ptr.right != nullNode )
                ptr = ptr.right;

            return ptr.element;
        }

        // Find an item in the tree.
        // x is the item to search for.
        // Returns the matching item of special value if not found.
        public AnyType Find( AnyType x )
        {
            AANode current = root;
            nullNode.element = x;

            for( ; ; )
            {
                if( x.CompareTo( current.element ) < 0 )
                    current = current.left;
                else if( x.CompareTo( current.element ) > 0 ) 
                    current = current.right;
                else
                    return current.element;
            }
        }

        // Find an item in the tree.
        // x is the item to search for.
        // Returns the matching item of special value if not found.
        public bool Contains( AnyType x )
        {
            AANode current = root;
            nullNode.element = x;

            for( ; ; )
            {
                if( x.CompareTo( current.element ) < 0 )
                    current = current.left;
                else if( x.CompareTo( current.element ) > 0 ) 
                    current = current.right;
                else
                    return current != nullNode;
            }
        }

        // Make the tree logically empty.
        public void MakeEmpty( )
        {
            root = nullNode;
        }

        // Returns true if empty, false otherwise.
        public bool IsEmpty( )
        {
            return root == nullNode;
        }

        // Internal method to insert into a subtree.
        // x is the item to insert.
        // t is the node that roots the tree.
        // Throws DuplicateItemException if x is already present.
        private void Insert( AnyType x, ref AANode t )
        {
            if( t == nullNode )
                t = new AANode( x, nullNode, nullNode );
            else if( x.CompareTo( t.element ) < 0 )
                Insert( x, ref t.left );
            else if( x.CompareTo( t.element ) > 0 )
                Insert( x, ref t.right );
            else
                throw new DuplicateItemException( x.ToString( ) );

            Skew( ref t );
            Split( ref t );
        }

        // Internal method to remove from a subtree.
        // x is the item to remove.
        // t is the node that roots the tree.
        // Throws ItemNotFoundException if x is not found.
        private void Remove( AnyType x, ref AANode t )
        {
            if( t != nullNode )
            {
                // Step 1: Search down the tree and set lastNode and deletedNode
                lastNode = t;
                if( x.CompareTo( t.element ) < 0 )
                    Remove( x, ref t.left );
                else
                {
                    deletedNode = t;
                    Remove( x, ref t.right );
                }

                // Step 2: If at the bottom of the tree and
                //         x is present, we remove it
                if( t == lastNode )
                {
                    if( deletedNode == nullNode || x.CompareTo( deletedNode.element ) != 0 )
                        throw new ItemNotFoundException( x.ToString( ) );
                    deletedNode.element = t.element;
                    t = t.right;
                }

                // Step 3: Otherwise, we are not at the bottom; rebalance
                else
                    if( t.left.level < t.level - 1 || t.right.level < t.level - 1 )
                    {
                        if( t.right.level > --t.level )
                            t.right.level = t.level;
                        Skew( ref t );
                        Skew( ref t.right );
                        Skew( ref t.right.right );
                        Split( ref t );
                        Split( ref t.right );
                    }
            }
        }

        // Skew primitive for AA-trees.
        // t is the node that roots the tree.
        private static void Skew( ref AANode t )
        {
            if( t.left.level == t.level )
                RotateWithLeftChild( ref t );
        }

        // Split primitive for AA-trees.
        // t is the node that roots the tree.
        private static void Split( ref AANode t )
        {
            if( t.right.right.level == t.level )
            {
                RotateWithRightChild( ref t );
                t.level++;
            }
        }

        // Rotate binary tree node with left child.
        private static void RotateWithLeftChild( ref AANode k2 )
        {
            AANode k1 = k2.left;
            k2.left = k1.right;
            k1.right = k2;
            k2 = k1;
        }

        // Rotate binary tree node with right child.
        private static void RotateWithRightChild( ref AANode k1 )
        {
            AANode k2 = k1.right;
            k1.right = k2.left;
            k2.left = k1;
            k1 = k2;
        }

        private class AANode
        {
                // Constructors
            public AANode(AnyType theElement, AANode lt, AANode rt)
            {
                element = theElement;
                left    = lt;
                right   = rt;
                level   = 1;
            }

            public AnyType  element;      // The data in the node
            public AANode left;         // Left child
            public AANode right;        // Right child
            public int level;        // Level
        }
        
        private AANode root;
        private AANode nullNode;
        private AnyType itemNotFound;
        
        private AANode deletedNode;
        private AANode lastNode;
    }

}

