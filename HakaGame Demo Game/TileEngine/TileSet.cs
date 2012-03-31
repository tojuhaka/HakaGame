using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HakaGame;

namespace HakaGame
{
    // Tileset is a set of tiles. It contains many tiles
    // in one picture. The key is to use rectangles to 
    // to keep track of single tiles. 
    public class TileSet
    {
        #region Fields and Properties
        Texture2D image;
        int tileWidthInPixels; // Single Tile
        int tileHeightInPixels;
        int tilesWide; // how many tiles Wide
        int tilesHigh; // how many tiles High
        Rectangle[] sourceRectangles;
      

        public Texture2D Texture
        {
            get { return image; }
            private set { image = value; }
        }
        public int TileWidth
        {
            get { return tileWidthInPixels; }
            private set { tileWidthInPixels = value; }
        }
        public int TileHeight
        {
            get { return tileHeightInPixels; }
            private set { tileHeightInPixels = value; }
        }
        public int TilesWide
        {
            get { return tilesWide; }
            private set { tilesWide = value; }
        }
        public int TilesHigh
        {
            get { return tilesHigh; }
            private set { tilesHigh = value; }
        }
        public Rectangle[] SourceRectangles
        {
            get { return (Rectangle[])sourceRectangles.Clone(); }
        }
        #endregion
        #region Constructor

        // Needs engine and filename where to load
        public TileSet(Engine engine,string filename, int tilesWide, int tilesHigh, int tileWidth, int tileHeight)
        {
            // Throw exception 
            if (filename == null || engine == null)
            {
                throw new Exception("Filename or engine can't be null in tileset");
            }

            Texture = engine.Content.Load<Texture2D>(filename);
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;
            int tiles = tilesWide * tilesHigh;
            sourceRectangles = new Rectangle[tiles];
            int tile = 0;

            // First x-axis, then go y-axis and again x-axis
            for (int y = 0; y < tilesHigh; y++)
                for (int x = 0; x < tilesWide; x++)
                {
                    sourceRectangles[tile] = new Rectangle(
                    x * tileWidth,
                    y * tileHeight,
                    tileWidth,
                    tileHeight);
                    tile++;
                }
        }

       
        #endregion
        #region Methods
        #endregion
    }
}