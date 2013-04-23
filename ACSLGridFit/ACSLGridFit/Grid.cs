using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACSLGridFit
{
    class Grid
    {
        private static int[,] myGrid = {{31, 32, 33, 34, 35},
                                           {26, 27, 28, 29, 30},
                                           {21, 22, 23, 24, 25},
                                           {16, 17, 18, 19, 20},
                                           {11, 12, 13, 14, 15},
                                           {6, 7, 8, 9, 10},
                                           {1, 2, 3, 4, 5}};
        private List<int> myFilledSquares;
        public Grid(int[] filledSquares) 
        { 
            myFilledSquares = new List<int>();
            myFilledSquares.AddRange(filledSquares);
        }

        private void fillSquare(int number) { if (number != 36) myFilledSquares.Add(number); }

        public int FitOneByOne()
        {
            int[] results = {FitOneByOne(new GridPoint(0, 0)), FitOneByOne(new GridPoint(0, 1)), FitOneByOne(new GridPoint(0, 2)),
            FitOneByOne(new GridPoint(0, 3)), FitOneByOne(new GridPoint(0, 4))};
            int least = FindLeast(results);
            fillSquare(least);
            return least;
        }

        private int FitOneByOne(GridPoint currentLoc)
        {
            if (currentLoc.R >= myGrid.GetLength(0)) return 36;
            if (FindFilled(myGrid[currentLoc.R, currentLoc.C]))
                return 36;
            else
            {
                int nextOption = FitOneByOne(new GridPoint(currentLoc.R + 1, currentLoc.C));
                if (nextOption == 36)
                    return myGrid[currentLoc.R, currentLoc.C];
                else return nextOption;
            }
        }

        public int FitOneByTwo()
        {
            int[] results = {FitOneByTwo(new GridPoint(1, 0)), FitOneByTwo(new GridPoint(1, 1)), FitOneByTwo(new GridPoint(1, 2)),
            FitOneByTwo(new GridPoint(1, 3)), FitOneByTwo(new GridPoint(1, 4))};
            int least = FindLeast(results);
            fillSquare(least);
            fillSquare(least + 5);
            return least;
        }

        private int FitOneByTwo(GridPoint currentLoc)
        {
            if (currentLoc.R >= myGrid.GetLength(0)) return 36;
            if (FindFilled(myGrid[currentLoc.R, currentLoc.C]) || FindFilled(myGrid[currentLoc.R - 1, currentLoc.C]))
                return 36;
            else
            {
                int nextOption = FitOneByTwo(new GridPoint(currentLoc.R + 1, currentLoc.C));
                if (nextOption == 36)
                    return myGrid[currentLoc.R, currentLoc.C];
                else return nextOption;
            }
        }

        public int FitTwoByOne()
        {
            int[] results = {FitTwoByOne(new GridPoint(0, 0)), FitTwoByOne(new GridPoint(0, 1)), FitTwoByOne(new GridPoint(0, 2)),
            FitTwoByOne(new GridPoint(0, 3))};
            int least = FindLeast(results);
            fillSquare(least);
            fillSquare(least + 1);
            return least;
        }

        private int FitTwoByOne(GridPoint currentLoc)
        {
            if (currentLoc.R >= myGrid.GetLength(0)) return 36;
            if (FindFilled(myGrid[currentLoc.R, currentLoc.C]) || FindFilled(myGrid[currentLoc.R, currentLoc.C+1]))
                return 36;
            else
            {
                int nextOption = FitTwoByOne(new GridPoint(currentLoc.R + 1, currentLoc.C));
                if (nextOption == 36)
                    return myGrid[currentLoc.R, currentLoc.C];
                else return nextOption;
            }
        }

        public int FitTopCornerPiece()
        {
            int[] results = {FitTopCornerPiece(new GridPoint(1, 0)), FitTopCornerPiece(new GridPoint(1, 1)), FitTopCornerPiece(new GridPoint(1, 2)),
            FitTopCornerPiece(new GridPoint(1, 3))};
            int least = FindLeast(results);
            fillSquare(least);
            fillSquare(least + 6);
            fillSquare(least + 5);
            return least;
        }

        private int FitTopCornerPiece(GridPoint currentLoc)
        {
            if (currentLoc.R >= myGrid.GetLength(0)) return 36;
            if (FindFilled(myGrid[currentLoc.R, currentLoc.C]) || FindFilled(myGrid[currentLoc.R - 1, currentLoc.C + 1]) || FindFilled(myGrid[currentLoc.R - 1, currentLoc.C]))
                return 36;
            else
            {
                int nextOption = FitTopCornerPiece(new GridPoint(currentLoc.R + 1, currentLoc.C));
                if (nextOption == 36)
                    return myGrid[currentLoc.R, currentLoc.C];
                else return nextOption;
            }
        }

        public int FitBottomCornerPiece()
        {
            int[] results = {FitBottomCornerPiece(new GridPoint(1, 0)), FitBottomCornerPiece(new GridPoint(1, 1)), FitBottomCornerPiece(new GridPoint(1, 2)),
            FitBottomCornerPiece(new GridPoint(1, 3))};
            int least = FindLeast(results);
            fillSquare(least);
            fillSquare(least + 1);
            fillSquare(least + 5);
            return least;
        }

        private int FitBottomCornerPiece(GridPoint currentLoc)
        {
            if (currentLoc.R >= myGrid.GetLength(0)) return 36;
            if (FindFilled(myGrid[currentLoc.R, currentLoc.C]) || FindFilled(myGrid[currentLoc.R, currentLoc.C + 1]) || FindFilled(myGrid[currentLoc.R - 1, currentLoc.C]) || FindFilled(myGrid[currentLoc.R - 1, currentLoc.C + 1]))
                return 36;
            else
            {
                int nexOption = FitBottomCornerPiece(new GridPoint(currentLoc.R + 1, currentLoc.C));
                if (nexOption == 36)
                    return myGrid[currentLoc.R, currentLoc.C];
                else return nexOption;
            }
        }

        private int FindLeast(int[] options)
        {
            int least = 36;
            foreach (int num in options)
            {
                if (num < least)
                    least = num;
            }
            return least;
        }

        private bool FindFilled(int number)
        {
            foreach (int square in myFilledSquares)
            {
                if (number == square) return true;
            }
            return false;
        }
    }

    class GridPoint
    {
        public int R;
        public int C;
        public GridPoint(int r, int c)
        {
            R = r;
            C = c;
        }
    }
}
