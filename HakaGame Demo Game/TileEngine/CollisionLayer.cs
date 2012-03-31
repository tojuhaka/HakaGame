using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HakaGame
{
    public enum CollisionType { Passable, Unpassable };

    // We need a single layer to determine all the collision
    // in the game. It's much easier to handle this way

    public class CollisionLayer
    {
        CollisionType[,] map;
        public CollisionLayer(int width, int height)
        {
            map = new CollisionType[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    SetTile(x, y, CollisionType.Passable);
                }
            }
        }

        public CollisionType GetTile(int x, int y)
        {
            return map[y, x];
        }

        public void SetTile(int x, int y, CollisionType value)
        {
            map[y, x] = value;
        }
    }
}
