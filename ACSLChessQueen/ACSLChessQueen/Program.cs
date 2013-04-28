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
                //Get all initial potential locations for the queen.
                List<string> result = getQueenLocations(int.Parse(tokens[0].Trim(' ')), int.Parse(tokens[1].Trim(' ')));
                for (int i = 1; i < (tokens.Length - 2) / 2; i++)
                {
                    //Get locations based on successive points and intersect them with current list of locations.
                    List<string> intermediary = getQueenLocations(int.Parse(tokens[2 * i]), int.Parse(tokens[2 * i + 1]));
                    result = intersect(result, intermediary);
                }
                Console.WriteLine(findLowestRow(result));
            }
        }

        /// <summary>
        /// Based on a given point, gives all locations that the queen could be to attack that point.
        /// </summary>
        /// <param name="r">The point's row.</param>
        /// <param name="c">The point's column.</param>
        /// <returns>A list of potential locations for the queen.</returns>
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

        /// <summary>
        /// Based on a given point, adds locations that the queen could be to attack from above to a list.
        /// </summary>
        /// <param name="r">The point's row.</param>
        /// <param name="c">The point's column.</param>
        /// <param name="result">The current list of queen's locations to add to.</param>
        /// <returns>The list with new locations added.</returns>
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

        /// <summary>
        /// Based on a given point, adds locations that the queen could be to attack from below to a list.
        /// </summary>
        /// <param name="r">The point's row.</param>
        /// <param name="c">The point's column.</param>
        /// <param name="result">The current list of queen's locations to add to.</param>
        /// <returns>The list with new locations added.</returns>
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

        /// <summary>
        /// Based on a given point, adds locations that the queen could be to attack from the left to a list.
        /// </summary>
        /// <param name="r">The point's row.</param>
        /// <param name="c">The point's column.</param>
        /// <param name="result">The current list of queen's locations to add to.</param>
        /// <returns>The list with new locations added.</returns>
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

        /// <summary>
        /// Based on a given point, adds locations that the queen could be to attack from the right to a list.
        /// </summary>
        /// <param name="r">The point's row.</param>
        /// <param name="c">The point's column.</param>
        /// <param name="result">The current list of queen's locations to add to.</param>
        /// <returns>The list with new locations added.</returns>
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

        /// <summary>
        /// Based on a given point, adds locations that the queen could be to attack from the upper right to a list.
        /// </summary>
        /// <param name="r">The point's row.</param>
        /// <param name="c">The point's column.</param>
        /// <param name="result">The current list of queen's locations to add to.</param>
        /// <returns>The list with new locations added.</returns>
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

        /// <summary>
        /// Based on a given point, adds locations that the queen could be to attack from the upper left to a list.
        /// </summary>
        /// <param name="r">The point's row.</param>
        /// <param name="c">The point's column.</param>
        /// <param name="result">The current list of queen's locations to add to.</param>
        /// <returns>The list with new locations added.</returns>
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

        /// <summary>
        /// Based on a given point, adds locations that the queen could be to attack from the lower right to a list.
        /// </summary>
        /// <param name="r">The point's row.</param>
        /// <param name="c">The point's column.</param>
        /// <param name="result">The current list of queen's locations to add to.</param>
        /// <returns>The list with new locations added.</returns>
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

        /// <summary>
        /// Based on a given point, adds locations that the queen could be to attack from the lower left to a list.
        /// </summary>
        /// <param name="r">The point's row.</param>
        /// <param name="c">The point's column.</param>
        /// <param name="result">The current list of queen's locations to add to.</param>
        /// <returns>The list with new locations added.</returns>
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

        /// <summary>
        /// Given two lists, generates a new list with the common values between the two.
        /// </summary>
        /// <param name="r1">The first list.</param>
        /// <param name="r2">The second list.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Finds the point with the lowest row number in the list and returns it.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        static string findLowestRow(List<string> result)
        {
            int lowRow = 9;
            string winner = "NONE";
            for (int i = 0; i < result.Count; i++)
            {
                //First character of each string gives the row.
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
