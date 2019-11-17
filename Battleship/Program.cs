using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            BattleGround battleGroundShips = new BattleGround();
            BattleGround battleGroundHits = new BattleGround();
            List<Ship> ships = new List<Ship>();
            List<int> shipSizes = new List<int>
            {
                4,4,5,
            };

            Console.WriteLine("Please specify the position of your ships. You will have \n - 1x Battleship (5 squares) \n - 2x Destroyers (4 squares) \n Specify the coordinates from start to end in line or column. Ex. for Battleship: A1-A5");


            while (shipSizes.Count() > 0)
            {
                string introducedData = Console.ReadLine().Replace(" ", string.Empty);
                List<Coordinates> shipCoordinates = GetShipCoordinates(introducedData);
                if (shipCoordinates == null)
                {
                    Console.WriteLine("Please provide valide coordinates");
                }
                else {
                    int width = shipCoordinates.Count();
                    if (shipSizes.Contains(width))
                    {
                        List<Tile> tiles = battleGroundShips.Tiles.Where(t => shipCoordinates.Where(c => c.Column == t.Coordinates.Column && c.Row == t.Coordinates.Row).Any()).ToList();
                        if (tiles.Where(t => t.TileStatus == TileStatus.Empty).Count() == tiles.Count())
                        {
                            foreach (var tile in tiles)
                            {
                                tile.TileStatus = TileStatus.Ship;
                            }

                            ships.Add(new Ship(width)
                            {
                                Coordinates = new List<Coordinates>(),
                                TileStatus = TileStatus.Ship
                            });
                            ships.Last().Coordinates = shipCoordinates;

                            OutputBoards(battleGroundShips);
                            shipSizes.Remove(width);
                        }
                        else
                        {
                            Console.WriteLine("Some of the spaces are already taken");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You have already used all ships with " +  width + " cells");
                    }
                }
                
            }

            Console.WriteLine("Now you can start firing !");
            Coordinates coord = new Coordinates(0, 0);

            OutputBoards(battleGroundHits);

            while (ships.Where(s => s.Drowned == false).Any())
            {
                string hitCoordinate = Console.ReadLine().Replace(" ", string.Empty);
                if (hitCoordinate.Length > 0)
                {
                    coord = coord.GetCoordinates(hitCoordinate);
                    Tile tile = battleGroundHits.Tiles.FirstOrDefault(t => t.Coordinates.Column == coord.Column && t.Coordinates.Row == coord.Row);
                    int index = battleGroundHits.Tiles.IndexOf(tile);
                    if (battleGroundShips.Tiles[index].IsUsed)
                    {
                        tile.TileStatus = TileStatus.Hit;
                        ships.FirstOrDefault(s => s.Coordinates.Where(c => c.Column == coord.Column && c.Row == coord.Row).Any()).Hits++;
                    }
                    else
                    {
                        tile.TileStatus = TileStatus.Miss;
                    }
                    OutputBoards(battleGroundHits);
                }
            }

            Console.WriteLine("Congrats, you destroyed all ships !!!");
            Console.ReadLine();
            Console.ReadLine();
            Console.ReadLine();

            void OutputBoards(BattleGround battleGround)
            {
                Console.WriteLine("Board:");
                for (int row = 1; row <= 10; row++)
                {
                    for (int ownColumn = 1; ownColumn <= 10; ownColumn++)
                    {
                        Console.Write(battleGround.Tiles.First(t => t.Coordinates.Row == row && t.Coordinates.Column == ownColumn).Status + " ");
                    }
                    Console.WriteLine(Environment.NewLine);
                }
                Console.WriteLine(Environment.NewLine);
            }
        }

        public static List<Coordinates> GetShipCoordinates(string shipCoordinates)
        {
            string[] coordinates = shipCoordinates.Split('-');



            int startX = Int32.Parse(Regex.Match(coordinates[0], @"\d+").Value);
            int finX = Int32.Parse(Regex.Match(coordinates[1], @"\d+").Value);
            int startY = Global.LINES.IndexOf(new String(coordinates[0].Where(Char.IsLetter).ToArray())) + 1;
            int finY = Global.LINES.IndexOf(new String(coordinates[1].Where(Char.IsLetter).ToArray())) + 1;
            if (!(Enumerable.Range(1, 10).Contains(startX) && Enumerable.Range(1, 10).Contains(finX)
                && startX - finX == 0 || startY - finY == 0))
            {
                return null;
            }

            List<Coordinates> coordList = new List<Coordinates>();
            if (startX - finX == 0 && startY - finY != 0)
            {
                for (int i = 0; i <= System.Math.Abs(startY - finY); i++)
                {
                    coordList.Add(new Coordinates(startY < finY ? startY + i : finY + i, startX));
                }
            }
            else if (startX - finX != 0 && startY - finY == 0)
            {
                for (int i = 0; i <= System.Math.Abs(startX - finX); i++)
                {
                    coordList.Add(new Coordinates(startY, startX < finX ? startX + i : finX + i));
                }
            }
            else
            {
                return null;
            }
            return coordList;
        }
    }
}
