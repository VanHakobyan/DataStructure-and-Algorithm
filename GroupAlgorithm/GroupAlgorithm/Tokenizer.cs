using System;
using System.IO;

namespace GroupAlgorithm
{

// Tokenizer class.
//
// CONSTRUCTION: with a TextReader object
// ******************PUBLIC OPERATIONS***********************
// char GetNextOpenClose( )   --> Get next opening/closing sym
// String GetNextID( )        --> Get next C# identifier
// int GetLineNumber( )       --> Return line number
// int GetErrorCount( )       --> Return error count
// ******************ERRORS**********************************
// Error checking on comments and quotes is performed
// main checks for balanced symbols.

    public class Tokenizer
    {
        // Constructor.
        // inStream is the stream containing a program.
        public Tokenizer(TextReader inStream)
        {
            errors = 0;
            ch = '\0';
            currentLine = 1;
            fullBuffer = false;
            input = inStream;
        }

        // Returns the current line number.
        public int GetLineNumber()
        {
            return currentLine;
        }

        // Returns the error count.
        public int GetErrorCount()
        {
            return errors;
        }

        // Gets the next opening or closing symbol.
        // Returns false if end of file.
        // Skips past comments and character and string constants
        public char GetNextOpenClose()
        {
            while (NextChar())
            {
                if (ch == '/')
                    ProcessSlash();
                else if (ch == '\'' || ch == '"')
                    SkipQuote(ch);
                else if (ch == '\\') // Extra case, not in text
                    NextChar();
                else if (ch == '(' || ch == '[' || ch == '{' ||
                         ch == ')' || ch == ']' || ch == '}')
                    return ch;
            }
            return '\0'; // End of file
        }

        // Returns true if ch can be part of a C# identifier
        private static bool IsIdChar(char ch)
        {
            return Char.IsLetterOrDigit(ch) || ch == '_';
        }

        // Return an identifier read from input stream
        // First character is already read into ch
        private string GetRemainingString()
        {
            string result = "" + ch;

            for (; NextChar(); result += ch)
                if (!IsIdChar(ch))
                {
                    PutBackChar();
                    break;
                }

            return result;
        }

        // Return next identifier, skipping comments
        // string constants, and character constants.
        // Place identifier in currentIdNode.word and return false
        // only if end of stream is reached.
        public string GetNextID()
        {
            while (NextChar())
            {
                if (ch == '/')
                    ProcessSlash();
                else if (ch == '\\')
                    NextChar();
                else if (ch == '\'' || ch == '"')
                    SkipQuote(ch);
                else if (!Char.IsDigit(ch) && IsIdChar(ch))
                    return GetRemainingString();
            }
            return ""; // End of file
        }

        // NextChar sets ch based on the next character in the input stream.
        // PutBackChar puts the character back onto the stream.
        // It should only be used once after a NextChar.
        // Both routines adjust currentLine if necessary.
        private bool NextChar()
        {
            try
            {
                if (fullBuffer)
                    fullBuffer = false;
                else
                {
                    int readVal = input.Read();
                    if (readVal == -1)
                        return false;
                    ch = (char) readVal;
                }
                if (ch == '\n')
                    currentLine++;
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        private void PutBackChar()
        {
            if (ch == '\n')
                currentLine--;
            fullBuffer = true;
        }

        // Precondition: We are about to process a comment; have already seen
        //                 comment-start token
        // Postcondition: Stream will be set immediately after
        //                 comment-ending token
        private void SkipComment(int start)
        {
            if (start == SLASH_SLASH)
            {
                while (NextChar() && (ch != '\n'))
                    ;
                return;
            }

            // Look for a */ sequence
            bool state = false; // True if we have seen *

            while (NextChar())
            {
                if (state && ch == '/')
                    return;
                state = (ch == '*');
            }
            errors++;
            Console.WriteLine("Unterminated comment!");
        }

        // Precondition: We are about to process a quote; have already seen
        //                   beginning quote.
        // Postcondition: Stream will be set immediately after
        //                   matching quote
        private void SkipQuote(char quoteType)
        {
            while (NextChar())
            {
                if (ch == quoteType)
                    return;
                if (ch == '\n')
                {
                    errors++;
                    Console.WriteLine("Missing closed quote at line " +
                                      currentLine);
                    return;
                }
                else if (ch == '\\')
                    NextChar();
            }
        }

        // After the opening slash is seen deal with next character.
        // If it is a comment starter, process it; otherwise putback
        // the next character if it is not a newline.
        private void ProcessSlash()
        {
            if (NextChar())
            {
                if (ch == '*')
                    SkipComment(SLASH_STAR);
                else if (ch == '/')
                    SkipComment(SLASH_SLASH);
                else if (ch != '\n')
                    PutBackChar();
            }
        }

        public const int SLASH_SLASH = 0;
        public const int SLASH_STAR = 1;

        private TextReader input; // The input stream
        private char ch; // Current character
        private bool fullBuffer; // True if just did an unread
        private int currentLine; // Current line
        private int errors; // Number of errors seen
    }

}