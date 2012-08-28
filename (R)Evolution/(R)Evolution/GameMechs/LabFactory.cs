using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _R_Evolution.GameObjects.Wall;

namespace _R_Evolution.GameMechs
{
    static class LabFactory
    {
        private const int WallRows = 16;
        private const int WallCols = 30;

        private const int WallColMultiplier = 25;
        private const int WallRowMultiplier = 25;

        private static readonly Random RandomGenerator;

        static LabFactory()
        {
            RandomGenerator = new Random((int)DateTime.Now.Ticks);
        }

        internal static List<Wall> Create(Game game)
        {
            int mapWidth = game.Window.ClientBounds.Width;
            int mapHeight = game.Window.ClientBounds.Height;

            var result = new Wall[WallRows * WallCols];

            int index = 0, pos_x, pos_y;

            for (int i = 0; i < WallCols;i++ )
            {
                pos_x = WallColMultiplier * (i + 1);

                for (int k = 0; k < WallRows; k++)
                {
                    pos_y = WallRowMultiplier * (k + 1);
                    result[index++] = new Wall(game, pos_x, pos_y, RandomWallOrientation());
                }
            }

            return result.ToList();
        }

        private static WallOrientation RandomWallOrientation()
        {
            return (RandomGenerator.Next() % 2 == 0) ? WallOrientation.Vertical : WallOrientation.Horizontal;
        }
    }
}
