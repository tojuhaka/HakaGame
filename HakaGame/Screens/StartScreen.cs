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
    public class StartScreen : GameScreen
    {
        MenuComponent menu;

        public StartScreen()
        {

        }

        protected override void Load()
        {
            string[] items = { "New Game", "Load Game", "Help", "Quit" };
            menu = new MenuComponent(Engine.Content.Load<SpriteFont>("Content/Georgia"), items);
            this.AddComponent(menu);

        //    this.AddComponent(new InputManager());

            // reposition menu
            menu.Position = new Vector2((Engine.GraphicsDevice.
               PresentationParameters.BackBufferWidth - menu.Width) / 2, (Engine.GraphicsDevice.
               PresentationParameters.BackBufferHeight - menu.Height)/2);
        }

        public int SelectedIndex
        {
            get { return menu.SelectedIndex; }
        }
    }
}
