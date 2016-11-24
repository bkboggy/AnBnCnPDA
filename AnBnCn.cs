/* Subject: CSE 500 - Fall 2016 - Extra Credit Exercise
 * Author:  Bogdan Kravtsov
 * Date:    11/22/2016
 * 
 * Details: Reads in a set of strings and checks if they belong in
 *          AnBnCn language using two-stack pushdown automaton concept.
 *          The actual process of a PDA was not replicated beyond using
 *          the stack to match symbols.  In this case, two stacks are 
 *          used.
 */


using System;
using System.Collections.Generic;
using System.IO;

namespace AnBnCn
{
    /// <summary>
    /// Reads in strings from a specified file and checks if each one 
    /// is in AnBnCn.
    /// </summary>
    internal class Program
    {     
        /// <summary>
        /// Program entry.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        public static void Main(string[] args)
        {
            IEnumerable<string> strings = GetStrings("sample.txt");
          
            if (strings != null)
            {
                foreach (var s in strings)
                {
                    Console.WriteLine($"String: {s, -30} Is in AnBnCn? {IsAnBnCn(s)}");
                }
            }
            else
            {
                Console.WriteLine("Could not fetch sample strings.");
            }
        }
    
        /// <summary>
        /// Gets strings from a file, assuming that each string is on a separate 
        /// line.
        /// An empty string is valid.
        /// </summary>
        /// <param name="filePath">File path from where the strings are to be 
        /// read.</param>
        /// <returns>IEnumerable of strings.</returns>
        private  static IEnumerable<string> GetStrings(string filePath)
        {
            try
            {
                return File.ReadLines(filePath);
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Tests whether a string is in AnBnCn by using two stacks to compare 
        /// characters.
        /// </summary>
        /// <param name="s">Target string to be tested.</param>
        /// <returns>True if is in AnBnCn; otherwise, false.</returns>
        private static bool IsAnBnCn(string s)
        {
            // Stacks.
            var stack1 = new Stack<char>();
            var stack2 = new Stack<char>();

            // Iteration counter.
            var i = 0;

            // While there are characters to read and the character is an 'a', 
            // push it to stack1.
            while (i < s.Length && s[i] == 'a')
            {
                stack1.Push('a');
                ++i;
            }

            // While there are characters to read, if the character is a 'b', 
            // push 'b' to stack2 and pop 'a' from stack1.
            while (i < s.Length && s[i] == 'b' && stack1.Count > 0)
            {
                stack1.Pop();
                stack2.Push('b');
                ++i;
            }

            // While there are characters to read and the character is a 'c', pop 'b' 
            // from stack2.
            while (i < s.Length && s[i] == 'c' && stack2.Count > 0)
            {
                stack2.Pop();
                ++i;
            }

            // If string has been read to the end and both stacks are empty, return 
            // true; otherwise, false.
            return i == s.Length && stack1.Count == 0 && stack2.Count == 0;
        }
    }
}
