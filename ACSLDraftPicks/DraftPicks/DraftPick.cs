using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftPicks
{
    class DraftPick
    {
        private int myLengthYears;
        private double myValue, myGuaranteedMoney;
        private const double CHANCE_OF_INJURY_16 = .03, CHANCE_OF_INJURY_18 = .03375;

        public DraftPick(int length, double value, double guaranteedMoney)
        {
            if (myLengthYears == 0)
            myLengthYears = length;
            myValue = value;
            myGuaranteedMoney = guaranteedMoney;
        }

        public double expectedValue16()
        {
            return ((1 - myLengthYears * CHANCE_OF_INJURY_16) * myValue + myLengthYears * CHANCE_OF_INJURY_16 * myGuaranteedMoney) * 1000000;
        }

        public double expectedValue18()
        {
            return ((1 - myLengthYears * CHANCE_OF_INJURY_18) * myValue + myLengthYears * CHANCE_OF_INJURY_18 * myGuaranteedMoney) * 1000000;
        }

        public int length() {return myLengthYears;}
        public double value() {return myValue;}
        public double guaranteedMoney() {return myGuaranteedMoney;}
    }
}
