using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACSLRummy
{
    class Deck
    {
        private List<Card> depositDeck;
        private List<Card> run;
        private List<Card> set;

        /// <summary>
        /// Constructs a deck using an array of card data.
        /// </summary>
        /// <param name="cards">Two-character strings representing the cards.</param>
        public Deck(string[] cards)
        {
            depositDeck = new List<Card>();
            foreach (string card in cards)
            {
                depositDeck.Add(new Card(card[0], card[1]));
            }
            checkForRun();
            checkForSet();
            depositDeck = sortByValue(depositDeck);
        }

        /// <summary>
        /// Constructs a deck using pre-defined instance fields. Only for use with the clone method.
        /// </summary>
        /// <param name="currentDeck">Deck data from previous deck.</param>
        /// <param name="currentRun">Run data from previous deck.</param>
        /// <param name="currentSet">Set data from previous deck.</param>
        protected Deck(List<Card> currentDeck, List<Card> currentRun, List<Card> currentSet)
        {
            depositDeck = new List<Card>();
            //The instance fields are input in loops to avoid side effects.
            foreach (Card card in currentDeck)
                depositDeck.Add(card);
            if (currentRun != null)
            {
                run = new List<Card>();
                foreach (Card card in currentRun)
                    run.Add(card);
            }
            if (currentSet != null)
            {
                set = new List<Card>();
                foreach (Card card in currentSet)
                    set.Add(card);
            }
        }

        private List<Card> swap(int a, int b, List<Card> list)
        {
            Card temp = list[a];
            list[a] = list[b];
            list[b] = temp;
            return list;
        }

        /// <summary>
        /// Sorts the input list in order of decreasing value and increasing suit.
        /// </summary>
        /// <param name="list">Unsorted list.</param>
        /// <returns>Sorted list.</returns>
        private List<Card> sortByValue(List<Card> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
                for (int j = 0; j < i; j++)
                {
                    if (list[j].Value < list[j + 1].Value)
                        list = swap(j+1, j, list);
                    else if (list[j].Value == list[j + 1].Value && list[j].Suit > list[j + 1].Suit)
                        list = swap(j+1, j, list);
                }
            return list;
        }

        /// <summary>
        /// Sorts the input list in order of increasing suit and increasing value.
        /// </summary>
        /// <param name="list">Unsorted list.</param>
        /// <returns>Sorted list.</returns>
        private List<Card> sortBySuit(List<Card> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
                for (int j = 0; j < i; j++)
                {
                    if (list[j].Suit > list[j + 1].Suit)
                        list = swap(j+1, j, list);
                    else if (list[j].Suit == list[j + 1].Suit && list[j].Value > list[j + 1].Value)
                        list = swap(j+1, j, list);
                }
            return list;
        }

        /// <summary>
        /// Determines if a run exists within the current deck, and if so, places it into the run field.
        /// </summary>
        private void checkForRun()
        {
            depositDeck = sortBySuit(depositDeck);
            int runLength = 1;
            int runStart = 0;
            bool currentRun = false;
            bool runFound = false;

            for (int i = 1; i < depositDeck.Count; i++)
            {
                if (!currentRun)
                {
                    if (depositDeck[i].Suit == depositDeck[i - 1].Suit && depositDeck[i].Value == depositDeck[i - 1].Value + 1)
                    {
                        //Start new run.
                        currentRun = true;
                        runStart = i - 1;
                        runLength = 2;
                    }
                }
                else if (depositDeck[i].Suit == depositDeck[i - 1].Suit && depositDeck[i].Value == depositDeck[i - 1].Value + 1)
                    //Increment current run.
                    runLength++;
                else
                {
                    if (runLength >= 3)
                    {
                        //The run is complete.
                        runFound = true;
                        break;
                    }
                    currentRun = false;
                }
                //So that runs found at the end are not ignored:
                if (i == depositDeck.Count - 1 && currentRun)
                {
                    if (runLength >= 3)
                    {
                        runFound = true;
                        break;
                    }
                }
            }
            if (runFound)
            {
                //Dump run components into run field and delete them from the original deck.
                run = new List<Card>();
                for (int i = runStart; i < runStart + runLength; i++)
                    run.Add((depositDeck[i]));
                for (int i = runStart + runLength - 1; i >= runStart; i--)
                    depositDeck.RemoveAt(i);
            }
        }

        /// <summary>
        /// Determines if a set exists within the current deck, and if so, places it into the set field.
        /// </summary>
        private void checkForSet()
        {
            depositDeck = sortByValue(depositDeck);
            int setLength = 1;
            int setStart = 0;
            bool setFound = false;
            bool currentSet = false;

            for (int i = 1; i < depositDeck.Count; i++)
            {
                if (!currentSet)
                {
                    if (depositDeck[i].Value == depositDeck[i - 1].Value)
                    {
                        //Start new set.
                        currentSet = true;
                        setStart = i - 1;
                        setLength = 2;
                    } 
                }
                else
                {
                    if (depositDeck[i].Value == depositDeck[i - 1].Value)
                        //Increment current set.
                        setLength++;
                    else
                    {
                        if (setLength >= 3)
                        {
                            //The set is complete.
                            setFound = true;
                            break;
                        }
                        currentSet = false;
                    }
                }
                //So that sets found at the end are not ignored:
                if (i == depositDeck.Count - 1 && currentSet)
                {
                    if (setLength >= 3)
                    {
                        setFound = true;
                        break;
                    }
                }
            }
            if (setFound)
            {
                //Dump set components into run field and delete them from the original deck.
                set = new List<Card>();
                for (int i = setStart; i < setStart + setLength; i++)
                    set.Add((depositDeck[i]));
                for (int i = setStart + setLength - 1; i >= setStart; i--)
                    depositDeck.RemoveAt(i);
            }
        }

        /// <summary>
        /// Adds a card to the deck, keeping it only if it makes or extends a run or set.
        /// </summary>
        /// <param name="cardData"></param>
        public void AddCard(string cardData)
        {
            //Will not add any cards if a win is already present.
            if (!Win())
            {
                bool cardAdded = false;
                Card newCard = new Card(cardData[0], cardData[1]);
                if (run == null || set == null)
                {
                    //Add card to deck to see if it makes a run or set.
                    depositDeck.Add(newCard);
                    if (run == null)
                    {
                        checkForRun();
                        if (run != null)
                            cardAdded = true;
                    }
                    if (set == null && !cardAdded)
                    {
                        checkForSet();
                        if (set != null)
                            cardAdded = true;
                    }
                    if (!cardAdded)
                        //No run or set found.
                        depositDeck.Remove(newCard);
                }
                //Check if the card extends a run.
                if (run != null && run.Count != 4 && !cardAdded)
                {
                    if (newCard.Suit == run[0].Suit)
                    {
                        if (newCard.Value == run[0].Value - 1)
                        {
                            run.Insert(0, newCard);
                            cardAdded = true;
                        }
                        else if (newCard.Value == run[run.Count - 1].Value + 1)
                        {
                            run.Add(newCard);
                            cardAdded = true;
                        }
                    }
                }
                //Check if the card extends a set.
                if (set != null && set.Count != 4 && !cardAdded)
                {
                    if (newCard.Value == set[0].Value)
                    {
                        for (int i = 0; i < set.Count; i++)
                        {
                            if (newCard.Suit < set[i].Suit)
                            {
                                set.Insert(i, newCard);
                                cardAdded = true;
                                break;
                            }
                            else if (i == set.Count - 1)
                            {
                                set.Add(newCard);
                                cardAdded = true;
                                break;
                            }
                        }
                    }
                }
                depositDeck = sortByValue(depositDeck);
                if (cardAdded) Discard(); //To keep the count at 7.
            }
        }

        /// <summary>
        /// Throws out a card according to ACSLRummy rules.
        /// </summary>
        public void Discard()
        {
            depositDeck.RemoveAt(depositDeck.Count - 1); 
            //By the rules of discarding, the card deleted is the last one in a sortByValue.
        }

        /// <summary>
        /// Constructs a deck equivalent to this one but with no side effects.
        /// </summary>
        /// <returns>The cloned deck.</returns>
        public Deck Clone()
        {
            return new Deck(depositDeck, run, set);
        }

        /// <summary>
        /// Constructs the list of cards in proper output order.
        /// </summary>
        /// <returns>The list of cards as a string.</returns>
        public override string ToString()
        {
            string deckString = "";
            if (run != null)
                foreach (Card card in run)
                    deckString += card + ", ";
            if (set != null)
                foreach (Card card in set)
                    deckString += card + ", "; 
            foreach (Card card in depositDeck)
                deckString += card + ", ";
            deckString = deckString.Remove(deckString.Length - 2);
            return deckString;
        }

        /// <summary>
        /// Determines if the game of Rummy has been won.
        /// </summary>
        /// <returns>True if winning hand.</returns>
        private bool Win()
        {
            if (run != null && set != null)
                return (set.Count == 4 && run.Count == 3) || (set.Count == 3 && run.Count == 4);
            return false;
        }
    }
}
