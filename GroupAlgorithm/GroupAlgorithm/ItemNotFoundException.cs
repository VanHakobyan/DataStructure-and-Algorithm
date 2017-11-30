using System;

namespace GroupAlgorithm
{    // Exception class for failed finds/removes in search
    // trees, hash tables, and list and tree iterators.
    public class ItemNotFoundException : ApplicationException
    {
        // Construct this exception object.
        // message is the error message.
        public ItemNotFoundException( string message ) : base( message )
        {
        }
    }
}
