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

            StreamReader st = new StreamReader(puzzlePath + puzzleName);

            List<string> rows = new List<string>();
            while (!st.EndOfStream)
            {
                rows.Add(st.ReadLine());
            }


            SudokuGrid Grid = new SudokuGrid();

            foreach(SudokuSquare square in Grid)
            {
                if (square.Region == 9)
                    Console.WriteLine(square.SquareID);
            }

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