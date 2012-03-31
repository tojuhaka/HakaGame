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
    public class BackgroundComponent : DrawableComponent
    {
        Texture2D backgroundTexture;
        private string filename;

        public BackgroundComponent(string filename)
        {
            this.filename = filename;
        }

        protected override void Load()
        {
            backgroundTexture = Parent.Engine.Content.Load<Texture2D>(filename);

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
