using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryNode
{
    public class BinaryNode<AnyType>
    {
        // Constructor
        public BinaryNode(AnyType theElement)
        {
            element = theElement;
            left = right = null;
        }

        internal AnyType element;                // The data in the node
        internal BinaryNode<AnyType> left;      // Left child
        internal BinaryNode<AnyType> right;     // Right child
    }
}
