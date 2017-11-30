using System;
using System.IO;

using weiss.nonstandard;

namespace GroupAlgorithm
{
    // HZIPReader wraps an input stream.
    // ReadByte returns an uncompressed byte from the
    // wrapped input stream.
    public class HZIPReader
    {
        public HZIPReader(Stream bin)
        {
            codeTree = new HuffmanTree();
            codeTree.ReadEncodingTable(bin);

            bitstream = new BitInputStream(bin);
        }

        public int ReadByte()
        {
            string bits = "";
            int bit;
            int decode;

            while (true)
            {
                bit = bitstream.ReadBit();
                if (bit == -1)
                    throw new IOException("Unexpected EOF");

                bits += bit;

                decode = codeTree.GetChar(bits);
                if (decode == HuffmanTree.INCOMPLETE_CODE)
                    continue;
                else if (decode == HuffmanTree.ERROR)
                    throw new IOException("Decoding error");
                else if (decode == HuffmanTree.END)
                    return -1;
                else
                    return decode;
            }
        }

        public void Close()
        {
            bitstream.Close();
        }

        private BitInputStream bitstream;
        private HuffmanTree codeTree;
    }

// Writes to HZIPWriter are compressed and
// sent to the output stream being wrapped.
// No writing is actually done until Close.
    public class HZIPWriter
    {
        public HZIPWriter(BinaryWriter sout)
        {
            dout = sout;
        }

        public void Write(byte ch)
        {
            byteOut.WriteByte(ch);
        }

        public void Close()
        {
            byte[] theInput = byteOut.ToArray();
            Console.WriteLine("Read " + theInput.Length + " bytes");

            MemoryStream byteIn = new MemoryStream(theInput);

            CharCounter countObj = new CharCounter(byteIn);
            byteIn.Close();

            HuffmanTree codeTree = new HuffmanTree(countObj);
            codeTree.WriteEncodingTable(dout);

            BitOutputStream bout = new BitOutputStream(dout);

            for (int i = 0; i < theInput.Length; i++)
                bout.WriteBits(codeTree.GetCode(theInput[i] & (0xff)));
            bout.WriteBits(codeTree.GetCode(BitUtils.EOF));

            bout.Close();
            byteOut.Close();
        }

        private MemoryStream byteOut = new MemoryStream();
        private BinaryWriter dout;
    }



    abstract class BitUtils
    {
        public const int BITS_PER_BYTES = 8;
        public const int DIFF_BYTES = 256;
        public const int EOF = 256;
    }

// BitInputStream class: Bit-input stream wrapper class.
//
// CONSTRUCTION: with an open (input) Stream.
//
// ******************PUBLIC OPERATIONS***********************
// int ReadBit( )              --> Read one bit as a 0 or 1
// void Close( )               --> Close underlying stream

    class BitInputStream
    {
        public BitInputStream(Stream fin)
        {
            stream = fin;
            bufferPos = BitUtils.BITS_PER_BYTES;
        }

        public int ReadBit()
        {
            if (bufferPos == BitUtils.BITS_PER_BYTES)
            {
                int nextByte = stream.ReadByte();
                if (nextByte == -1)
                    return -1;

                buffer = (byte) nextByte;
                bufferPos = 0;
            }

            return GetBit(buffer, bufferPos++);
        }

        public void Close()
        {
            stream.Close();
        }

        private static int GetBit(int pack, int pos)
        {
            return (pack & (1 << pos)) != 0 ? 1 : 0;
        }

        private Stream stream;
        private byte buffer;
        private int bufferPos;
    }

// BitOutputStream class: Bit-output stream wrapper class.
//
// CONSTRUCTION: with an open BinaryWriter stream.
//
// ******************PUBLIC OPERATIONS***********************
// void WriteBit( val )        --> Write one bit (0 or 1)
// void WriteBits( vald )      --> Write array of bits
// void Flush( )               --> Flush buffered bits
// void Close( )               --> Close underlying stream

    class BitOutputStream
    {
        public BitOutputStream(BinaryWriter fout)
        {
            bufferPos = 0;
            buffer = 0;
            stream = fout;
        }

        public void WriteBit(byte val)
        {
            buffer = (byte) SetBit(buffer, bufferPos++, val);
            if (bufferPos == BitUtils.BITS_PER_BYTES)
                Flush();
        }

        public void WriteBits(byte[] val)
        {
            foreach (byte v in val)
                WriteBit(v);
        }

        public void Flush()
        {
            if (bufferPos == 0)
                return;

            stream.Write(buffer);
            bufferPos = 0;
            buffer = 0;
        }

        public void Close()
        {
            Flush();
            stream.Close();
        }

        private int SetBit(int pack, int pos, int val)
        {
            if (val == 1)
                pack |= (val << pos);
            return pack;
        }

        private BinaryWriter stream;
        private byte buffer;
        private int bufferPos;
    }

// CharCounter class: A character counting class.
//
// CONSTRUCTION: with no parameters or an open InputStream.
//
// ******************PUBLIC OPERATIONS***********************
// int GetCount( ch )           --> Return # occurrences of ch
// void SetCount( ch, count )   --> Set # occurrences of ch
// ******************ERRORS**********************************
// No error checks.

    class CharCounter
    {
        public CharCounter()
        {
        }

        public CharCounter(Stream input)
        {
            int ch;
            while ((ch = input.ReadByte()) != -1)
                theCounts[ch]++;
        }

        public int GetCount(int ch)
        {
            return theCounts[ch & 0xff];
        }

        public void SetCount(int ch, int count)
        {
            theCounts[ch & 0xff] = count;
        }

        private int[] theCounts = new int[BitUtils.DIFF_BYTES + 1];
    }


// Huffman tree class interface: manipulate huffman coding tree.
//
// CONSTRUCTION: with no parameters or a CharCounter object.
//
// ******************PUBLIC OPERATIONS***********************
// int [ ] GetCode( ch )        --> Return code given character
// int GetChar( code )          --> Return character given code
// void WriteEncodingTable( out ) --> Write coding table to out
// void ReadEncodingTable( in ) --> Read encoding table from in
// ******************ERRORS**********************************
// Error check for illegal code.

    class HuffmanTree
    {
        // Basic node in a Huffman coding tree.
        private class HuffNode : System.Collections.Generic.IComparable<HuffNode>
        {
            public int value;
            public int weight;

            public int CompareTo(HuffNode rhs)
            {
                return weight - rhs.weight;
            }

            public bool Equals(HuffNode rhs)
            {
                return weight == rhs.weight;
            }

            public HuffNode left;
            public HuffNode right;
            public HuffNode parent;

            public HuffNode(int v, int w, HuffNode lt, HuffNode rt, HuffNode pt)
            {
                value = v;
                weight = w;
                left = lt;
                right = rt;
                parent = pt;
            }
        }

        public HuffmanTree()
        {
            theCounts = new CharCounter();
            root = null;
        }

        public HuffmanTree(CharCounter cc)
        {
            theCounts = cc;
            root = null;
            CreateTree();
        }

        public const int ERROR = -3;
        public const int INCOMPLETE_CODE = -2;
        public const int END = BitUtils.DIFF_BYTES;

        // Return the code corresponding to character ch.
        // (The parameter is an int to accomodate EOF).
        // If code is not found, return an array of length 0.
        public byte[] GetCode(int ch)
        {
            HuffNode current = theNodes[ch];
            if (current == null)
                return null;

            string v = "";
            HuffNode par = current.parent;

            while (par != null)
            {
                if (par.left == current)
                    v = "0" + v;
                else
                    v = "1" + v;
                current = current.parent;
                par = current.parent;
            }

            byte[] result = new byte[v.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = (byte) (v[i] == '0' ? 0 : 1);

            return result;
        }

        // Get the character corresponding to code.
        public int GetChar(string code)
        {
            HuffNode p = root;
            for (int i = 0; p != null && i < code.Length; i++)
                if (code[i] == '0')
                    p = p.left;
                else
                    p = p.right;

            if (p == null)
                return ERROR;

            return p.value;
        }

        // Writes an encoding table to an output stream.
        // Format is character, count (as bytes).
        // A zero count terminates the encoding table.
        public void WriteEncodingTable(BinaryWriter fout)
        {
            for (int i = 0; i < BitUtils.DIFF_BYTES; i++)
            {
                if (theCounts.GetCount(i) > 0)
                {
                    fout.Write((byte) i);
                    fout.Write(theCounts.GetCount(i));
                }
            }
            fout.Write((byte) 0);
            fout.Write((int) 0);
        }

        // Read the encoding table from an input stream in format
        // given above and then construct the Huffman tree.
        // Stream will then be positioned to read compressed data.
        public void ReadEncodingTable(Stream sin)
        {
            BinaryReader bin = new BinaryReader(sin);

            for (int i = 0; i < BitUtils.DIFF_BYTES; i++)
                theCounts.SetCount(i, 0);

            int ch;
            int num;

            for (;;)
            {
                ch = bin.ReadByte();
                num = bin.ReadInt32();
                if (num == 0)
                    break;
                theCounts.SetCount(ch, num);
            }

            CreateTree();
        }

        // Construct the Huffman coding tree.
        private void CreateTree()
        {
            IPriorityQueue<HuffNode> pq = new BinaryHeap<HuffNode>();

            for (int i = 0; i < BitUtils.DIFF_BYTES; i++)
                if (theCounts.GetCount(i) > 0)
                {
                    HuffNode newNode = new HuffNode(i,
                        theCounts.GetCount(i), null, null, null);
                    theNodes[i] = newNode;
                    pq.Insert(newNode);
                }

            theNodes[END] = new HuffNode(END, 1, null, null, null);
            pq.Insert(theNodes[END]);

            while (pq.Size() > 1)
            {
                HuffNode n1 = pq.DeleteMin();
                HuffNode n2 = pq.DeleteMin();
                HuffNode result = new HuffNode(INCOMPLETE_CODE,
                    n1.weight + n2.weight, n1, n2, null);
                n1.parent = n2.parent = result;
                pq.Insert(result);
            }

            root = pq.FindMin();
        }

        private CharCounter theCounts;
        private HuffNode[] theNodes = new HuffNode[BitUtils.DIFF_BYTES + 1];
        private HuffNode root;
    }


    public class Hzip
    {

        public static void Compress(string inFile)
        {
            string compressedFile = inFile + ".huf";
            Stream fin = new FileStream(inFile, FileMode.Open);
            BinaryWriter fout = new BinaryWriter(new FileStream(compressedFile, FileMode.OpenOrCreate));

            HZIPWriter hzout = new HZIPWriter(fout);
            int ch;
            while ((ch = fin.ReadByte()) != -1)
                hzout.Write((byte) ch);
            fin.Close();
            hzout.Close();
        }

        public static void Uncompress(string compressedFile)
        {
            string inFile;
            string extension;

            inFile = compressedFile.Substring(0, compressedFile.Length - 4);
            extension = compressedFile.Substring(compressedFile.Length - 4);

            if (!extension.Equals(".huf"))
            {
                Console.WriteLine("Not a compressed file!");
                return;
            }

            inFile += ".uc"; // for debugging, so as to not clobber original
            HZIPReader hzin = new HZIPReader(new FileStream(compressedFile, FileMode.Open));

            BinaryWriter fout = new BinaryWriter(new FileStream(inFile, FileMode.OpenOrCreate));
            int ch;
            while ((ch = hzin.ReadByte()) != -1)
                fout.Write((byte) ch);

            hzin.Close();
            fout.Close();
        }

        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: java Hzip -[cu] files");
                return;
            }

            string option = args[0];
            for (int i = 1; i < args.Length; i++)
            {
                string nextFile = args[i];
                if (option.Equals("-c"))
                    Compress(nextFile);
                else if (option.Equals("-u"))
                    Uncompress(nextFile);
                else
                {
                    Console.WriteLine("Usage: java Hzip -[cu] files");
                    return;
                }
            }
        }
    }
}