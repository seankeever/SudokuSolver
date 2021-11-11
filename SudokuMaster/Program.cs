using SudokuMaster.Models;
using SudokuMaster.Services;
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
            string puzzleName = "puzzle4.txt";
            
            SudokuGrid grid = LogicService.InitializePuzzle(puzzlePath,puzzleName);

            while (grid.IsSolved == false)
            {
                LogicService.NarrowDownCanidates(ref grid);
                while (LogicService.PopulateSingledOutCanidates(ref grid))
                    ValidationService.CheckIfSolved(ref grid);
            }
            PrintingService.PrintGrid(grid);
        }

    }
}
