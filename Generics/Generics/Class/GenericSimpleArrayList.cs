using System;
namespace Generics.Class
{

    // The StringArrayList implements a growable array of String.
    // Insertions are always done at the end.
    public class GenericSimpleArrayList<AnyType>
    {

        // Construct an empty ArrayList.
        public GenericSimpleArrayList()
        {
            Clear();
        }

        // Returns the number of items in this collection.
        public int Count
        {
            get
            {
                return theSize;
            }
        }

        // Allow indexing using []
        public AnyType this[int idx]
        {
            get
            {
                if (idx < 0 || idx >= Count)
                    throw new IndexOutOfRangeException("Index " + idx + "; size " + Count);

                return theItems[idx];
            }
            // Changes the item at position idx.
            set
            {
                if (idx < 0 || idx >= Count)
                    throw new IndexOutOfRangeException("Index " + idx + "; size " + Count);

                AnyType old = theItems[idx];

                theItems[idx] = value;
            }
        }


        // Adds an item to this collection, at the end.
        public int Add(AnyType x)
        {
            if (theItems.Length == Count)
            {
                AnyType[] old = theItems;
                theItems = new AnyType[theItems.Length * 2 + 1];
                for (int i = 0; i < Count; i++)
                    theItems[i] = old[i];
            }

            theItems[theSize++] = x;

            return theSize - 1;
        }

        /**
         * Removes an item from this collection.
         * @param idx the index of the object.
         * @return the item was removed from the collection.
         */
        public void RemoveAt(int idx)
        {
            AnyType removedItem = theItems[idx];

            for (int i = idx; i < Count - 1; i++)
                theItems[i] = theItems[i + 1];
            theSize--;
        }

        /**
         * Change the size of this collection to zero.
         */
        public void Clear()
        {
            theSize = 0;
            theItems = new AnyType[INIT_CAPACITY];
        }

        private const int INIT_CAPACITY = 10;
        private const int NOT_FOUND = -1;

        private int theSize;
        private AnyType[] theItems;

    }
}