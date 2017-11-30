using System;
using System.Collections.Generic;
using System.IO;

namespace GroupAlgorithm
{

    // Xref class interface: generate cross-reference
    //
    // CONSTRUCTION: with a Reader object
    // ******************PUBLIC OPERATIONS***********************
    // void GenerateCrossReference( ) --> Name says it all ...
    // ******************ERRORS**********************************
    // Error checking on comments and quotes is performed
    // main generates cross-reference.


    // Class to perform cross reference
    // generation for C# programs.
    public class Xref
    {
        // Constructor.
        // inStream is the stream containing a program.
        public Xref(TextReader inStream)
        {
            tok = new Tokenizer(inStream);
        }

        private Tokenizer tok; // tokenizer object


        // Output the cross reference.
        public void GenerateCrossReference()
        {
            SortedDictionary<string, List<int>> theIdentifiers = new SortedDictionary<string, List<int>>();
            string current;

            // Insert identifiers into the search tree
            while ((current = tok.GetNextID()) != "")
            {
                bool hasKey = theIdentifiers.ContainsKey(current);
                if (!hasKey)
                    theIdentifiers.Add(current, new List<int>());
                theIdentifiers[current].Add(tok.GetLineNumber());
            }

            // Iterate through search tree and output
            // identifiers and their line number
            IEnumerator<KeyValuePair<string, List<int>>> itr = theIdentifiers.GetEnumerator();

            while (itr.MoveNext())
            {
                KeyValuePair<string, List<int>> thisNode = itr.Current;
                IEnumerator<int> lineItr = thisNode.Value.GetEnumerator();

                // Print identifier and first line where it occurs
                Console.Write(thisNode.Key + ": ");
                lineItr.MoveNext();
                Console.Write(lineItr.Current);

                // Print all other lines on which it occurs
                while (lineItr.MoveNext())
                    Console.Write(", " + lineItr.Current);
                Console.WriteLine();
            }
        }

        // main routine to generate cross-reference.
        // If no command line parameters, standard input is used.
        // Otherwise, files in command line are used.
        public static void Main(string[] args)
        {

            Xref p;

            if (args.Length == 0)
            {
                p = new Xref(Console.In);
                p.GenerateCrossReference();

                return;
            }

            for (int i = 0; i < args.Length; i++)
            {
                TextReader f = null;
                try
                {
                    f = new StreamReader(args[i]);

                    Console.WriteLine(args[i] + ": ");
                    p = new Xref(f);
                    p.GenerateCrossReference();
                }
                catch (IOException e)
                {
                    Console.Error.WriteLine(e + args[i]);
                }
                finally
                {
                    if (f != null)
                        f.Close();

                }
            }
        }
    }
}