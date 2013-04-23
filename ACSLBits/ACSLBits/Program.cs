using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACSLBits
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
                try
                {
                    input = new StreamReader(fileName);
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
                    try
                    {
                        string inLine = input.ReadLine();
                        string[] data = inLine.Split(',');
                        int numBitStrings = int.Parse(data[0]);
                        char[][] bits = new char[numBitStrings][];
                        for (int i = 1; i < data.Length; i++)
                        {
                            data[i] = data[i].TrimStart();
                            bits[i - 1] = data[i].ToCharArray();
                        }
                        char[] result = new char[bits[0].Length];
                        int numAsterisks = 0;
                        for (int i = 0; i < bits[0].Length; i++)
                        {
                            int initialValue = int.Parse(bits[0][i].ToString());
                            result[i] = char.Parse(initialValue.ToString());
                            foreach (char[] bitArray in bits)
                            {
                                if (int.Parse(bitArray[i].ToString()) != 0 && int.Parse(bitArray[i].ToString()) != 1)
                                    throw new FormatException();
                                else if (int.Parse(bitArray[i].ToString()) != initialValue && result[i] != '*')
                                {
                                    result[i] = '*';
                                    numAsterisks++;
                                }
                            }
                        }
                        if (numAsterisks > numBitStrings / 2)
                            Console.WriteLine("NONE");
                        else Console.WriteLine(result);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Rats! Something went wrong. Moving on to the next iteration...");
                        continue;
                    }
                }
                finished = true;
            } while (!finished);
        }
    }
}
