using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using HakaGame;

namespace HakaGame
{
    // Holds the background of the screen
    public class BackgroundComponent : DrawableComponent
    {
        Texture2D backgroundTexture;

        public BackgroundComponent(Texture2D bgTexture)
        {
            this.backgroundTexture = bgTexture;
        }

        protected override void Load()
        {

            this.SourceRectangle = new Rectangle(0, 0, 
                Parent.Engine.GraphicsDevice.PresentationParameters.BackBufferWidth, 
                Parent.Engine.GraphicsDevice.PresentationParameters.BackBufferHeight);
           
            base.Load();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            spriteBatch.Draw(backgroundTexture, SourceRectangle, Color.White);
            base.Draw();
        }
    }
}
