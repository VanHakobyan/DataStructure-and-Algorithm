using System;
using System.IO;
using System.Collections.Generic;

// WordSearch class interface: solve word search puzzle
//
// CONSTRUCTION: with no initializer
// ******************PUBLIC OPERATIONS******************
// int solvePuzzle( )   --> Print all words found in the
//                          puzzle; return number of matches
namespace GroupAlgorithm
{


    public class WordSearch
    {
        // Constructor for WordSearch class.
        // Prompts for and reads puzzle and dictionary files.
        public WordSearch()
        {
            puzzleStream = OpenFile("Enter puzzle file");
            wordStream = OpenFile("Enter dictionary name");
            Console.WriteLine("Reading files...");
            ReadPuzzle();
            ReadWords();
        }

        // Routine to solve the word search puzzle.
        // Performs checks in all eight directions.
        // Returns the number of matches
        public int SolvePuzzle()
        {
            int matches = 0;

            for (int r = 0; r < rows; r++)
            for (int c = 0; c < columns; c++)
            for (int rd = -1; rd <= 1; rd++)
            for (int cd = -1; cd <= 1; cd++)
                if (rd != 0 || cd != 0)
                    matches += SolveDirection(r, c, rd, cd);

            return matches;
        }


        // Search the grid from a starting point and direction.
        // Returns the number of matches
        private int SolveDirection(int baseRow, int baseCol, int rowDelta, int colDelta)
        {
            string charSequence = "";
            int numMatches = 0;
            int searchResult;

            charSequence += theBoard[baseRow][baseCol];

            for (int i = baseRow + rowDelta, j = baseCol + colDelta;
                i >= 0 && j >= 0 && i < rows && j < columns;
                i += rowDelta, j += colDelta)
            {
                charSequence += theBoard[i][j];
                searchResult = PrefixSearch(theWords, charSequence);

                if (searchResult == theWords.Length)
                    break;
                if (!theWords[searchResult].StartsWith(charSequence))
                    break;

                if (theWords[searchResult].Equals(charSequence))
                {
                    if (theWords[searchResult].Length < 5)
                        continue;
                    numMatches++;
                    Console.WriteLine("Found " + charSequence + " at " +
                                      baseRow + " " + baseCol + " to " +
                                      i + " " + j);
                }
            }

            return numMatches;
        }

        // Performs the binary search for word search.
        // Returns the last position examined;
        //     this position either matches x, or x is
        //     a prefix of the mismatch, or there is no
        //     word for which x is a prefix.
        private static int PrefixSearch(string[] a, string x)
        {
            int idx = Array.BinarySearch(a, x);

            if (idx < 0)
                return ~idx;
            else
                return idx;
        }

        // Print a prompt and open a file.
        // Retry until open is successful.
        // Program exits if end of file is hit.
        private TextReader OpenFile(string message)
        {
            String fileName = "";
            TextReader theFile = null;

            do
            {
                Console.WriteLine(message + ": ");

                try
                {
                    fileName = Console.ReadLine();
                    if (fileName == null)
                        Environment.Exit(0);
                    theFile = new StreamReader(fileName);
                }
                catch (IOException)
                {
                    Console.Error.WriteLine("Cannot open " + fileName);
                }
            } while (theFile == null);

            Console.WriteLine("Opened " + fileName);
            return theFile;
        }

        // Routine to read the grid.
        // Checks to ensure that the grid is rectangular.
        // Checks to make sure that capacity is not exceeded is omitted.
        private void ReadPuzzle()
        {
            string oneLine;
            List<String> puzzleLines = new List<String>();

            if ((oneLine = puzzleStream.ReadLine()) == null)
                throw new IOException("No lines in puzzle file");

            columns = oneLine.Length;
            puzzleLines.Add(oneLine);

            while ((oneLine = puzzleStream.ReadLine()) != null)
            {
                if (oneLine.Length != columns)
                    Console.Error.WriteLine("Puzzle is not rectangular; skipping row");
                else
                    puzzleLines.Add(oneLine);
            }

            rows = puzzleLines.Count;
            theBoard = new char[rows][];
            for (int i = 0; i < rows; i++)
                theBoard[i] = new char[columns];
            IEnumerator<string> itr = puzzleLines.GetEnumerator();
            for (int r = 0; r < rows; r++)
            {
                itr.MoveNext();
                string theLine = itr.Current;
                theBoard[r] = theLine.ToCharArray();
            }
        }

        // Routine to read the dictionary.
        // Error message is printed if dictionary is not sorted.
        private void ReadWords()
        {
            List<string> words = new List<string>();

            string lastWord = null;
            string thisWord;

            while ((thisWord = wordStream.ReadLine()) != null)
            {
                if (lastWord != null && thisWord.CompareTo(lastWord) < 0)
                {
                    Console.Error.WriteLine("Dictionary is not sorted... skipping");
                    continue;
                }
                words.Add(thisWord);
                lastWord = thisWord;
            }

            theWords = new string[words.Count];
            theWords = words.ToArray();
        }

        // Cheap main
        public static void Main(string[] args)
        {
            WordSearch p = null;

            try
            {
                p = new WordSearch();
            }
            catch (IOException e)
            {
                Console.WriteLine("IO Error: " + e);
                return;
            }

            Console.WriteLine("Solving...");
            p.SolvePuzzle();
        }


        private int rows;
        private int columns;
        private char[][] theBoard;
        private string[] theWords;
        private TextReader puzzleStream;
        private TextReader wordStream;
    }
}