using InvalidOperationException = System.InvalidOperationException;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;
using ArgumentException = System.ArgumentException;

namespace Generics.Class
{
    public struct KeyValuePair<KeyType, ValueType>
    {
        public KeyValuePair( KeyType k, ValueType v )
        {
            key = k;
            val = v;
        }

        public KeyType Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        public ValueType Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
            }
        }

        private KeyType key;
        private ValueType val;
    }

    /*
    public interface IComparable<AnyType>
    {
        int CompareTo( AnyType other );
    }
    */

    public interface IComparer<AnyType>
    {
        int Compare( AnyType lhs, AnyType rhs );
    }

    public interface IKeyComparer<AnyType> : IComparer<AnyType>
    {
        bool Equals( AnyType lhs, AnyType rhs );
        int GetHashCode( AnyType obj );
    }

    public interface IEnumerator<AnyType>
    {
        bool MoveNext( );

        AnyType Current
        {
            get;
        }
    }

    public interface IEnumerable<AnyType>
    {
        IEnumerator<AnyType> GetEnumerator( );
    }

    public interface ICollection<AnyType> : IEnumerable<AnyType>
    {
        void CopyTo( AnyType[ ] array );

        int Count
        {
            get;
        }
    }

    public interface IList<AnyType> : ICollection<AnyType>, IEnumerable<AnyType>
    {
        int Add( AnyType item );
        void Clear( );
        bool Contains( AnyType item );
        int IndexOf( AnyType item );
        void Insert( int index, AnyType item );
        void Remove( AnyType item );
        void RemoveAt( int index );

        AnyType this[ int index ]
        {
            get;
            set;
        }
    }

    public interface IDictionary<KeyType, ValueType> : ICollection<KeyValuePair<KeyType, ValueType>>, IEnumerable<KeyValuePair<KeyType, ValueType>>
    {
        void Add( KeyType key, ValueType val );
        void Clear( );
        bool ContainsKey( KeyType key );
        bool Remove( KeyType key );

        ValueType this[ KeyType key ]
        {
            get;
            set;
        }

        ICollection<KeyType> Keys
        {
            get;
        }

        ICollection<ValueType> Values
        {
            get;
        }

    }

    public class Stack<AnyType>
    {
        // Construct the stack.
        public Stack( )
        {
            theArray = new AnyType[ DEFAULT_CAPACITY ];
            topOfStack = -1;
        }
        // Makes the stack logically empty.
        public void Clear( )
        {
            topOfStack = -1;
        }
        // Return the most recently inserted item in the stack.
        // Throws UnderflowException if the stack is empty.
        public AnyType Peek( )
        {
            if( IsEmpty( ) )
                throw new InvalidOperationException( "ArrayStack top" );

            return theArray[ topOfStack ];
        }

        // Returns and remove the most recently inserted item from the stack.
        // Tthrows Underflow if the stack is empty.
        public AnyType Pop( )
        {
            if( IsEmpty( ) )
                throw new InvalidOperationException( "ArrayStack topAndPop" );

            return theArray[ topOfStack-- ];
        }
        // Insert a x into the stack.
        public void Push( AnyType x )
        {
            if( topOfStack + 1 == theArray.Length )
                DoubleArray( );

            theArray[ ++topOfStack ] = x;
        }

        private bool IsEmpty( )
        {
            return topOfStack == -1;
        }

        // Internal method to extend theArray.
        private void DoubleArray( )
        {
            AnyType[ ] newArray;

            newArray = new AnyType[ theArray.Length * 2 ];
            for( int i = 0; i < theArray.Length; i++ )
                newArray[ i ] = theArray[ i ];

            theArray = newArray;
        }

        public int Count
        {
            get
            {
                return topOfStack + 1;
            }
        }

        private AnyType[ ] theArray;
        private int topOfStack;
        private const int DEFAULT_CAPACITY = 10;
    }

    public class Queue<AnyType>
    {
        // Construct the queue.
        public Queue( )
        {
            theArray = new AnyType[ DEFAULT_CAPACITY ];
            Clear( );
        }
        // Tests if the queue is logically empty.
        // Returns true if empty, false otherwise.
        public int Count
        {
            get
            {
                return currentSize;
            }
        }

        private bool IsEmpty( )
        {
            return Count == 0;
        }

        // Makes the queue logically empty.
        public void Clear( )
        {
            currentSize = 0;
            front = 0;
            back = -1;
        }
        // Removes and returns the least recently inserted item from the queue.
        // Throws UnderflowException if the queue is empty.
        public AnyType Dequeue( )
        {
            if( IsEmpty( ) )
                throw new InvalidOperationException( "ArrayQueue dequeue" );

            currentSize--;

            AnyType returnValue = theArray[ front ];

            Increment( ref front );
            return returnValue;
        }
        // Returns the least recently inserted item in the queue.
        // Throws UnderflowException if the queue is empty.
        public AnyType Peek( )
        {
            if( IsEmpty( ) )
                throw new InvalidOperationException( "ArrayQueue getFront" );

            return theArray[ front ];
        }
        // Inserts x into the queue.
        public void Enqueue( AnyType x )
        {
            if( currentSize == theArray.Length )
                DoubleQueue( );

            Increment( ref back );
            theArray[ back ] = x;
            currentSize++;
        }
        // Internal method to increment with wraparound.
        // x is any index in theArray's range.
        // Returns x+1, or 0 if x is at the end of theArray.
        private void Increment( ref int x )
        {
            if( ++x == theArray.Length )
                x = 0;
        }
        // Internal method to expand theArray.
        private void DoubleQueue( )
        {
            AnyType[ ] newArray = new AnyType[ theArray.Length * 2 + 1 ];

            // Copy elements that are logically in the queue
            for( int i = 0; i < currentSize; i++, Increment( ref front ) )
                newArray[ i ] = theArray[ front ];

            theArray = newArray;
            front = 0;
            back = currentSize - 1;
        }
        private AnyType[ ] theArray;
        private int currentSize;
        private int front;
        private int back;
        private const int DEFAULT_CAPACITY = 10;
    }

    public abstract class AbstractCollection<AnyType> : ICollection<AnyType>
    {
        public void CopyTo( AnyType[ ] array )
        {
            IEnumerator<AnyType> itr = GetEnumerator( );
            int i = 0;

            while( itr.MoveNext( ) )
                array[ i++ ] = itr.Current;
        }

        public AnyType[ ] ToArray( )
        {
            AnyType[ ] array = new AnyType[ Count ];
            CopyTo( array );
            return array;
        }

        public abstract IEnumerator<AnyType> GetEnumerator( );

        public int Count
        {
            get
            {
                return count;
            }
        }

        protected void IncrementCount( )
        {
            count++;
        }

        protected void DecrementCount( )
        {
            count--;
        }

        protected void ZeroCount( )
        {
            count = 0;
        }

        private int count = 0;
    }


    public class List<AnyType> : AbstractCollection<AnyType>, IList<AnyType>
    {
        public struct Enumerator : IEnumerator<AnyType>
        {
            public Enumerator( List<AnyType> lst )
            {
                theList = lst;
                current = -1;
                expectedModCount = theList.modCount;
            }

            public bool MoveNext( )
            {
                CheckForConcurrentModification( );
                if( current < theList.Count - 1 )
                {
                    current++;
                    return true;
                }
                else
                    return false;
            }

            public AnyType Current
            {
                get
                {
                    CheckForConcurrentModification( );
                    return theList[ current ];
                }
            }

            private void CheckForConcurrentModification( )
            {
                if( expectedModCount != theList.modCount )
                    throw new InvalidOperationException( );
            }

            private int current;
            private int expectedModCount;
            private List<AnyType> theList;
        }

        public List( )
        {
            Clear( );
        }

        public override IEnumerator<AnyType> GetEnumerator( )
        {
            return new Enumerator( this );
        }

        public int Add( AnyType item )
        {
            Insert( Count, item );
            return Count - 1;
        }

        private void ExpandIfNeeded( )
        {
            if( theItems.Length == Count )
            {
                AnyType[ ] old = theItems;

                theItems = new AnyType[ Count * 2 + 1 ];
                for( int i = 0; i < Count; i++ )
                    theItems[ i ] = old[ i ];
            }
        }

        public void Clear( )
        {
            ZeroCount( );
            theItems = new AnyType[ DEFAULT_CAPACITY ];
            modCount++;
        }
        public bool Contains( AnyType item )
        {
            return IndexOf( item ) != NOT_FOUND;
        }

        public int IndexOf( AnyType item )
        {
            for( int i = 0; i < Count; i++ )
                if( item.Equals( theItems[ i ] ) )
                    return i;

            return NOT_FOUND;
        }
        public void Insert( int index, AnyType item )
        {
            ExpandIfNeeded( );

            if( index < 0 || index > Count )
                throw new ArgumentOutOfRangeException( );

            for( int i = Count; i > index; i-- )
                theItems[ i ] = theItems[ i - 1 ];
            theItems[ index ] = item;
            IncrementCount( );
            modCount++;
        }
        public void Remove( AnyType item )
        {
            int index = IndexOf( item );

            if( index != NOT_FOUND )
                RemoveAt( index );
        }
        public void RemoveAt( int index )
        {
            if( index < 0 || index >= Count )
                throw new ArgumentOutOfRangeException( );

            for( int i = index; i < Count - 1; i++ )
                theItems[ i ] = theItems[ i + 1 ];

            DecrementCount( );
            modCount++;
        }
        public AnyType this[ int index ]
        {
            get
            {
                if( index < 0 || index >= Count )
                    throw new ArgumentOutOfRangeException( );
                return theItems[ index ];
            }

            set
            {
                if( index < 0 || index >= Count )
                    throw new ArgumentOutOfRangeException( );
                theItems[ index ] = value;
            }
        }

        private int modCount = 0;
        private AnyType[ ] theItems;

        private const int DEFAULT_CAPACITY = 10;
        private const int NOT_FOUND = -1;
    }

    public abstract class AbstractDictionary<KeyType, ValueType> : AbstractCollection<KeyValuePair<KeyType, ValueType>>, IDictionary<KeyType, ValueType>
    {
        public abstract void Add( KeyType key, ValueType val );
        public abstract void Clear( );
        public abstract bool ContainsKey( KeyType key );
        public abstract bool Remove( KeyType key );

        public abstract ValueType this[ KeyType key ]
        {
            get;
            set;
        }

        private class KeyCollection : AbstractCollection<KeyType>
        {
            private struct Enumerator : IEnumerator<KeyType>
            {
                public Enumerator( IEnumerator<KeyValuePair<KeyType, ValueType>> enumerator )
                {
                    theEnumerator = enumerator;
                }
                public bool MoveNext( )
                {
                    return theEnumerator.MoveNext( );
                }
                public KeyType Current
                {
                    get
                    {
                        return theEnumerator.Current.Key;
                    }
                }

                private IEnumerator<KeyValuePair<KeyType, ValueType>> theEnumerator;
            }

            public KeyCollection( IDictionary<KeyType, ValueType> dict )
            {
                dictionary = dict;
            }

            public override IEnumerator<KeyType> GetEnumerator( )
            {
                return new Enumerator( dictionary.GetEnumerator( ) );
            }

            private IDictionary<KeyType, ValueType> dictionary;
        }

        private class ValueCollection : AbstractCollection<ValueType>
        {
            private struct Enumerator : IEnumerator<ValueType>
            {
                public Enumerator( IEnumerator<KeyValuePair<KeyType, ValueType>> enumerator )
                {
                    theEnumerator = enumerator;
                }
                public bool MoveNext( )
                {
                    return theEnumerator.MoveNext( );
                }
                public ValueType Current
                {
                    get
                    {
                        return theEnumerator.Current.Value;
                    }
                }

                private IEnumerator<KeyValuePair<KeyType, ValueType>> theEnumerator;
            }
            public ValueCollection( IDictionary<KeyType, ValueType> dict )
            {
                dictionary = dict;
            }
            public override IEnumerator<ValueType> GetEnumerator( )
            {
                return new Enumerator( dictionary.GetEnumerator( ) );
            }
            private IDictionary<KeyType, ValueType> dictionary;
        }

        public ICollection<KeyType> Keys
        {
            get
            {
                return new KeyCollection( this );
            }
        }
        public ICollection<ValueType> Values
        {
            get
            {
                return new ValueCollection( this );
            }
        }
    }

    public class Dictionary<KeyType, ValueType> : AbstractDictionary<KeyType, ValueType>
    {
        private class HashEntry
        {
            public KeyValuePair<KeyType, ValueType> element;
            public bool isActive;

            public HashEntry( KeyValuePair<KeyType, ValueType> e )
                : this( e, true )
            {
            }
            public HashEntry( KeyValuePair<KeyType, ValueType> e, bool i )
            {
                element = e;
                isActive = i;
            }
        }

        public struct Enumerator : IEnumerator<KeyValuePair<KeyType, ValueType>>
        {
            public bool MoveNext( )
            {
                CheckForConcurrentModification( );
                if( visited == theDictionary.Count )
                    return false;

                do
                {
                    currentPos++;
                } while( currentPos < theDictionary.array.Length && !Dictionary<KeyType, ValueType>.IsActive( theDictionary.array, currentPos ) );

                visited++;
                return true;
            }

            public KeyValuePair<KeyType, ValueType> Current
            {
                get
                {
                    CheckForConcurrentModification( );
                    return theDictionary.array[ currentPos ].element;
                }
            }

            public Enumerator( Dictionary<KeyType, ValueType> dict )
            {
                theDictionary = dict;
                expectedModCount = theDictionary.modCount;
                currentPos = -1;
                visited = 0;
            }

            private void CheckForConcurrentModification( )
            {
                if( expectedModCount != theDictionary.modCount )
                    throw new InvalidOperationException( );
            }

            private Dictionary<KeyType, ValueType> theDictionary;
            private int expectedModCount;
            private int currentPos;
            private int visited;
        }

        public override IEnumerator<KeyValuePair<KeyType, ValueType>> GetEnumerator( )
        {
            return new Enumerator( this );
        }

        public Dictionary( )
        {
            AllocateArray( DEFAULT_TABLE_SIZE );
            Clear( );
        }

        public Dictionary( IDictionary<KeyType, ValueType> other )
        {
            AllocateArray( other.Count * 2 );
            Clear( );
            foreach( KeyValuePair<KeyType, ValueType> pair in other )
                Add( pair );
        }

        private void AllocateArray( int arraySize )
        {
            array = new HashEntry[ arraySize ];
        }

        public override void Add( KeyType key, ValueType val )
        {
            int currentPos = FindPos( key );
            if( IsActive( array, currentPos ) )
            {
                array[ currentPos ].element.Value = val;
                return;
            }

            array[ currentPos ] = new HashEntry( new KeyValuePair<KeyType, ValueType>( key, val ) );
            IncrementCount( );
            occupied++;
            modCount++;

            if( occupied > array.Length / 2 )
                Rehash( );
        }

        private void Add( KeyValuePair<KeyType, ValueType> pair )
        {
            Add( pair.Key, pair.Value );
        }

        public override void Clear( )
        {
            ZeroCount( );
            occupied = 0;
            modCount++;
            for( int i = 0; i < array.Length; i++ )
                array[ i ] = null;
        }

        public override bool ContainsKey( KeyType key )
        {
            return IsActive( array, FindPos( key ) );
        }

        private static bool IsActive( HashEntry[ ] arr, int pos )
        {
            return arr[ pos ] != null && arr[ pos ].isActive;
        }

        private int FindPos( KeyType key )
        {
            int collisionNum = 0;
            int currentPos = System.Math.Abs( key.GetHashCode( ) % array.Length );

            while( array[ currentPos ] != null )
            {
                if( key.Equals( array[ currentPos ].element.Key ) )
                    break;

                currentPos += 2 * ++collisionNum - 1;
                if( currentPos >= array.Length )
                    currentPos -= array.Length;
            }

            return currentPos;
        }

        public override bool Remove( KeyType key )
        {
            int currentPos = FindPos( key );

            if( !IsActive( array, currentPos ) )
                return false;

            array[ currentPos ].isActive = false;
            DecrementCount( );
            modCount++;

            if( Count < array.Length / 8 )
                Rehash( );
            return true;
        }

        private void Rehash( )
        {
            HashEntry[ ] oldArray = array;

            AllocateArray( NextPrime( 4 * Count ) );
            ZeroCount( );
            occupied = 0;

            for( int i = 0; i < oldArray.Length; i++ )
                if( IsActive( oldArray, i ) )
                    Add( oldArray[ i ].element );
        }
        public override ValueType this[ KeyType key ]
        {
            get
            {
                int currentPos = FindPos( key );
                if( IsActive( array, currentPos ) )
                    return array[ currentPos ].element.Value;
                else
                    throw new ArgumentException( "Key not present in dictionary" );
            }

            set
            {
                int currentPos = FindPos( key );
                if( IsActive( array, currentPos ) )
                    array[ currentPos ].element.Value = value;
                else
                    Add( key, value );
            }
        }

        private static int NextPrime( int n )
        {
            if( n % 2 == 0 )
                n++;

            for( ; !IsPrime( n ); n += 2 )
                ;

            return n;
        }

        private static bool IsPrime( int n )
        {
            if( n == 2 || n == 3 )
                return true;

            if( n == 1 || n % 2 == 0 )
                return false;

            for( int i = 3; i * i <= n; i += 2 )
                if( n % i == 0 )
                    return false;

            return true;
        }

        private HashEntry[ ] array;
        private int occupied;
        private int modCount;

        public const int DEFAULT_TABLE_SIZE = 11;
    }

    public class SortedDictionary<KeyType, ValueType> : AbstractDictionary<KeyType, ValueType>
    {
        public struct Enumerator : IEnumerator<KeyValuePair<KeyType, ValueType>>
        {
            private SortedDictionary<KeyType, ValueType> theDictionary;
            private int expectedModCount;
            private int visited;
            private Stack<AANode> path;
            private AANode current;
            private AANode nullNode;
            private AANode root;

            public Enumerator( SortedDictionary<KeyType, ValueType> dict )
            {
                theDictionary = dict;
                visited = 0;
                path = new Stack<AANode>( );
                current = null;
                expectedModCount = theDictionary.modCount;
                nullNode = theDictionary.nullNode;
                root = theDictionary.root;

            }

            public bool MoveNext( )
            {
                CheckForConcurrentModification( );

                if( visited >= theDictionary.Count )
                {
                    current = null;
                    return false;
                }

                if( visited == 0 )
                {
                    AANode p = null;
                    for( p = root; p.left != nullNode; p = p.left )
                        path.Push( p );

                    current = p;
                    visited++;
                    return true;
                }

                if( current.right != nullNode )
                {
                    path.Push( current );
                    current = current.right;
                    while( current.left != nullNode )
                    {
                        path.Push( current );
                        current = current.left;
                    }
                }
                else
                {
                    AANode parent;

                    for( ; path.Count > 0; current = parent )
                    {
                        parent = path.Pop( );

                        if( parent.left == current )
                        {
                            current = parent;
                            break;
                        }
                    }
                }

                visited++;
                return true;
            }

            public KeyValuePair<KeyType, ValueType> Current
            {
                get
                {
                    CheckForConcurrentModification( );
                    return current.element;
                }
            }

            private void CheckForConcurrentModification( )
            {
                if( expectedModCount != theDictionary.modCount )
                    throw new InvalidOperationException( );
            }
        }

        private class AANode
        {
            // Constructor
            public AANode( KeyType key, ValueType val, AANode lt, AANode rt )
            {
                element = new KeyValuePair<KeyType, ValueType>( key, val );
                left = lt;
                right = rt;
                level = 1;
            }

            public KeyValuePair<KeyType, ValueType> element;   // The data in the node
            public AANode left;                              // Left child
            public AANode right;                             // Right child
            public int level;                             // Level
        }

        public override IEnumerator<KeyValuePair<KeyType, ValueType>> GetEnumerator( )
        {
            return new Enumerator( this );
        }

        public override void Add( KeyType key, ValueType val )
        {
            Insert( key, val, ref root );
        }

        // Internal method to insert key and value into a subtree.
        // t is the node that roots the tree.
        private void Insert( KeyType key, ValueType val, ref AANode t )
        {
            if( t == nullNode )
            {
                t = new AANode( key, val, nullNode, nullNode );
                modCount++;
                IncrementCount( );
            }
            else
            {
                int result = Compare( key, t.element.Key );

                if( result < 0 )
                    Insert( key, val, ref t.left );
                else if( result > 0 )
                    Insert( key, val, ref t.right );
                else
                    return;
            }
            Skew( ref t );
            Split( ref t );
        }

        public override void Clear( )
        {
            ZeroCount( );
            modCount++;
            root = nullNode;
        }


        public override bool ContainsKey( KeyType key )
        {
            return Find( key ) != null;
        }
        public override bool Remove( KeyType key )
        {
            deletedNode = nullNode;
            return Remove( key, ref root );
        }


        // Internal method to remove from a subtree.
        // key is the key to remove.
        // t is the node that roots the tree.
        // Returns true if the remove succeeds.
        private bool Remove( KeyType key, ref AANode t )
        {
            bool result = false;

            if( t != nullNode )
            {
                // Step 1: Search down the tree and set lastNode and deletedNode
                lastNode = t;
                if( Compare( key, t.element.Key ) < 0 )
                    result = Remove( key, ref t.left );
                else
                {
                    deletedNode = t;
                    result = Remove( key, ref t.right );
                }

                // Step 2: If at the bottom of the tree and
                //         x is present, we remove it
                if( t == lastNode )
                {
                    if( deletedNode == nullNode || Compare( key, deletedNode.element.Key ) != 0 )
                        return false;   // Item not found; do nothing
                    deletedNode.element = t.element;
                    t = t.right;
                    DecrementCount( );
                    modCount++;
                    return true;
                }

                // Step 3: Otherwise, we are not at the bottom; rebalance
                else
                    if( t.left.level < t.level - 1 || t.right.level < t.level - 1 )
                {
                    if( t.right.level > --t.level )
                        t.right.level = t.level;
                    Skew( ref t );
                    Skew( ref t.right );
                    Skew( ref t.right.right );
                    Split( ref t );
                    Split( ref t.right );
                }
            }
            return result;
        }


        public override ValueType this[ KeyType key ]
        {
            get
            {
                AANode p = Find( key );
                if( p != null )
                    return p.element.Value;
                else
                    throw new ArgumentException( "Key not present in dictionary" );
            }

            set
            {
                AANode p = Find( key );
                if( p != null )
                    p.element.Value = value;
                else
                    Add( key, value );
            }
        }

        public SortedDictionary( )
        {
            nullNode = new AANode( KeyType.default, ValueType.default, null, null );
            nullNode.left = nullNode.right = nullNode;
            nullNode.level = 0;
            root = nullNode;
            cmp = null;
        }

        public SortedDictionary( IComparer<KeyType> c ) : this( )
        {
            cmp = c;
        }

        public SortedDictionary( SortedDictionary<KeyType, ValueType> other ) : this( other.cmp )
        {
            CopyFrom( other );
        }

        private void CopyFrom( IDictionary<KeyType, ValueType> other )
        {
            Clear( );
            foreach( KeyValuePair<KeyType, ValueType> x in other )
                Add( x.Key, x.Value );
        }

        private int Compare( KeyType lhs, KeyType rhs )
        {
            if( cmp == null )
                return ( (System.IComparable) lhs ).CompareTo( rhs );
            else
                return cmp.Compare( lhs, rhs );
        }

        // Find an item in the tree.
        // x is the item to search for.
        // Returns the matching item or null if not found.
        private AANode Find( KeyType key )
        {
            AANode current = root;
            nullNode.element = new KeyValuePair<KeyType, ValueType>( key, ValueType.default );

            for( ; ; )
            {
                int result = Compare( key, current.element.Key );

                if( result < 0 )
                    current = current.left;
                else if( result > 0 )
                    current = current.right;
                else if( current != nullNode )
                    return current;
                else
                    return null;
            }
        }


        // Skew primitive for AA-trees.
        // t is the node that roots the tree.
        // Returns the new root after the rotation.
        private void Skew( ref AANode t )
        {
            if( t.left.level == t.level )
                RotateWithLeftChild( ref t );
        }

        // Split primitive for AA-trees.
        // t is the node that roots the tree.
        // Returns the new root after the rotation.
        private void Split( ref AANode t )
        {
            if( t.right.right.level == t.level )
            {
                RotateWithRightChild( ref t );
                t.level++;
            }
        }

        // Rotate binary tree node with left child.
        private static void RotateWithLeftChild( ref AANode k2 )
        {
            AANode k1 = k2.left;
            k2.left = k1.right;
            k1.right = k2;
            k2 = k1;
        }

        // Rotate binary tree node with right child.
        private static void RotateWithRightChild( ref AANode k1 )
        {
            AANode k2 = k1.right;
            k1.right = k2.left;
            k2.left = k1;
            k1 = k2;
        }

        private AANode deletedNode;
        private AANode lastNode;
        private AANode nullNode;

        private int modCount = 0;
        private AANode root = null;
        private IComparer<KeyType> cmp;

    }
}
