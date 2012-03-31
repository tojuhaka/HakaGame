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
    public class Sprite : DrawableComponent
    {
        protected Texture2D spriteTexture;
        protected string filename;
    
        // These two properties allows us to specify
        // origin point, around which the spire is 
        // rotated
        public Vector2 Origin { get; set; }
        public float Rotation { get; set; }

        // Resize the image
        public float Scale { get; set; }

        // Can be used to flip the image vertically etc...
        public SpriteEffects Effects { get; set; }

        public Sprite(float x, float y, float rotation, string filename)
        {
            this.Position = new Vector2(x, y);
            this.filename = filename;

            Origin = Vector2.Zero;
            Rotation = rotation;
            SourceRectangle = new Rectangle();
            Scale = 1;
            Effects = SpriteEffects.None;
        }

        protected override void Load()
        {
            spriteTexture = Parent.Engine.Content.Load<Texture2D>(filename);

            Origin = new Vector2(spriteTexture.Width / 2f,
                spriteTexture.Height / 2f);

            SourceRectangle = new Rectangle(0, 0, spriteTexture.Width,
                spriteTexture.Height);

        }

        public override void Draw()
        {
            //Parent.Engine.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None);
            Parent.Engine.SpriteBatch.Draw(spriteTexture, Position, SourceRectangle, Color.White, Rotation, Origin, Scale, Effects, 0);
            //Parent.Engine.SpriteBatch.End();

            // Set back certain render states that are changed by the SpriteBatch
            // that interfere with 3D rendering
            resetRenderStates();
        }

        public override void Update()
        {
            
            base.Update();
        }

        // Move the sprite to the given direction
        // Parameters: Speed, degrees (direction)
        public void Move(GameTime gameTime, Vector2 speed, int degrees)
        {
            degrees = degrees - 90;
            Vector2 theDirection = AngleToVector(MathHelper.ToRadians(degrees));
            this.Position += theDirection * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Move(GameTime gameTime, Vector2 speed, Vector2 destination)
        {
            float angle = VectorToAngle(destination);
            MathHelper.ToDegrees(angle);
            Move(gameTime, speed, (int)MathHelper.ToDegrees(angle));
        }

        public void Move(GameTime gameTime, Vector2 speed,  float xAmount, float yAmount)
        {
           // this.X += speed * xAmount * gameTime.ElapsedGameTime.Milliseconds;
           // this.Y += speed * yAmount * gameTime.ElapsedGameTime.Milliseconds;
            this.Position += new Vector2(xAmount, yAmount) * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        // Converts wanted angle to vector
        protected Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        // Converts vector to angle
        protected float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }


        protected void resetRenderStates()
        {
            Parent.Engine.GraphicsDevice.RenderState.DepthBufferEnable = true;
            Parent.Engine.GraphicsDevice.RenderState.AlphaBlendEnable = false;
            Parent.Engine.GraphicsDevice.RenderState.AlphaTestEnable = false;
        }
    }

}
