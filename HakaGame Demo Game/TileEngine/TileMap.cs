using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HakaGame;

namespace HakaGame
{
    public class TileMap
    {
        #region Fields 
        List<TileSet> tileSets;
        List<MapLayer> mapLayers;

        // Map should be same size as layers
        static int mapWidth;
        static int mapHeight;
        Session Session = Session.Instance;

        CollisionLayer collisionLayer;

        public static int WidthInPixels
        {
            get { return mapWidth * TileEngine.TileWidth; }
        }
        public static int HeightInPixels
        {
            get { return mapHeight * TileEngine.TileHeight; }
        }
        #endregion

        #region Constructors
        public TileMap(List<TileSet> tileSets, List<MapLayer> layers)
        {
            mapWidth = layers[0].Width; // Every layer's size is equal
            mapHeight = layers[0].Height;

            this.tileSets = tileSets;
            this.mapLayers = layers;
        }

        public TileMap(TileSet tileset, MapLayer layer)
        {
            mapWidth = layer.Width;
            mapHeight = layer.Height;

            tileSets = new List<TileSet>();
            tileSets.Add(tileset);

            mapLayers = new List<MapLayer>();
            mapLayers.Add(layer);
        }

        public TileMap(List<MapLayer> layers)
        {
            mapWidth = layers[0].Width; // Every layer's size is equal
            mapHeight = layers[0].Height;

            collisionLayer = new CollisionLayer(mapWidth, mapHeight);
            foreach (MapLayer mapLayer in layers)
            {
                ProcessCollisionLayer(mapLayer);
            }

           // this.tileSets = tileSets;
            this.mapLayers = layers;
        }
        #endregion

        #region Methods

        // Paint the layers
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destination = new Rectangle(0, 0, TileEngine.TileWidth, TileEngine.TileHeight);
            Tile tile;
            foreach (MapLayer layer in mapLayers)
            {
                for (int y = 0; y < layer.Height; y++)
                {
                    destination.Y = (int)(y * TileEngine.TileHeight
                        -Session.Camera.Position.Y);

                    for (int x = 0; x < layer.Width; x++)
                    {
                        tile = layer.GetTile(x, y);
                        
                        if (tile.TileID != 0)
                        {
                            //Draw individual tile
                            if (!tile.IsPartOfTileset && !tile.IsNullTile)
                            {
                                destination.X = (int)(x * TileEngine.TileWidth - Session.Camera.Position.X);
                                spriteBatch.Draw(
                                tile.Texture,
                                destination,
                                Color.White);
                            }
                            //Draw tile which is a part of multiple tiles (tileset)
                            else
                            {
                                if (!tile.IsNullTile)
                                {
                                    destination.X = (int)(x * TileEngine.TileWidth - Session.Camera.Position.X);
                                    spriteBatch.Draw(
                                    tile.Texture,
                                    destination, tile.TilesetPart,
                                    Color.White);
                                }
                            }
                        }
                    }
                }
            }
        }

        // Add new layer, make sure it's the same size as
        // the map
        internal void AddLayer(MapLayer layer)
        {
            if (layer.Width != mapWidth || layer.Height != mapHeight)
                throw new Exception("Error in layer size.");
            mapLayers.Add(layer);
        }

        
        private void ProcessCollisionLayer(MapLayer layer)
        {
            for (int y = 0; y < mapHeight; y++)
                for (int x = 0; x < mapWidth; x++)
                {
                    Tile tile = layer.GetTile(x, y);
                    if (tile.IsNullTile)
                    {
                        if (tile.CollisionType == CollisionType.Unpassable) 
                        collisionLayer.SetTile(x, y, CollisionType.Unpassable);
                    }
                }     
        }

        public CollisionType GetCollisionValue(int x, int y)
        {
            return collisionLayer.GetTile(x, y);
        }

        // COLLISION DETECTION TODO: Optimize
        public bool CheckUpAndLeft(Rectangle nextRectangle)
        {
            Point tile1 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X + nextRectangle.Width,
            nextRectangle.Y + nextRectangle.Height));
            bool doesCollide = false;
            if (tile1.X < 0 || tile1.Y < 0)
                return !doesCollide;
            for (int y = tile1.Y; y <= tile2.Y; y++)
                for (int x = tile1.X; x <= tile2.X; x++)
                    if (GetCollisionValue(x, y) != CollisionType.Passable)
                        doesCollide = true;
            return doesCollide;
        }

        public bool CheckUp(Rectangle nextRectangle)
        {
            Point tile1 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X + nextRectangle.Width - 1,
            nextRectangle.Y + nextRectangle.Height));
            bool doesCollide = false;
            if (tile1.Y < 0)
                return !doesCollide;
            int y = tile1.Y;
            for (int x = tile1.X; x <= tile2.X; x++)
                if (GetCollisionValue(x, y) != CollisionType.Passable)
                    doesCollide = true;
            return doesCollide;
        }

        public bool CheckUpAndRight(Rectangle nextRectangle)
        {
            Point tile1 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X + nextRectangle.Width + 1,
            nextRectangle.Y + nextRectangle.Height));
            bool doesCollide = false;
            if (tile2.X >= mapWidth || tile1.Y < 0)
                return !doesCollide;
            for (int y = tile1.Y; y <= tile2.Y; y++)
                for (int x = tile1.X; x <= tile2.X; x++)
                    if (GetCollisionValue(x, y) != CollisionType.Passable)
                        doesCollide = true;
            return doesCollide;
        }

        public bool CheckLeft(Rectangle nextRectangle)
        {
            Point tile1 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X + nextRectangle.Width,
            nextRectangle.Y + nextRectangle.Height - 1));
            bool doesCollide = false;
            if (tile1.X < 0)
                return !doesCollide;
            int x = tile1.X;
            for (int y = tile1.Y; y <= tile2.Y; y++)
            {
                if (GetCollisionValue(x, y) != CollisionType.Passable)
                    doesCollide = true;
            }
            return doesCollide;
        }

        public bool CheckRight(Rectangle nextRectangle)
        {
            Point tile1 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X + nextRectangle.Width,
            nextRectangle.Y + nextRectangle.Height - 1));
            bool doesCollide = false;
            if (tile2.X >= mapWidth)
                return !doesCollide;
            int x = tile2.X;
            for (int y = tile1.Y; y <= tile2.Y; y++)
            {
                if (GetCollisionValue(x, y) != CollisionType.Passable)
                    doesCollide = true;
            }
            return doesCollide;
        }

        public bool CheckDownAndLeft(Rectangle nextRectangle)
        {
            Point tile1 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X + nextRectangle.Width,
            nextRectangle.Y + nextRectangle.Height));
            bool doesCollide = false;
            if (tile1.X < 0 || tile2.Y >= mapHeight)
                return !doesCollide;
            for (int y = tile1.Y; y <= tile2.Y; y++)
                for (int x = tile1.X; x <= tile2.X; x++)
                    if (GetCollisionValue(x, y) != CollisionType.Passable)
                        doesCollide = true;
            return doesCollide;
        }

        public bool CheckDown(Rectangle nextRectangle)
        {
            Point tile1 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X + nextRectangle.Width - 1,
            nextRectangle.Y + nextRectangle.Height));
            bool doesCollide = false;
            if (tile2.Y >= mapHeight)
                return !doesCollide;
            int y = tile2.Y;
            for (int x = tile1.X; x <= tile2.X; x++)
                if (GetCollisionValue(x, y) != CollisionType.Passable)
                    doesCollide = true;
            return doesCollide;
        }

        public bool CheckDownAndRight(Rectangle nextRectangle)
        {
            Point tile1 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X, nextRectangle.Y));
            Point tile2 = TileEngine.VectorToCell(
            new Vector2(nextRectangle.X + nextRectangle.Width,
            nextRectangle.Y + nextRectangle.Height));
            bool doesCollide = false;
            if (tile2.X >= mapWidth || tile2.Y >= mapHeight)
                return !doesCollide;
            for (int y = tile1.Y; y <= tile2.Y; y++)
                for (int x = tile1.X; x <= tile2.X; x++)
                    if (GetCollisionValue(x, y) != CollisionType.Passable)
                        doesCollide = true;
            return doesCollide;
        }
        //ENDS
        #endregion
    }
}
