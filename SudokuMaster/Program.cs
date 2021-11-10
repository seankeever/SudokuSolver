using SudokuMaster.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace SudokuMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            string puzzlePath = @"C:\Users\sean\source\repos\SudokuSolver\Puzzles\";
            string puzzleName = "puzzle5.txt";
            
            SudokuGrid grid = InitializePuzzle(puzzlePath,puzzleName);

            while (grid.IsSolved == false)
            {
                NarrowDownCanidates(ref grid);

                bool wasNarrowedDown = true;
                while (wasNarrowedDown)
                {
                    wasNarrowedDown = PopulateSingledOutCanidates(ref grid);
                    CheckIfSolved(ref grid);
                }

            }
            PrintGrid(grid);


        }

        private static bool CheckIfSolved(ref SudokuGrid grid)
        {
            for(int i=1;i<=9; i++)
            {
                if (!ValidateSet(grid.GetRowAt(i)))
                    return false;

                if (!ValidateSet(grid.GetColumnAt(i)))
                    return false;

                if (!ValidateSet(grid.GetRegionAt(i)))
                    return false;
            }

            grid.IsSolved = true;
            return true;
        }

        private static bool ValidateSet(List<SudokuSquare> focussSet)
        {
            List<int> values = new List<int>();

            foreach(SudokuSquare square in focussSet)
                if (square.Value != 0)
                    values.Add(square.Value);

            if (values.Count != 9)
                return false;

            for (int i = 1; i <= 9; i++)
                if (!values.Contains(i))
                    return false;

            return true;
        }


        private static bool PopulateSingledOutCanidates(ref SudokuGrid grid)
        {
            bool wasNarrowedDown = false;
            foreach(SudokuSquare square in grid)
            {
                if(square.IsSet == false)
                {
                    List<int> canidates = GetValidCanidates(square);
                    if (canidates.Count == 1)
                    {
                        grid.GetSquareAt(square.Row, square.Column).Value = canidates[0];
                        grid.GetSquareAt(square.Row, square.Column).IsSet = true;
                        wasNarrowedDown = true;
                    }
                        
                }
            }
            return wasNarrowedDown;
        }

        private static List<int> GetValidCanidates(SudokuSquare square)
        {
            List<int> validCanidates = new List<int>();
            CanidateList list = square.CanidateList;

            foreach(Canidate c in list)
                if(!c.IsCrossedOff)
                    validCanidates.Add(c.Value);

            return validCanidates;
        }

        private static void NarrowDownCanidates(ref SudokuGrid grid)
        {
            for(int row = 1; row<=9; row++)
            {
                List<SudokuSquare> currentRow = grid.GetRowAt(row);
                List<int> permanentValues = GetPermanentValues(currentRow);
                ScratchOutTakenValues(ref grid, currentRow, permanentValues);
            }

            for (int column = 1; column <= 9; column++)
            {
                List<SudokuSquare> currentColumn = grid.GetColumnAt(column);
                List<int> permanentValues = GetPermanentValues(currentColumn);
                ScratchOutTakenValues(ref grid, currentColumn, permanentValues);
            }

            for (int region = 1; region <= 9; region++)
            {
                List<SudokuSquare> currentRegion = grid.GetRegionAt(region);
                List<int> permanentValues = GetPermanentValues(currentRegion);
                ScratchOutTakenValues(ref grid, currentRegion, permanentValues);
            }
        }

        private static void ScratchOutTakenValues(ref SudokuGrid grid, List<SudokuSquare> focussSet, List<int> permanentValues)
        {
            foreach(SudokuSquare square in focussSet)
            {
                foreach (int permanentValue in permanentValues)
                    grid.GetSquareAt(square.Row, square.Column).CanidateList.GetCanidateByValue(permanentValue).IsCrossedOff = true;
            }
        }

        private static List<int> GetPermanentValues(List<SudokuSquare> focussSet)
        {
            List<int> permanentValues = new List<int>();
            foreach (SudokuSquare square in focussSet)
                if (square.Value != 0)
                    permanentValues.Add(square.Value);
            return permanentValues;
        }



        private static SudokuGrid InitializePuzzle(string puzzlePath, string puzzleName)
        {
            StreamReader st = new StreamReader(puzzlePath + puzzleName);
            List<string> rows = new List<string>();
            rows.Add("Blank Row to fill up 0'th row index to improve understandability");
            while (!st.EndOfStream)
                rows.Add("_"+st.ReadLine());

            SudokuGrid grid = new SudokuGrid();
            
            for(int row=1;row<=9;row++)
            {
                for(int column=1;column<=9;column++)
                {
                    string currentInputValue = rows[row][column].ToString();
                    if (currentInputValue != "X")
                    {
                        int permanentValue = int.Parse(currentInputValue.ToString());
                        SudokuSquare currentSquare = grid.GetSquareAt(row, column);
                        currentSquare.Value = permanentValue;
                        currentSquare.IsSet = true;
                    }
                }
            }

            return grid;
        }







        //---------------------------------------------------------------------------------------
        ////--------------------------------- printing methods ----------------------------------
        //---------------------------------------------------------------------------------------
        private static void PrintGrid(SudokuGrid grid, char emptyChar = 'X')
        {
            for (int row = 1; row <= 9; row++)
            {
                string currentLine = string.Empty;
                for (int column = 1; column <= 9; column++)
                {
                    currentLine += grid.GetSquareAt(row, column).Value.ToString();
                }
                currentLine = currentLine.Replace('0', emptyChar);
                Console.WriteLine(currentLine);
            }
        }
        private static void PrintSquareByID(SudokuGrid grid, int squareID)
        {
            foreach (SudokuSquare square in grid)
            {
                if (square.SquareID == squareID)
                {
                    PrintSquare(square);
                }
            }
        }

        private static void PrintSquare(SudokuSquare square)
        {
            Console.WriteLine("ID: " + square.SquareID.ToString());
            Console.WriteLine("Row: " + square.Row.ToString());
            Console.WriteLine("Column: " + square.Column.ToString());
            Console.WriteLine("Is Set: " + square.IsSet.ToString());
            Console.WriteLine("Canidate List: " + "\n");

            foreach(Canidate c in square.CanidateList)
                if (!c.IsCrossedOff)
                    Console.WriteLine(c.Value.ToString());
        }
    }
}
//static void Main(string[] args)
//{
//    string puzzlePath = @"C:\Users\sean\source\repos\SudokuSolver\Puzzles\";
//    string puzzleName = "puzzle1.txt";

//    StreamReader st = new StreamReader(puzzlePath + puzzleName);

//    List<string> rows = new List<string>();
//    while (!st.EndOfStream)
//    {
//        rows.Add(st.ReadLine());
//    }
//}