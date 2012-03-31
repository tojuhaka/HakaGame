using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HakaGame
{
    // we can use this class as a normal sprite
    // or animatedsprite. Sprite is a single
    // drawable object on the screen
    public class AnimatedSprite : DrawableComponent
    {
        #region Fields and Properties
        Dictionary<AnimationKey, Animation> animations;
        AnimationKey currentAnimation;
        bool isAnimating;
        Texture2D texture;
        Vector2 velocity;
        float speed = 4.0f; // for normalizing the velocity

        public AnimationKey CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }
        public bool IsAnimating
        {
            get { return isAnimating; }
            set { isAnimating = value; }
        }
        public int fWidth
        {
            get { return animations[currentAnimation].FrameWidth; }
        }
        public int fHeight
        {
            get { return animations[currentAnimation].FrameHeight; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(speed, 1.0f, 16.0f); }
        }

        // Velocity should be normalized
        // so the motion of the sprite is
        // the same in all directions
        public Vector2 Velocity
        {
            get { return velocity; }
            set
            {
                velocity = value;
                if (velocity != Vector2.Zero)
                    velocity.Normalize();
            }
        }

        public Vector2 Origin
        {
            get { return position / 2; }
        }

        #endregion

        #region Constructors
        public AnimatedSprite(Texture2D texture, Dictionary<AnimationKey, Animation> animations) : base()
        {
            this.texture = texture;
            this.animations = animations;
            

        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {

            if (isAnimating)
            {
                animations[currentAnimation].Update(gameTime);
                this.Width = animations[currentAnimation].FrameWidth;
                this.Height = animations[currentAnimation].FrameHeight;
            }

        }
        public override void Draw()
        {
            spriteBatch.Draw(
            texture,
            position - Session.Camera.Position,
            animations[currentAnimation].CurrentFrameRect,
            Color.White);
        }
        public void LockToViewport()
        {
            position.X = MathHelper.Clamp(position.X, 0, TileMap.WidthInPixels - Width);
            position.Y = MathHelper.Clamp(position.Y, 0, TileMap.HeightInPixels - Height);
        }
        #endregion

    }
}
