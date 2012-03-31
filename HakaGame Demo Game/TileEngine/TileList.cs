using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HakaGame;

namespace HakaGame
{
    public class TileList
    {
        Dictionary<int, Tile> dict = new Dictionary<int, Tile>();

        public Dictionary<int, Tile> Dict
        {
            get { return dict; }
        }

        public TileList()
        {

        }

        public void Add(Tile tile)
        {
            if (!dict.ContainsValue(tile))
            {
                dict.Add(tile.TileID, tile);
            }
            else
            {
                throw new Exception("Cannot add tile, it's already in there");
            }
        }

        public Tile GetTile(int TileID)
        {
            return dict[TileID];
        }


    }
}
