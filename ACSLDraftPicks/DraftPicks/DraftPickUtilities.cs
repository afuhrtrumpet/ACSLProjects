using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftPicks
{
    static class DraftPickUtilities
    {
        public static double rangeOfSalaries16(List<DraftPick> draftPicks)
        {
            double highestSalary = 0, lowestSalary = double.MaxValue;
            foreach (DraftPick draftPick in draftPicks)
            {
                double salary = draftPick.value() * 1000000 / draftPick.length() / 16;
                if (salary > highestSalary) highestSalary = salary;
                if (salary < lowestSalary) lowestSalary = salary;
            }
            return highestSalary - lowestSalary;
        }

        public static double midRangeOfSalaries18(List<DraftPick> draftPicks)
        {
            double highestSalary = 0, lowestSalary = double.MaxValue;
            foreach (DraftPick draftPick in draftPicks)
            {
                double salary = draftPick.value() * 1000000 / draftPick.length() / 18;
                if (salary > highestSalary) highestSalary = salary;
                if (salary < lowestSalary) lowestSalary = salary;
            }
            return (highestSalary + lowestSalary) / 2;
        }

        public static int highestValuedPlayer16(List<DraftPick> draftPicks)
        {
            int highestValued = 0;
            for (int i = 0; i < draftPicks.Count; i++)
            {
                if (draftPicks[i].expectedValue16() > draftPicks[highestValued].expectedValue16())
                    highestValued = i;
            }
            return highestValued;
        }

        public static double averageExpectedValue18(List<DraftPick> draftPicks)
        {
            double sum = 0;
            foreach (DraftPick draftPick in draftPicks)
            {
                sum += draftPick.expectedValue18();
            }
            return sum / draftPicks.Count;
        }

        private static List<DraftPick> sortByAnnualSalary(List<DraftPick> draftPicks)
        {
            List<DraftPick> clonedDraftPicks = new List<DraftPick>();
            foreach (DraftPick draftPick in draftPicks)
                clonedDraftPicks.Add(draftPick);
            List<DraftPick> sortedList = new List<DraftPick>();
            for (int i = 0; i < draftPicks.Count; i++)
            {
                int lowestIndex = 0;
                DraftPick lowestRemaining = new DraftPick(1, double.MaxValue, 0);
                for (int j = 0; j < clonedDraftPicks.Count; j++)
                {
                    if (clonedDraftPicks[j].value() / clonedDraftPicks[j].length() < lowestRemaining.value() / lowestRemaining.length())
                    {
                        lowestRemaining = clonedDraftPicks[j];
                        lowestIndex = j;
                    }
                }
                sortedList.Add(lowestRemaining);
                clonedDraftPicks.RemoveAt(lowestIndex);
            }
            return sortedList;
        }

        public static double medianOfAnnualSalary(List<DraftPick> draftPicks)
        {
            List<DraftPick> sortedList = sortByAnnualSalary(draftPicks);
            if (sortedList.Count % 2 == 0)
            {
                return (sortedList[sortedList.Count / 2 - 1].value() / sortedList[sortedList.Count / 2 -1].length() + sortedList[sortedList.Count / 2].value() / sortedList[sortedList.Count / 2].length()) / 2 * 1000000;
            }
            else return sortedList[sortedList.Count / 2 - 1].value() / sortedList[sortedList.Count / 2 - 1].length() * 1000000;
        }
    }
}
