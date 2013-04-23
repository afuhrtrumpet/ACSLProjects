using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACSLGridFit
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader input;
            bool finished = false;
            Console.Write("Enter a file:  ");
            string fileName = Console.ReadLine();
            do
            {
                Grid grid;
                try
                {
                    input = new StreamReader(fileName);
                    string inLine = input.ReadLine();
                    string[] data = inLine.Split(',');
                    int[] filledSquares = new int[int.Parse(data[0].Trim())];
                    for (int i = 1; i < data.Length; i++)
                    {
                        filledSquares[i - 1] = int.Parse(data[i].Trim());
                    }
                    grid = new Grid(filledSquares);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("\n\nFile not found!\n\nTry again.");
                    Console.Write("Enter the complete data file name:  ");
                    fileName = Console.ReadLine();
                    continue;
                }

                while (!input.EndOfStream)
                {
                    string line = input.ReadLine();
                    int result = 36;
                    switch (int.Parse(line))
                    {
                        case 1: result = grid.FitOneByOne(); break;
                        case 2: result = grid.FitOneByTwo(); break;
                        case 3: result = grid.FitTwoByOne(); break;
                        case 4: result = grid.FitTopCornerPiece(); break;
                        case 5: result = grid.FitBottomCornerPiece(); break;
                        default: throw new FormatException();
                    }
                    if (result == 36) Console.WriteLine("NONE");
                    else Console.WriteLine(result);
                }
                finished = true;
            } while (!finished);
        }
    }
}
