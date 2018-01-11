namespace BinarySearchWithRank
{
    public class BinaryNode<AnyType>
    {
        // Constructor
        public BinaryNode( AnyType theElement )
        {
            element = theElement;
            left = right = null;
        }

        internal AnyType element;               
        internal BinaryNode<AnyType> left;     
        internal BinaryNode<AnyType> right;    
    }
}
