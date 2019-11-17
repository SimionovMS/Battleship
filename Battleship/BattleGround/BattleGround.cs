using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class BattleGround
    {
        public List<Tile> Tiles { get; set; }

        public BattleGround ()
        {
            Tiles = new List<Tile>();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Tiles.Add(new Tile(i, j));
                }
            }

        }
    }
}
