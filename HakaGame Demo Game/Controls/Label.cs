using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HakaGame
{
    public class Label : Control
    {
        SpriteBatch spriteBatch;

        #region Contructors
        public Label(SpriteFont spriteFont, SpriteBatch spriteBatch)
        {
            SpriteFont = spriteFont;
            TabStop = false;
            Enabled = true;
            Visible = true;
            Color = Color.White;
            this.spriteBatch = spriteBatch;
        }
        #endregion

        #region Override Methods
        public override void HandleInput()
        {
        }

        public override void Draw()
        {
            spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }

        public override void Update(GameTime gameTime)
        {
        }
        #endregion

    }
}
