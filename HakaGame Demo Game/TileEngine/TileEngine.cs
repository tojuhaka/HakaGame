using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Collections.Specialized;
using HakaGame;

namespace HakaGame
{
    // This component holds the width and the height of the tiles
    // on the screen. It may be developed more down the road as 
    // the game progresses
    public class TileEngine 
    {

        #region Fields and Properties
        static int tileWidth;
        static int tileHeight;

        // The are the map is being drawn to
        static int viewportWidth;
        static int viewportHeight;
        public static int TileWidth
        {
            get { return tileWidth; }
        }
        public static int TileHeight
        {
            get { return tileHeight; }
        }

        public static int ViewportWidth
        {
            get { return viewportWidth; }
        }
        public static int ViewportHeight
        {
            get { return viewportHeight; }
        }

        #endregion
        #region Constructors
        public TileEngine(int tileWidth, int tileHeight, MainGame game)
        {
            TileEngine.tileWidth = tileWidth;
            TileEngine.tileHeight = tileHeight;

                viewportWidth = game.Window.ClientBounds.Width;
                viewportHeight = game.Window.ClientBounds.Height;
        }

        #endregion
        #region Methods
        public static Point VectorToCell(Vector2 position)
        {
            return new Point((int)position.X / tileWidth, (int)position.Y / tileHeight);
        }

        #endregion
    }
}
