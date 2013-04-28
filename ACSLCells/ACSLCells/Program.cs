using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACSLCells
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a file name.  ");
            string file = Console.ReadLine();
            StreamReader reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] tokens = line.Split(',');
                switch (tokens[0].Substring(0, 1))
                {
                        //Only looks at the first letter of the command for simplicity.
                    case "D":
                        char[] arg = tokens[1].Trim().ToCharArray();
                        char[,] result = Divide(arg);
                        Console.WriteLine(ExtractString(result, 0) + " and " + ExtractString(result, 1));
                        break;
                    case "A":
                        int n = int.Parse(tokens[0].Substring(3, 1));
                        char[] arg1 = tokens[1].Trim().ToCharArray();
                        char[] result4 = Add(arg1, n);
                        Console.WriteLine(new string(result4));
                        break;
                    case "S":
                        char[] arg2 = tokens[1].Trim().ToCharArray();
                        int n2 = int.Parse(tokens[0].Substring(8, 1));
                        char[] result1 = Subtract(arg2, n2);
                        Console.WriteLine(new string(result1));
                        break;
                    case "U":
                        char[] arg3 = tokens[1].Trim().ToCharArray();
                        char[] arg4 = tokens[2].Trim().ToCharArray();
                        char[] result2 = Union(arg3, arg4);
                        Console.WriteLine(new string(result2));
                        break;
                    case "I":
                        char[] arg5 = tokens[1].Trim().ToCharArray();
                        char[] arg6 = tokens[2].Trim().ToCharArray();
                        char[] result3 = Intersect(arg5, arg6);
                        Console.WriteLine(new string(result3));
                        break;
                }
            }
            Console.Read();
        }

        static char[] Swap(char[] arg, int firstPosition, int secondPosition)
        {
            char temp = arg[firstPosition];
            arg[firstPosition] = arg[secondPosition];
            arg[secondPosition] = temp;
            return arg;
        }

        /// <summary>
        /// Performs a bubble sort on the specified array.
        /// </summary>
        /// <param name="arg">Array to be sorted.</param>
        /// <param name="start">Index to start at.</param>
        /// <param name="length">Length of the sorted region.</param>
        /// <returns>The sorted array.</returns>
        static char[] Sort(char[] arg, int start, int length)
        {
            for (int i = start; i < start + length; i++)
            {
                for (int j = i + 1; j < start + length; j++)
                {
                    if (arg[i].CompareTo(arg[j]) > 0)
                    {
                        arg = Swap(arg, i, j);
                    }
                }
            }
            return arg;
        }

        /// <summary>
        /// Performs the DIVIDE operation on the specified array.
        /// </summary>
        /// <param name="arg">The array to be divided.</param>
        /// <returns>The two resulting arrays.</returns>
        static char[,] Divide(char[] arg)
        {
            char[,] result = new char[2,8];
            //Define half-arrays to be replicated into result later.
            char[] firstArray = new char[4], secondArray = new char[4];
            for (int i = 0; i < 4; i++)
            {
                firstArray[i] = arg[i];
            }
            for (int i = 0; i < 4; i++)
            {
                secondArray[i] = arg[i + 4];
            }
            Sort(firstArray, 0, 4);
            Sort(secondArray, 0, 4);
            //Replicate each array twice into result.
            for (int i = 0; i < 4; i++)
            {
                result[0, i] = firstArray[i];
                result[0, i + 4] = firstArray[i];
                result[1, i] = secondArray[i];
                result[1, i + 4] = secondArray[i];
            }
            return result;
        }

        /// <summary>
        /// Extracts a string from a multidimensional char array at the specified index.
        /// </summary>
        /// <param name="arg">The multidimensional char array to be extracted from.</param>
        /// <param name="number">The index where extraction is to happen.</param>
        /// <returns>The resulting string.</returns>
        static string ExtractString(char[,] arg, int number)
        {
            char[] result = new char[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = arg[number, i];
            }
            return new string(result);
        }

        /// <summary>
        /// Performs the ADD operation on the specified array.
        /// </summary>
        /// <param name="arg">The array to be added.</param>
        /// <param name="n">Number of bits to replicate.</param>
        /// <returns></returns>
        static char[] Add(char[] arg, int n)
        {
            char[] result = new char[8];
            //Copy the first n bits twice into result.
            for (int i = 0; i < n; i++)
            {
                result[i] = arg[i];
                result[i + n] = arg[i];
            }
            //Concatenate remaining bits to the end.
            for (int i = 2 * n; i < 8; i++)
            {
                result[i] = arg[i - n];
            }
            //Sort only second set of n bits.
            result = Sort(result, n, n);
            return result;
        }

        /// <summary>
        /// Performs the SUBTRACT operation on the specified array.
        /// </summary>
        /// <param name="arg">The array to be subtracted.</param>
        /// <param name="n">Number of bits to replicate.</param>
        /// <returns></returns>
        static char[] Subtract(char[] arg, int n)
        {
            char[] result = new char[8];
            //Copy remaining bits into result.
            for (int i = 0; i < 8 - 2 * n; i++)
            {
                result[i] = arg[i + n];
            }
            //Concatenate last n bits twice.
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[8 - n * 2 + i * n + j] = arg[8 - n + j];
                }
            }
            //Sort only last n bits.
            result = Sort(result, 8 - n, n);
            return result;
        }

        /// <summary>
        /// Performs the UNION operation on the specified array.
        /// </summary>
        /// <param name="arg1">First array to be united.</param>
        /// <param name="arg2">Second array to be united.</param>
        /// <returns>The united array.</returns>
        static char[] Union(char[] arg1, char[] arg2)
        {
            char[] result = new char[8];
            //Place last four bits of first array into result.
            for (int i = 0; i < 4; i++)
            {
                result[i] = arg1[i + 4];
            }
            //Place first four bits of second array into result.
            for (int i = 0; i < 4; i++)
            {
                result[i + 4] = arg2[i];
            }
            //Sort both halves.
            result = Sort(result, 0, 4);
            result = Sort(result, 4, 4);
            return result;
        }

        /// <summary>
        /// Performs the INTERSECT operation on the specified array.
        /// </summary>
        /// <param name="arg1">First array to be intersected.</param>
        /// <param name="arg2">Second array to be intersected.</param>
        /// <returns>The intersected array.</returns>
        static char[] Intersect(char[] arg1, char[] arg2)
        {
            char[] result = new char[8];
            //Place first two bits of first array into result.
            for (int i = 0; i < 2; i++)
            {
                result[i] = arg1[i];
            }
            //Place last two bits of first array into result.
            for (int i = 0; i < 2; i++)
            {
                result[i + 2] = arg1[6 + i];
            }
            //Place first two bits of second array into result.
            for (int i = 0; i < 2; i++)
            {
                result[i + 4] = arg2[i];
            }
            //Place last two bits of second array into result.
            for (int i = 0; i < 2; i++)
            {
                result[i + 6] = arg2[i + 6];
            }
            //Sort both halves.
            result = Sort(result, 0, 4);
            result = Sort(result, 4, 4);
            return result;
        }
    }
}
