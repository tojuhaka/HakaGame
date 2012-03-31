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
    public class HelpScreen : GameScreen
    {
         public HelpScreen()
        {

        }

        protected override void Load()
        {
            AddComponent(new BackgroundComponent("content/helpbackground"));
        }
    }
}
