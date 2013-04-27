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

        protected Deck(List<Card> currentDeck, List<Card> currentRun, List<Card> currentSet)
        {
            depositDeck = new List<Card>();
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
                        currentRun = true;
                        runStart = i - 1;
                        runLength = 2;
                    }
                }
                else if (depositDeck[i].Suit == depositDeck[i - 1].Suit && depositDeck[i].Value == depositDeck[i - 1].Value + 1)
                    runLength++;
                else
                {
                    if (runLength >= 3)
                    {
                        runFound = true;
                        break;
                    }
                    currentRun = false;
                }
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
                run = new List<Card>();
                for (int i = runStart; i < runStart + runLength; i++)
                    run.Add((depositDeck[i]));
                for (int i = runStart + runLength - 1; i >= runStart; i--)
                    depositDeck.RemoveAt(i);
            }
        }

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
                        currentSet = true;
                        setStart = i - 1;
                        setLength = 2;
                    } 
                }
                else
                {
                    if (depositDeck[i].Value == depositDeck[i - 1].Value)
                        setLength++;
                    else
                    {
                        if (setLength >= 3)
                        {
                            setFound = true;
                            break;
                        }
                        currentSet = false;
                    }
                }
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
                set = new List<Card>();
                for (int i = setStart; i < setStart + setLength; i++)
                    set.Add((depositDeck[i]));
                for (int i = setStart + setLength - 1; i >= setStart; i--)
                    depositDeck.RemoveAt(i);
            }
        }

        public void AddCard(string cardData)
        {
            if (!Win())
            {
                bool cardAdded = false;
                Card newCard = new Card(cardData[0], cardData[1]);
                if (run == null || set == null)
                {
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
                        depositDeck.Remove(newCard);
                }
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
                if (cardAdded) Discard();
            }
        }

        public void Discard()
        {
            depositDeck.RemoveAt(depositDeck.Count - 1);
        }

        public Deck Clone()
        {
            return new Deck(depositDeck, run, set);
        }

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

        private bool Win()
        {
            if (run != null && set != null)
                return (set.Count == 4 && run.Count == 3) || (set.Count == 3 && run.Count == 4);
            return false;
        }
    }
}
