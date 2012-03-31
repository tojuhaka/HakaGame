using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HakaGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace HakaGame
{
    public class ActionScreen : GameScreen
    {
        #region Fields
        #endregion

        TileEngine tileEngine;
        #region Constructors
        public ActionScreen()
        {

        }
        #endregion

        #region Methods

        public override void Update()
        {
            if (Engine.Input.KeyReleased(Keys.Escape))
            {
                Engine.QuitScreen(this);
            }
            base.Update();
        }
        protected override void Load()
        {
            tileEngine = new TileEngine("Content/tileTexturetest");
            AddComponent(tileEngine);
        }
        #endregion
    }
}
