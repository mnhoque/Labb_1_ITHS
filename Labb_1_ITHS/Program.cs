
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
/*

*/

namespace Labb_1_ITHS
{
    class Program
    {
        static void Main(string[] args)
        {
            String word = "29535123t48723487597645723645";
            //"237539032434567t565";

            //detect the location of the character...
            int charLocation = -1;
            char? dilimiiter = null;

            for (int i = 0; i < word.Length; i++)
            {
                if (char.IsDigit(word[i]) == false)
                {
                    charLocation = i;
                    dilimiiter = word[i];
                    break;
                }
            }

            if (charLocation == -1)
            {
                Console.WriteLine("No char found! Program will exit.");
                Console.ReadKey();
                return;
            }

            //now that the charLocation is found, let's proceed...

            string leftSegment = word.Substring(0, charLocation); //segment before the character
            string rightSegment = word.Substring(charLocation + 1); //segment after the character

            List<List<Character>> leftPatternSet = new List<List<Character>>();

            bool cont = false;
            List<Character> outputStr = null;
            for (int i = 0; i < leftSegment.Length; i++)
            {


                string strLeftOfBold = "";
                if (cont == false)
                {
                    outputStr = new List<Character>();

                    strLeftOfBold = leftSegment.Substring(0, i);//print all the characters before i

                    //dump these characters to outputStr
                    DumpCharacters(strLeftOfBold, outputStr, false);

                }

                int matchingIndex = FindNextMatchingIndex(leftSegment, i);
                string strBold = "";
                if (matchingIndex != -1)
                {
                    strBold = GetBoldStringBetweenIndex(leftSegment, i, matchingIndex);

                    DumpCharacters(strBold, outputStr, true);

                    string strRightOfBold = "";
                    if (matchingIndex + 1 <= leftSegment.Length - 1)
                    {

                        strRightOfBold = leftSegment.Substring(matchingIndex + 1);

                        DumpCharacters(strRightOfBold, outputStr, false);

                    }
                    leftPatternSet.Add(outputStr);
                    cont = false;

                }
                else
                {

                    outputStr.Add(new Character() { ch = leftSegment[i], IsBold = false });
                    cont = true;
                    continue;
                }
            }



            foreach (var pattern in leftPatternSet)
            {
                foreach (Character character in pattern)
                {
                    if (character.IsBold)
                        WriteBold(character.ch.ToString());
                    else
                        Console.Write(character.ch.ToString());
                }
                Console.WriteLine(dilimiiter + rightSegment);
            }

            ////////////////////////////////////
            ///now repeat the same process for right segment....

            List<List<Character>> rightPatternSet = new List<List<Character>>();

            cont = false;
            outputStr = null;
            for (int i = 0; i < rightSegment.Length; i++)
            {


                string strLeftOfBold = "";
                if (cont == false)
                {
                    outputStr = new List<Character>();

                    strLeftOfBold = rightSegment.Substring(0, i);//print all the characters before i

                    //dump these characters to outputStr
                    DumpCharacters(strLeftOfBold, outputStr, false);

                }

                int matchingIndex = FindNextMatchingIndex(rightSegment, i);
                string strBold = "";
                if (matchingIndex != -1)
                {
                    strBold = GetBoldStringBetweenIndex(rightSegment, i, matchingIndex);

                    DumpCharacters(strBold, outputStr, true);

                    string strRightOfBold = "";
                    if (matchingIndex + 1 <= rightSegment.Length - 1)
                    {

                        strRightOfBold = rightSegment.Substring(matchingIndex + 1);

                        DumpCharacters(strRightOfBold, outputStr, false);

                    }
                    rightPatternSet.Add(outputStr);
                    cont = false;

                }
                else
                {
                    outputStr.Add(new Character() { ch = rightSegment[i], IsBold = false });
                    cont = true;
                    continue;
                }
            }



            foreach (var pattern in rightPatternSet)
            {
                Console.Write(leftSegment + dilimiiter);

                foreach (Character character in pattern)
                {
                    if (character.IsBold)
                        WriteBold(character.ch.ToString());
                    else
                        Console.Write(character.ch.ToString());
                }
                Console.WriteLine();
            }



            Console.ReadKey();
        }

        public static void WriteBold(string str)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(str);
            Console.ResetColor();
        }

        public static int FindNextMatchingIndex(string strs, int startLocation)
        {
            int matchingIndex = -1;
            for (int i = startLocation + 1; i < strs.Length; i++)
            {
                if (strs[i] == strs[startLocation])
                {
                    matchingIndex = i;
                    break;
                }
            }

            return matchingIndex;
        }

        public static string GetBoldStringBetweenIndex(string str, int startIndex, int endIndex)
        {
            string s = str.Substring(startIndex, (endIndex - startIndex) + 1);
            return s;
            //WriteBold(s);
        }

        static void DumpCharacters(string str, List<Character> list, bool isBold)
        {
            foreach (var ch in str)
            {
                list.Add(new Character() { ch = ch, IsBold = isBold });
            }
        }
    }

    class Character
    {
        public char ch { get; set; }
        public bool IsBold { get; set; }
    }
}
