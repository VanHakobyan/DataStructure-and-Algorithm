namespace BinarySearchWithRank
{
    // Basic node stored in unbalanced binary search trees
    public class BinaryNode<AnyType>
    {
        // Constructor
        public BinaryNode( AnyType theElement )
        {
            element = theElement;
            left = right = null;
        }

        internal AnyType element;                // The data in the node
        internal BinaryNode<AnyType> left;      // Left child
        internal BinaryNode<AnyType> right;     // Right child
    }
}