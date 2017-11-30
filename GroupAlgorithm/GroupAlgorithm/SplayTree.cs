using System;

namespace GroupAlgorithm
{
    // SplayTree class
    // Implements a top-down splay tree.
    // Note that all "matching" is based on the CompareTo method.
    //
    // CONSTRUCTION: with no initializer
    //
    // ******************PUBLIC OPERATIONS*********************
    // void Insert( x )       --> Insert x
    // void Remove( x )       --> Remove x
    // AnyType Find( x )      --> Return item that matches x
    // bool Contains( x )     --> Returns true if x is present; else false
    // AnyType FindMin( )     --> Return smallest item
    // AnyType FindMax( )     --> Return largest item
    // bool IsEmpty( )        --> Return true if empty; else false
    // void MakeEmpty( )      --> Remove all items
    // ******************ERRORS********************************
    // Exceptions are thrown by insert and remove if warranted

    public class SplayTree<AnyType> where AnyType : System.Collections.Generic.IComparable<AnyType>
    {
        // Construct the tree.
        public SplayTree( )
        {
            nullNode = new BinaryNode<AnyType>( AnyType.default );
            nullNode.left = nullNode.right = nullNode;
            root = nullNode;
        }

        private BinaryNode<AnyType> newNode = null;  // Used between different inserts

        // Insert into the tree.
        // x is the item to insert.
        // Throws DuplicateItemException if x is already present.
        public void Insert( AnyType x )
        {
            if( newNode == null )
                newNode = new BinaryNode<AnyType>( AnyType.default );
            newNode.element = x;

            if( root == nullNode )
            {
                newNode.left = newNode.right = nullNode;
                root = newNode;
            }
            else
            {
                root = Splay( x, root );
                if( x.CompareTo( root.element ) < 0 )
                {
                    newNode.left = root.left;
                    newNode.right = root;
                    root.left = nullNode;
                    root = newNode;
                }
                else
                if( x.CompareTo( root.element ) > 0 )
                {
                    newNode.right = root.right;
                    newNode.left = root;
                    root.right = nullNode;
                    root = newNode;
                }
                else
                    throw new DuplicateItemException( x.ToString( ) );
            }
            newNode = null;   // So next insert will call new
        }

        // Remove from the tree.
        // x is the item to remove.
        // Throws ItemNotFoundException if x is not found.
        public void Remove( AnyType x )
        {
            BinaryNode<AnyType> newTree;

            // If x is found, it will be at the root
            root = Splay( x, root );
            if( root.element.CompareTo( x ) != 0 )
                throw new ItemNotFoundException( x.ToString( ) );

            if( root.left == nullNode )
                newTree = root.right;
            else
            {
                // Find the maximum in the left subtree
                // Splay it to the root; and then attach right child
                newTree = root.left;
                newTree = Splay( x, newTree );
                newTree.right = root.right;
            }
            root = newTree;
        }

        // Find the smallest item in the tree.
        // Not the most efficient implementation (uses two passes), but has correct
        //     amortized behavior.
        // A good alternative is to first call find with parameter
        //     smaller than any item in the tree, then call findMin.
        // Returns the smallest item or null if empty.
        public AnyType FindMin( )
        {
            if( IsEmpty( ) )
                return AnyType.default;

            BinaryNode<AnyType> ptr = root;

            while( ptr.left != nullNode )
                ptr = ptr.left;

            root = Splay( ptr.element, root );
            return ptr.element;
        }

        // Find the largest item in the tree.
        // Not the most efficient implementation (uses two passes), but has correct
        //     amortized behavior.
        // A good alternative is to first call find with parameter
        //     larger than any item in the tree, then call findMax.
        // Returns the largest item or null if empty.
        public AnyType FindMax( )
        {
            if( IsEmpty( ) )
                return AnyType.default;

            BinaryNode<AnyType> ptr = root;

            while( ptr.right != nullNode )
                ptr = ptr.right;

            root = Splay( ptr.element, root );
            return ptr.element;
        }

        // Finds an item in the tree.
        // x is the item to search for.
        // Returns the matching item or null if not found.
        public AnyType Find( AnyType x )
        {
            root = Splay( x, root );

            if( IsEmpty( ) || root.element.CompareTo( x ) != 0 )
                return AnyType.default;

            return root.element;
        }

        // Tests if an item is in the tree.
        // x is the item to search for.
        // Call is O(1) amortized time if immediately followed
        // or preceded by call to Find(x).
        public bool Contains( AnyType x )
        {
            root = Splay( x, root );
            return ( !IsEmpty( ) && root.element.CompareTo( x ) == 0 );
        }

        // Makes the tree logically empty.
        public void MakeEmpty( )
        {
            root = nullNode;
        }

        // Returns true if the tree is empty, false otherwise.
        public bool IsEmpty( )
        {
            return root == nullNode;
        }

        private BinaryNode<AnyType> header = new BinaryNode<AnyType>( AnyType.default ); // For splay

        // Internal method to perform a top-down splay.
        // The last accessed node becomes the new root.
        // x is the target item to splay around.
        // t is the root of the subtree to splay.
        // Returns the subtree after the splay.
        private BinaryNode<AnyType> Splay( AnyType x, BinaryNode<AnyType> t )
        {
            BinaryNode<AnyType> leftTreeMax, rightTreeMin;

            header.left = header.right = nullNode;
            leftTreeMax = rightTreeMin = header;

            nullNode.element = x;   // Guarantee a match

            for( ; ; )
                if( x.CompareTo( t.element ) < 0 )
                {
                    if( x.CompareTo( t.left.element ) < 0 )
                        Rotations.RotateWithLeftChild( ref t );
                    if( t.left == nullNode )
                        break;
                    // Link Right
                    rightTreeMin.left = t;
                    rightTreeMin = t;
                    t = t.left;
                }
                else if( x.CompareTo( t.element ) > 0 )
                {
                    if( x.CompareTo( t.right.element ) > 0 )
                        Rotations.RotateWithRightChild( ref t );
                    if( t.right == nullNode )
                        break;
                    // Link Left
                    leftTreeMax.right = t;
                    leftTreeMax = t;
                    t = t.right;
                }
                else
                    break;

            leftTreeMax.right = t.left;
            rightTreeMin.left = t.right;
            t.left = header.right;
            t.right = header.left;
            return t;
        }

        private BinaryNode<AnyType> root;
        private BinaryNode<AnyType> nullNode;
    }
}