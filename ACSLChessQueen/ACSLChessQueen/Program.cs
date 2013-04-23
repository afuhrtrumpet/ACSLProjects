using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACSLChessQueen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a file path:  ");
            StreamReader reader = new StreamReader(Console.ReadLine());
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] tokens = line.Split(',');
                List<string> result = getQueenLocations(int.Parse(tokens[0].Trim(' ')), int.Parse(tokens[1].Trim(' ')));
                for (int i = 1; i < (tokens.Length - 2) / 2; i++)
                {
                    List<string> intermediary = getQueenLocations(int.Parse(tokens[2 * i]), int.Parse(tokens[2 * i + 1]));
                    result = intersect(result, intermediary);
                }
                Console.WriteLine(findLowestRow(result));
            }
        }

        static List<string> getQueenLocations(int r, int c)
        {
            List<string> result = new List<string>();
            result = getAbove(r, c, result);
            result = getBelow(r, c, result);
            result = getLeft(r, c, result);
            result = getRight(r, c, result);
            result = getUpperRight(r, c, result);
            result = getUpperLeft(r, c, result);
            result = getLowerRight(r, c, result);
            result = getLowerLeft(r, c, result);
            return result;
        }

        static List<string> getAbove(int r, int c, List<string> result)
        {
            int currentRow = r + 1;
            while (currentRow <= 8)
            {
                result.Add(currentRow + ", " + c);
                currentRow++;
            }
            return result;
        }

        static List<string> getBelow(int r, int c, List<string> result)
        {
            int currentRow = r - 1;
            while (currentRow >= 1)
            {
                result.Add(currentRow + ", " + c);
                currentRow--;
            }
            return result;
        }

        static List<string> getLeft(int r, int c, List<string> result)
        {
            int currentCol = c - 1;
            while (currentCol >= 1)
            {
                result.Add(r + ", " + currentCol);
                currentCol--;
            }
            return result;
        }

        static List<string> getRight(int r, int c, List<string> result)
        {
            int currentCol = c + 1;
            while (currentCol <= 8)
            {
                result.Add(r + ", " + currentCol);
                currentCol++;
            }
            return result;
        }

        static List<string> getUpperRight(int r, int c, List<string> result)
        {
            int currentCol = c + 1;
            int currentRow = r + 1;
            while (currentCol <= 8 && currentRow <= 8)
            {
                result.Add(currentRow + ", " + currentCol);
                currentRow++;
                currentCol++;
            }
            return result;
        }

        static List<string> getUpperLeft(int r, int c, List<string> result)
        {
            int currentCol = c - 1;
            int currentRow = r + 1;
            while (currentCol >= 1 && currentRow <= 8)
            {
                result.Add(currentRow + ", " + currentCol);
                currentRow++;
                currentCol--;
            }
            return result;
        }

        static List<string> getLowerRight(int r, int c, List<string> result)
        {
            int currentCol = c + 1;
            int currentRow = r - 1;
            while (currentCol <= 8 && currentRow >= 1)
            {
                result.Add(currentRow + ", " + currentCol);
                currentRow--;
                currentCol++;
            }
            return result;
        }

        static List<string> getLowerLeft(int r, int c, List<string> result)
        {
            int currentCol = c - 1;
            int currentRow = r - 1;
            while (currentCol >= 1 && currentRow >= 1)
            {
                result.Add(currentRow + ", " + currentCol);
                currentRow--;
                currentCol--;
            }
            return result;
        }

        static List<string> intersect(List<string> r1, List<string> r2)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < r1.Count; i++)
            {
                for (int j = 0; j < r2.Count; j++)
                {
                    if (r1[i].Equals(r2[j]))
                        result.Add(r1[i]);
                }
            }
            return result;
        }

        static string findLowestRow(List<string> result)
        {
            int lowRow = 9;
            string winner = "NONE";
            for (int i = 0; i < result.Count; i++)
            {
                int r = int.Parse(result[i].Substring(0, 1));
                if (r < lowRow)
                {
                    lowRow = r;
                    winner = result[i];
                }
            }
            return winner;
        }
    }
}
