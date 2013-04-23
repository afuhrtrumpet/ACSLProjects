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

        static char[] Sort(char[] arg, int start, int length)
        {
            // Sorting: Bubble Sort
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

        static char[,] Divide(char[] arg)
        {
            char[,] result = new char[2,8];
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
            for (int i = 0; i < 4; i++)
            {
                result[0, i] = firstArray[i];
                result[0, i + 4] = firstArray[i];
                result[1, i] = secondArray[i];
                result[1, i + 4] = secondArray[i];
            }
            return result;
        }

        static string ExtractString(char[,] arg, int number)
        {
            char[] result = new char[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = arg[number, i];
            }
            return new string(result);
        }

        static char[] Add(char[] arg, int n)
        {
            char[] result = new char[8];
            for (int i = 0; i < n; i++)
            {
                result[i] = arg[i];
                result[i + n] = arg[i];
            }
            for (int i = 2 * n; i < 8; i++)
            {
                result[i] = arg[i - n];
            }
            result = Sort(result, n, n);
            return result;
        }

        static char[] Subtract(char[] arg, int n)
        {
            char[] result = new char[8];
            for (int i = 0; i < 8 - 2 * n; i++)
            {
                result[i] = arg[i + n];
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[8 - n * 2 + i * n + j] = arg[8 - n + j];
                }
            }
            result = Sort(result, 8 - n, n);
            return result;
        }

        static char[] Union(char[] arg1, char[] arg2)
        {
            char[] result = new char[8];
            for (int i = 0; i < 4; i++)
            {
                result[i] = arg1[i + 4];
            }
            for (int i = 0; i < 4; i++)
            {
                result[i + 4] = arg2[i];
            }
            result = Sort(result, 0, 4);
            result = Sort(result, 4, 4);
            return result;
        }

        static char[] Intersect(char[] arg1, char[] arg2)
        {
            char[] result = new char[8];
            for (int i = 0; i < 2; i++)
            {
                result[i] = arg1[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[i + 2] = arg1[6 + i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[i + 4] = arg2[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[i + 6] = arg2[i + 6];
            }
            result = Sort(result, 0, 4);
            result = Sort(result, 4, 4);
            return result;
        }
    }
}
