using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACSLRummy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input file name:  ");
            string filename = Console.ReadLine();
            StreamReader reader = new StreamReader(filename);
            string[] deckData = reader.ReadLine().Split(' ');
            for (int i = 0; i < deckData.Length; i++)
            {
                deckData[i] = deckData[i].TrimEnd(',');
            }
            Deck originalDeck = new Deck(deckData);
            while (!reader.EndOfStream)
            {
                Deck deck = originalDeck.Clone();
                string[] draw = reader.ReadLine().Split(' ');
                for (int i = 0; i < draw.Length; i++)
                {
                    draw[i] = draw[i].TrimEnd(',');
                    deck.AddCard(draw[i]);
                }
                Console.WriteLine(deck);
            }
        }
    }
}
