namespace GroupAlgorithm
{ // A class for simulating an integer memory cell
    public class IntCell
    {
        // Get the stored value.
        public int Read()
        {
            return storedValue;
        }

        // Store a value
        public void Write(int x)
        {
            storedValue = x;
        }

        private int storedValue;
    }
}