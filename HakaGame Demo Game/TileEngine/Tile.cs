using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HakaGame
{
    //TODO Refactor tile
    // Tells us where a single exists
    public class Tile
    {
        #region Fields and Properties Region
        int tileIndex; // Index in set
        int tileset; //which tileset
        int tileID;
        CollisionType collisionType;
        Rectangle tilesetPart;
        bool isPartOfTileset = false;
        bool isNullTile = false; //Used for collision detection tiles
        Texture2D texture;
        string filename;
        bool textureLoaded = false;


        public bool TextureLoaded
        {
            get { return textureLoaded; }
            set { textureLoaded = value; }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public CollisionType CollisionType
        {
            get { return collisionType; }
            set { collisionType = value; }
        }

        public Rectangle TilesetPart
        {
            get { return tilesetPart; }
            set { tilesetPart = value; }
        }

        public bool IsPartOfTileset
        {
            get { return isPartOfTileset; }
            set { isPartOfTileset = value; }
        }

        public bool IsNullTile
        {
            get { return isNullTile; }
            set { isNullTile = value; }
        }


        public int TileIndex
        {
            get { return tileIndex; }
            private set { tileIndex = value; }
        }

        public Texture2D Texture
        {
            get {
                    return texture;
            }
            set { texture = value; }
        }

        public int TileID
        {
            get { return tileID; }
            set { tileID = value; }
        }
        public int Tileset
        {
            get { return tileset; }
            private set { tileset = value; }
        }
        #endregion
        #region Constructor Region
        public Tile(CollisionType type)
        {
            isNullTile = true;
            collisionType = type;
        }

        public Tile(int tileIndex, int tileSet)
        {
            TileIndex = tileIndex;
            Tileset = tileSet;
        }

        public Tile(Texture2D tileTexture, int tileID)
        {
            this.texture = tileTexture;
            this.tileID = tileID;
            textureLoaded = true;
        }

        // when using content pipeline, we dont have
        // content ref
        public Tile(string filename, int tileID)
        {
            this.filename = filename;
            this.tileID = tileID;
        }

        public Tile(Texture2D tileTexture, int tileID, Rectangle tilesetPart)
        {
            this.tilesetPart = tilesetPart;
            this.texture = tileTexture;
            this.tileID = tileID;
            textureLoaded = true;
        }

        public Tile(string filename, int tileID, Rectangle tilesetPart)
        {
            this.tilesetPart = tilesetPart;
            this.filename = filename;
            this.tileID = tileID;
            textureLoaded = true;
        }
        #endregion

        #region Methods

        // If texture is null, we should load the texture with file name given
        // if filename is null, then there is something wrong TODO: fix that bug
        public void Load(IEContentManager content)
        {
            texture = content.Load<Texture2D>(filename);
        }
        #endregion
    }
}