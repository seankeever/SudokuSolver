using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuMaster.Models
{
    public class SudokuSquare
    {
        public SudokuSquare()
        {

        }

        public SudokuSquare(int Row, int Column, int Region)
        {
            this.SquareID = SquareID;
            this.Row = Row;
            this.Column = Column;
            this.Region = Region;
        }
        
        public int SquareID { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Region { get; set; }
    }
}
