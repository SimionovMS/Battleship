using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Tile
    {
        public Coordinates Coordinates { get; set; }
        public TileStatus TileStatus { get; set; }

        public Tile (int row, int column)
        {
            Coordinates = new Coordinates(row, column);
            TileStatus = TileStatus.Empty;
        }

        public bool IsUsed { get { return TileStatus != TileStatus.Empty; } }

        public string Status
        {
            get
            {   
                return TileStatus.GetDescription();
            }
        }
    }
}
