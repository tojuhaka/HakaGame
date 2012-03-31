using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HakaGame
{
    // Makes the core component drawable. Also
    // got position, visible and enabled properties
    public class DrawableComponent : Component
    {
        protected SpriteBatch spriteBatch;
        protected Vector2 position = Vector2.Zero;
        protected int width;
        protected int height;

        public Rectangle SourceRectangle { get; set; }
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }

        private bool enabled;

        public bool Enabled
        {
            set { enabled= value; }
            get { return enabled; }
        }

        private bool visible;

        public bool Visible
        {
            set { visible = value; }
            get {return visible; }
        }

        public float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public Vector2 Position 
        {
           get { return position; }
           set { position = value; } 
        }

        public DrawableComponent()
        {
            visible = true;
        }

        protected override void Load()
        {
            spriteBatch = Parent.Engine.SpriteBatch;
            base.Load();
        }

        public virtual void Draw()
        {
            //Draw here
        }

        public virtual void Show()
        {
            Visible = true;
            Enabled = true;
        }

        public virtual void Hide()
        {
            Visible = false;
            Enabled = false;
        }

    }
}
