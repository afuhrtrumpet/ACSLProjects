using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACSLTimeSheets
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
                string[] tokens = line.Split(' ');
                double[] hoursPerDay = new double[tokens.Length - 1];
                int location = int.Parse(tokens[0].Trim(','));
                for (int i = 0; i < hoursPerDay.Length; i++)
                {
                    string[] interval = tokens[i+1].Split(',');
                    hoursPerDay[i] = determineHour(char.Parse(interval[1].Trim(','))) - determineHour(char.Parse(interval[0]));
                }
                Console.WriteLine("$" + Math.Round(determinePay(location, hoursPerDay), 2).ToString("N2"));
            }
            Console.Read();
        }

        static double determinePay(int loc, double[] hours)
        {
            double totalHours = 0;
            for (int i = 0; i < hours.Length; i++)
             totalHours += hours[i];
            if (loc < 200)
            {
                if (totalHours < 30)
                    return 10 * totalHours;
                else return 10 * totalHours + 5 * (totalHours - 30);
            }
            else if (loc < 300)
            {
                if (totalHours < 40)
                    return 7.5 * totalHours;
                else return 7.5 * totalHours + 7.5 * (totalHours - 40);
            }
            else if (loc < 400)
            {
                if (totalHours < 20)
                    return 9.25 * totalHours;
                else return 9.25 * 20 + 10.5 * (totalHours - 20);
            }
            else if (loc < 500)
            {
                double sum = 0;
                for (int i = 0; i < hours.Length; i++)
                {
                    double salary = 0;
                    if (i == 0 || i == 6) salary = 13.5;
                    else salary = 6.75;
                    sum += salary * hours[i];
                }
                return sum;
            }
            else
            {
                double sum = 0;
                for (int i = 0; i < hours.Length; i++)
                {
                    if (hours[i] < 6) sum += 8 * hours[i];
                    else sum += 8 * hours[i] + 4 * (hours[i] - 6);
                }
                return sum;
            }
            return -1;
        }

        static double determineHour(char mander)
        {
            switch (mander)
            {
                case '1': return 0; break;
                case '2': return 0.5; break;
                case '3': return 1; break;
                case '4': return 1.5; break;
                case '5': return 2; break;
                case '6': return 2.5; break;
                case '7': return 3; break;
                case '8': return 3.5; break;
                case '9': return 4; break;
                case 'A': return 4.5; break;
                case 'B': return 5; break;
                case 'C': return 5.5; break;
                case 'D': return 6; break;
                case 'E': return 6.5; break;
                case 'F': return 7; break;
                case 'G': return 7.5; break;
                case 'H': return 8; break;
            }
            return -1;
        }
    }
}
