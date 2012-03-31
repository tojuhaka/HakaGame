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
    // Provides us Sprite with some basic animation
    // Nagivages through the sheet jumping 
    // parameters: 
    // float x, y: position on the screen
    // filename: path to the pic we want to load
    // FrameOffsetX, FrameOffsetY: if we need to optimize the width and height
    // FrameWidth, FrameHeight: Width and Height of a single pic
    // FrameCount: How many frames
    public class AnimatedSprite : Sprite
    {
        float fFrameRate = 0.02f;
        float fElapsed = 0.0f;

        int iFrameOffsetX = 0;
        int iFrameOffsetY = 0;
        int iFrameWidth = 32;
        int iFrameHeight = 32;

        int iFrameCount = 1;
        int iCurrentFrame = 0;

        bool bAnimating = true;

        public AnimatedSprite(float x,
              float y,
              float rotation,
            string filename,
              int FrameOffsetX,
              int FrameOffsetY,
              int FrameWidth,
              int FrameHeight,
              int FrameCount) : base(x, y, rotation, filename)
            
        {
            // Of course FrameCount cant be 0, just throw an exception
            if (FrameCount < 1) throw new Exception("Frame count cant be 0");

            iFrameOffsetX = FrameOffsetX;
            iFrameOffsetY = FrameOffsetY;
            iFrameWidth = FrameWidth;
            iFrameHeight = FrameHeight;
            iFrameCount = FrameCount;

            this.SourceRectangle = new Rectangle(iFrameOffsetX + (iFrameWidth * iCurrentFrame), iFrameOffsetY,
            iFrameWidth,iFrameHeight);
        }

        public Rectangle GetSourceRect()
        {
            this.SourceRectangle = new Rectangle(iFrameOffsetX + (iFrameWidth * iCurrentFrame), iFrameOffsetY,
             iFrameWidth, iFrameHeight);

            return this.SourceRectangle;
        }

        public int Frame
        {
            get { return iCurrentFrame; }
            set { iCurrentFrame = (int)MathHelper.Clamp(value, 0, iFrameCount); }
        }

        public float FrameLength
        {
            get { return fFrameRate; }
            set { fFrameRate = (float)Math.Max(fFrameRate, 0f); }
        }

        public bool IsAnimating
        {
            get { return bAnimating; }
            set { bAnimating = value; }
        }

        protected override void Load()
        {
            spriteTexture = Parent.Engine.Content.Load<Texture2D>(filename);
            base.Load();
        }

        // Set the next line of the sheet
        // OffsetY is most of the time pic height
        public void NextSheetLine(int iFrameOffsetY)
        {
            this.iFrameOffsetY = iFrameOffsetY;
        }

        public void Draw(SpriteBatch spriteBatch, int XOffset, int YOffset)
        {
            spriteBatch.Draw(
                spriteTexture,
                new Rectangle(
                  (int)this.X + XOffset,
                  (int)this.Y + YOffset,
                  iFrameWidth,
                  iFrameHeight),
                GetSourceRect(),
                Color.White);
        }

        public override void Draw()
        {
            this.Draw(Parent.Engine.SpriteBatch, 0, 0);
        }


        public override void Update()
        {
            if (bAnimating)
            {
                // Accumulate elapsed time... //TODO BUG
                fElapsed += (float)this.Parent.Engine.GameTime.ElapsedGameTime.TotalSeconds;

                // Until it passes our frame length
                if (fElapsed > fFrameRate)
                {
                    // Increment the current frame, wrapping back to 0 at iFrameCount
                    iCurrentFrame = (iCurrentFrame + 1) % iFrameCount;

                    // Reset the elapsed frame time.
                    fElapsed = 0.0f;
                }
            }
            base.Update();
        }
    }
}
