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
            string puzzleDir = @"C:\Users\sean\source\repos\SudokuSolver\Puzzles\"; // For Sean
            //string puzzleDir = @"../Puzzles/"; // For UNIX-style FS with curdir SudokuMaster
            string puzzleName = "puzzle4.txt";
            string puzzlePath = puzzleDir+puzzleName;
            if (args.Length>0) puzzlePath = args[0]; // override puzzlePath by providing as first arg
            
            SudokuGrid grid = LogicService.InitializePuzzle(puzzlePath);
            if (args.Length>1&&args[1].Equals("fast")) {
                if (LogicService.FastSolve(grid)) grid.IsSolved = true;
                //else throw(new Exception("No Solution"));
                else {
                    Console.WriteLine("No solution");
                    System.Environment.Exit(1);
                }
            }

            while (grid.IsSolved == false)
            {
                LogicService.NarrowDownCanidates(ref grid);
                while (LogicService.PopulateSingledOutCanidates(ref grid))
                    ValidationService.CheckIfSolved(ref grid);
            }
            ImportExportService.ExportCompletedPuzzleSolution(grid, puzzlePath);
        }

    }
}
