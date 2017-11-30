namespace Generics.Class
{
// MemoryCell class
//  AnyType Read( )         -->  Returns the stored value
//  void Write( AnyType x ) -->  x is stored
    public class GenericMemoryCell<AnyType>
    {
        // Public methods
        public AnyType Read()
        {
            return storedValue;
        }

        public void Write(AnyType x)
        {
            storedValue = x;
        }

        // Private internal data representation
        private AnyType storedValue;
    }
}