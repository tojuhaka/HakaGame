using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using HakaGame;
using System.IO;
using System.Xml.Serialization;

namespace HakaGame
{
    // Session class holds the session of the game.
    // it has player status, camera, tile engine
    // and almost everything the game itself needs
    // It's a singleton class so there is only one instance of it
    public sealed class Session
    {
        #region Fields and Properties
        private static readonly Session instance = new Session();
        public static StorageDevice StorageDevice = null;
        public static SaveData GameData = new SaveData();

        public static Session Instance
        {
            get { return instance; }
        }

        public static MainGame GameRef;
        public static Camera Camera;
        public static TileEngine TileEngine;
        public static TileMap CurrentMap;

        #endregion
        #region Constructors
        private Session() { }
        #endregion
        #region Methods
        public void Initialize(MainGame game)
        {
            GameRef = game;
            Camera = new Camera();
            TileEngine = new TileEngine(32,32,game);
        }

        public static void DrawMap(SpriteBatch spriteBatch)
        {
            CurrentMap.Draw(spriteBatch);
        }

        public static void SaveGame(string gamename)
        {
            StorageContainer container = StorageDevice.OpenContainer("HakaGame");
            string filename = Path.Combine(container.Path, gamename);
            FileStream stream = new FileStream(filename, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            serializer.Serialize(stream, GameData);
            stream.Close();
            container.Dispose();
        }
        #endregion
    }
}