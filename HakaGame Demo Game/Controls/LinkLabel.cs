using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HakaGame
{
    public class LinkLabel : Control
    {
        #region Fields and Properties
        Color selectedColor = Color.Red;
        SpriteBatch spriteBatch;
        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }
        #endregion
        #region Constructors
        public LinkLabel(SpriteFont spriteFont, SpriteBatch spriteBatch)
        {
            SpriteFont = spriteFont;
            Enabled = true;
            Visible = true;
            TabStop = true;
            HasFocus = false;
            Color = Color.White;
            this.spriteBatch = spriteBatch;
        }
        #endregion
        #region Abstract Methods
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw()
        {
            if (hasFocus)
                spriteBatch.DrawString(SpriteFont, Text, Position, selectedColor);
            else
                spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }
        public override void HandleInput()
        {
            if (!HasFocus)
                return;
            if (InputManager.KeyReleased(Keys.Enter))
                base.OnSelected(null);
        }
        #endregion
    }
}
