using System;
using System.IO;

namespace SudokuMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            string puzzlePath = @"C:\Users\sean\source\repos\SudokuSolver\Puzzles\";
            string puzzleName = "puzzle1.txt";

            string fullPath = puzzlePath + puzzleName;
            string puzzleContent = File.ReadAllText(fullPath);

            Console.WriteLine(puzzleContent);
        }
    }
}
