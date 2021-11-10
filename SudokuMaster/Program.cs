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
            string puzzleName = "puzzle1.txt";



            
            SudokuGrid grid = InitializePuzzle(puzzlePath,puzzleName);
            NarrowDownCanidates(ref grid);

            PrintGrid(grid, '_');

        }

        private static void NarrowDownCanidates(ref SudokuGrid grid)
        {
            for(int row = 1; row<=9; row++)
            {
                List<SudokuSquare> currentRow = grid.GetRowAt(row);
                List<int> permanentValues = GetPermanentValues(currentRow);
                ScratchOutTakenValues(ref grid, currentRow, permanentValues);
            }

        }

        private static void ScratchOutTakenValues(ref SudokuGrid grid, List<SudokuSquare> currentRow, List<int> permanentValues)
        {
            foreach(SudokuSquare square in currentRow)
            {
                foreach (int permanentValue in permanentValues)
                    grid.GetSquareAt(square.Row, square.Column).CanidateList.GetCanidateByValue(permanentValue).IsCrossedOff = true;
            }
        }

        private static List<int> GetPermanentValues(List<SudokuSquare> currentRow)
        {
            List<int> permanentValues = new List<int>();
            foreach (SudokuSquare square in currentRow)
                if (square.Value != 0)
                    permanentValues.Add(square.Value);
            return permanentValues;
        }

        private static void PrintGrid(SudokuGrid grid, char emptyChar = 'X' )
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
                    }
                }
            }

            return grid;
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