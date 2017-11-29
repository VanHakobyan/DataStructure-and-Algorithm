using System;
// BinaryNode class; stores a node in a tree;
// contains recursive routines to compute size and height.
//
// CONSTRUCTION: with (a) no parameters, or (b) an Object,
//     or (c) an Object, left child, and right child.
//
// *******************PUBLIC OPERATIONS**********************
// int Size( )            --> Return size of subtree at node
// int Height( )          --> Return height of subtree at node
// void PrintPostOrder( ) --> Print a postorder tree traversal
// void PrintInOrder( )   --> Print an inorder tree traversal
// void PrintPreOrder( )  --> Print a preorder tree traversal
// BinaryNode Duplicate( )--> Return a duplicate tree
namespace Three
{
    public class BinaryNode<AnyType>
    {
        public BinaryNode() : this(AnyType.default, null, null)
        {
        }

        public BinaryNode(AnyType theElement, BinaryNode<AnyType> lt, BinaryNode<AnyType> rt)
        {
            element = theElement;
            left = lt;
            right = rt;
        }

        // Return the size of the binary tree rooted at t.
        public static int Size(BinaryNode<AnyType> t)
        {
            if (t == null)
                return 0;
            else
                return 1 + Size(t.left) + Size(t.right);
        }

        // Return the height of the binary tree rooted at t.
        public static int Height(BinaryNode<AnyType> t)
        {
            if (t == null)
                return -1;
            else
                return 1 + Math.Max(Height(t.left), Height(t.right));
        }

        // Print tree rooted at current node using preorder traversal.
        public void PrintPreOrder()
        {
            Console.WriteLine(element); // Node
            if (left != null)
                left.PrintPreOrder(); // Left
            if (right != null)
                right.PrintPreOrder(); // Right
        }


        // Print tree rooted at current node using postorder traversal.
        public void PrintPostOrder()
        {
            if (left != null)
                left.PrintPostOrder(); // Left
            if (right != null)
                right.PrintPostOrder(); // Right
            Console.WriteLine(element); // Node
        }

        // Print tree rooted at current node using inorder traversal.
        public void PrintInOrder()
        {
            if (left != null)
                left.PrintInOrder(); // Left
            Console.WriteLine(element); // Node
            if (right != null)
                right.PrintInOrder(); // Right
        }


        // Return a reference to a node that is the root of a
        // duplicate of the binary tree rooted at the current node.
        public BinaryNode<AnyType> Duplicate()
        {
            BinaryNode<AnyType> root = new BinaryNode<AnyType>(element, null, null);

            if (left != null) // If there's a left subtree
                root.left = left.Duplicate(); // Duplicate; attach
            if (right != null) // If there's a right subtree
                root.right = right.Duplicate(); // Duplicate; attach
            return root; // Return resulting tree
        }

        public AnyType Element
        {
            get { return element; }

            set { element = value; }
        }

        public BinaryNode<AnyType> Left
        {
            get { return left; }

            set { left = value; }
        }

        public BinaryNode<AnyType> Right
        {
            get { return right; }
            set { right = value; }
        }

        private AnyType element;
        private BinaryNode<AnyType> left;
        private BinaryNode<AnyType> right;
    }

// BinaryTree class; stores a binary tree and illustrates the calling of
// BinaryNode recursive routines and Merge.
//
// CONSTRUCTION: with (a) no parameters or (b) an object to
//    be placed in the root of a one-element tree.
//
// *******************PUBLIC OPERATIONS**********************
// Various tree traversals, Size, Height, IsEmpty, MakeEmpty.
// Also, the following tricky method
// void Merge( Object root, BinaryTree t1, BinaryTree t2 )
//                        --> Construct a new tree
// *******************ERRORS*********************************
// Error message printed for illegal Merges.

    public class BinaryTree<AnyType>
    {
        public BinaryTree()
        {
            root = null;
        }

        public BinaryTree(AnyType rootItem)
        {
            root = new BinaryNode<AnyType>(rootItem, null, null);
        }

        public void PrintPreOrder()
        {
            if (root != null)
                root.PrintPreOrder();
        }

        public void PrintInOrder()
        {
            if (root != null)
                root.PrintInOrder();
        }

        public void PrintPostOrder()
        {
            if (root != null)
                root.PrintPostOrder();
        }

        public void MakeEmpty()
        {
            root = null;
        }

        public bool IsEmpty()
        {
            return root == null;
        }

        // Merge routine for BinaryTree class.
        // Forms a new tree from rootItem, t1 and t2.
        // Does not allow t1 and t2 to be the same.
        // Correctly handles other aliasing conditions.
        public void Merge(AnyType rootItem, BinaryTree<AnyType> t1, BinaryTree<AnyType> t2)
        {
            if (t1.root == t2.root && t1.root != null)
            {
                Console.Error.WriteLine("leftTree==rightTree; merge aborted");
                return;
            }

            // Allocate new node
            root = new BinaryNode<AnyType>(rootItem, t1.root, t2.root);

            // Ensure that every node is in one tree
            if (this != t1)
                t1.root = null;

            if (this != t2)
                t2.root = null;
        }

        public int Size()
        {
            return BinaryNode<AnyType>.Size(root);
        }

        public int Height()
        {
            return BinaryNode<AnyType>.Height(root);
        }

        public BinaryNode<AnyType> Root
        {
            get { return root; }
        }

        private BinaryNode<AnyType> root;

    }
}