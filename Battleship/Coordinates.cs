using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Coordinates
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Coordinates (int row, int column)
        {
            Row = row;
            Column = column;
        }

        public Coordinates GetCoordinates(string coord)
        {
            return new Coordinates(Global.LINES.IndexOf(coord[0]) + 1, Int32.Parse(coord[1].ToString()));
        }
    }
}
