using System;

namespace GroupAlgorithm
{

    // Exception class for access in empty containers
    // such as stacks, queues, and priority queues.
    public class UnderflowException : ApplicationException
    {
        // Construct this exception object.
        // message is the error message.
        public UnderflowException( string message ) : base( message )
        {
        }
    }
}
