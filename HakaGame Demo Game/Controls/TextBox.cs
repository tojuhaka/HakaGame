using System;
using System.Collections.Generic;
using System.Linq;
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
    // Simple textbox with 16 chars long
    // Used for name for example
    public class TextBox : Control
    {
        Texture2D textboxTexture;
        Texture2D cursor;
        IEContentManager content;
        SpriteBatch spriteBatch;


        public override string Text
        {
            get { return text; }
            set { text = value; }
        }

        Keys[] keysToCheck = new Keys[] {
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
            Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
            Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
            Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
            Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y,
            Keys.Z, Keys.Back, Keys.Space };

        Vector2 cursorPosition;
        Vector2 textPosition;

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                textPosition = new Vector2(base.Position.X + 5, base.Position.Y + 5);
                cursorPosition = new Vector2(base.Position.X + 5, base.Position.Y + 5);
            }
        }

        public int Height
        {
            get { return textboxTexture.Height; }
        }
        public int Width
        {
            get { return textboxTexture.Width; }
        }

        // For cursor blinking
        TimeSpan blinkTime;

        bool blink;

        public TextBox(SpriteFont spriteFont, IEContentManager Content, SpriteBatch spriteBatch)
        {
            this.spriteFont = spriteFont;
            this.content = Content;

            textboxTexture = Content.Load<Texture2D>("Content/GUI/textbox");
            cursor = Content.Load<Texture2D>("Content/GUI/cursor");
            blink = false;
            text = "";
            this.Visible = true;
            this.Enabled = true;
            this.TabStop = true;
            this.HasFocus = false;
            this.spriteBatch = spriteBatch;
        }

        public override void Update(GameTime gameTime)
        {
            if (hasFocus)
            {
                blinkTime += gameTime.ElapsedGameTime;
                if (blinkTime > TimeSpan.FromMilliseconds(500))
                {
                    blink = !blink;
                    blinkTime -= TimeSpan.FromMilliseconds(500);
                }
            }
            
        }

        // SWITCH-CASE FTW!
        private void AddKeyToText(Keys key)
        {
            string newChar = "";
            if (text.Length >= 16 && key != Keys.Back)
                return;
            switch (key)
            {
                case Keys.A:
                    newChar += "a";
                    break;
                case Keys.B:
                    newChar += "b";
                    break;
                case Keys.C:
                    newChar += "c";
                    break;
                case Keys.D:
                    newChar += "d";
                    break;
                case Keys.E:
                    newChar += "e";
                    break;
                case Keys.F:
                    newChar += "f";
                    break;
                case Keys.G:
                    newChar += "g";
                    break;
                case Keys.H:
                    newChar += "h";
                    break;
                case Keys.I:
                    newChar += "i";
                    break;
                case Keys.J:
                    newChar += "j";
                    break;
                case Keys.K:
                    newChar += "k";
                    break;
                case Keys.L:
                    newChar += "l";
                    break;
                case Keys.M:
                    newChar += "m";
                    break;
                case Keys.N:
                    newChar += "n";
                    break;
                case Keys.O:
                    newChar += "o";
                    break;
                case Keys.P:
                    newChar += "p";
                    break;
                case Keys.Q:
                    newChar += "q";
                    break;
                case Keys.R:
                    newChar += "r";
                    break;
                case Keys.S:
                    newChar += "s";
                    break;
                case Keys.T:
                    newChar += "t";
                    break;
                case Keys.U:
                    newChar += "u";
                    break;
                case Keys.V:
                    newChar += "v";
                    break;
                case Keys.W:
                    newChar += "w";
                    break;
                case Keys.X:
                    newChar += "x";
                    break;
                case Keys.Y:
                    newChar += "y";
                    break;
                case Keys.Z:
                    newChar += "z";
                    break;
                case Keys.Space:
                    newChar += " ";
                    break;
                case Keys.Back:
                    if (text.Length != 0)
                        text = text.Remove(text.Length - 1);
                    return;
            }

            if (InputManager.KeyDown(Keys.RightShift) ||
            InputManager.KeyDown(Keys.LeftShift))
            {
                newChar = newChar.ToUpper();
            }
            text += newChar;
        }

        public override void Draw()
        {
            spriteBatch.Draw(textboxTexture, Position, Color.White);
            if (!blink && hasFocus)
                spriteBatch.Draw(cursor, cursorPosition, Color.White);
            spriteBatch.DrawString(spriteFont, text, textPosition, Color.Black); 
        }

        public override void HandleInput()
        {
            foreach (Keys key in keysToCheck)
            {
                if (InputManager.KeyReleased(key))
                {
                    AddKeyToText(key);
                    break;
                }
            }

            Vector2 textSize = spriteFont.MeasureString(text);
            cursorPosition.X = textPosition.X + textSize.X;
        }

    }
}