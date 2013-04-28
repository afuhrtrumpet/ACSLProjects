using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACSLRummy
{
    class Card
    {
        private char cValue, cSuit;
        private int myValue = 0;
        private int mySuit = 0;

        /// <summary>
        /// Constructs a card based on characters representing the value and suit.
        /// </summary>
        /// <param name="value">Value of the card (ace, five, jack, king, etc) in character form.</param>
        /// <param name="suit">Suit of the card, either S, H, C, or D.</param>
        public Card(char value, char suit)
        {
            cValue = value;
            cSuit = suit;
            switch (value)
            {
                case 'A': myValue = 1; break;
                case '2': myValue = 2; break;
                case '3': myValue = 3; break;
                case '4': myValue = 4; break;
                case '5': myValue = 5; break;
                case '6': myValue = 6; break;
                case '7': myValue = 7; break;
                case '8': myValue = 8; break;
                case '9': myValue = 9; break;
                case 'T': myValue = 10; break;
                case 'J': myValue = 11; break;
                case 'Q': myValue = 12; break;
                case 'K': myValue = 13; break;
            }

            switch (suit)
            {
                case 'S': mySuit = 0; break;
                case 'H': mySuit = 1; break;
                case 'C': mySuit = 2; break;
                case 'D': mySuit = 3; break;
            }
        }

        public int Value
        {
            get
            {
                return myValue;
            }
        }

        public int Suit
        {
            get
            {
                return mySuit;
            }
        }

        /// <summary>
        /// Put's the card back into a two-character string.
        /// </summary>
        /// <returns>The two-character string the card was constructed with.</returns>
        public override string ToString()
        {
            return cValue.ToString() + cSuit.ToString();
        }
    }
}
