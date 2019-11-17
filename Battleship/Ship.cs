using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Ship
    {
        public List<Coordinates> Coordinates { get; set; }
        public TileStatus TileStatus { get; set; }
        public int Width { get; set; }
        public int Hits { get; set; }
        public bool Drowned
        {
            get
            {
                return Hits >= Width;
            }
        }

        public Ship(int width)
        {
            Width = width;
            TileStatus = TileStatus.Ship;
        }
    }
}
