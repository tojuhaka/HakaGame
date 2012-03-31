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
    // The game starts with this screen
    // uses menu component to show
    // the options
    public class StartScreen : GameScreen
    {
        MenuComponent menu;

        public StartScreen()
        {

        }

        protected override void Load()
        {
            string[] items = { "New Game", "Load Game", "Help", "Quit" };
            menu = new MenuComponent(Engine.Content.Load<SpriteFont>("Content/Fonts/MenuFont"), items);
            this.AddComponent(menu);

             //    this.AddComponent(new InputManager());

            // reposition menu
            menu.Position = new Vector2((Engine.GraphicsDevice.
               PresentationParameters.BackBufferWidth - menu.Width) / 2, (Engine.GraphicsDevice.
               PresentationParameters.BackBufferHeight - menu.Height)/2);

            BackgroundComponent bg = new BackgroundComponent(Engine.Content.Load<Texture2D>("Content/GUI/blackbackground"));
            AddComponent(bg);
        }

        public int SelectedIndex
        {
            get { return menu.SelectedIndex; }
        }

        #region Handle Inputs
        private void HandleStartScreenInput()
        {
            if (InputManager.KeyReleased(Keys.Enter))
            {
                switch (this.SelectedIndex)
                {
                    case 0:
                        engine.ChangeGameScreen(Engine.GameRef.CGScreen);
                        break;
                    case 2:
                        engine.ChangeGameScreen(Engine.GameRef.HelpScreen);
                        break;
                    case 3:
                        engine.GameRef.Exit();
                        break;
                }
            }
        }
        #endregion

        public override void Update()
        {
            HandleStartScreenInput();
            base.Update();
        }
    }
}
