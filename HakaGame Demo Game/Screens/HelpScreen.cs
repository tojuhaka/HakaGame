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
    // Manual
    public class HelpScreen : GameScreen
    {
         public HelpScreen()
        {

        }

        protected override void Load()
        {
            AddComponent(new BackgroundComponent(Engine.Content.Load<Texture2D>("content/GUI/blackbackground")));

            Label helpText = new Label(Engine.Content.Load<SpriteFont>("content/fonts/MenuFont"), Engine.SpriteBatch);
            helpText.Text = "Use Arrow-Keys to move the character \nUse escape to quit current screen \nUse Enter to accept selected choice";
            helpText.Position = new Vector2(Engine.GraphicsDevice.PresentationParameters.BackBufferWidth/4, Engine.GraphicsDevice.PresentationParameters.BackBufferHeight / 3);
            AddControl(helpText);
            base.Load();
        }

        private void HandleScreenInput()
        {

              if (InputManager.KeyReleased(Keys.Escape) )
              {
                  Engine.ChangeGameScreen(Engine.GameRef.StartScreen);
              }
        }

        public override void Update()
        {
            HandleScreenInput();
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        private void HandleHelpScreenInput()
        {
             if (InputManager.KeyReleased(Keys.Escape))
            {
                engine.PopGameScreen();
                engine.PushGameScreen(Engine.GameRef.StartScreen);

            }
        }

    }
}
