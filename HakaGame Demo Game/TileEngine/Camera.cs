using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HakaGame
{
    public class Camera
    {
        #region Fields

        // Ye I know local variables suck, but this time it's needes
        // since we call the camera from static session
        public Vector2 Position;


        float speed = 8f;
        public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(speed, 1.0f, 16.0f); }
        }
        #endregion

        #region Constructors
        public Camera()
        {
            Position = Vector2.Zero;
        }

        public Camera(Vector2 position)
        {
            Position = position;
        }
        #endregion

        #region Methods
        public void LockCamera()
        {
            Position.X = MathHelper.Clamp(Position.X, 0,
            TileMap.WidthInPixels - TileEngine.ViewportWidth);

            Position.Y = MathHelper.Clamp(Position.Y,
            0, TileMap.HeightInPixels - TileEngine.ViewportHeight);
        }

        public void LockToSprite(AnimatedSprite sprite)
        {
            Position.X = sprite.Position.X + sprite.Width / 2
            - (TileEngine.ViewportWidth / 2);
            Position.Y = sprite.Position.Y + sprite.Height / 2
            - (TileEngine.ViewportHeight / 2);
        }
        #endregion
    }
}
