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
    public abstract class DrawableComponent : Component
    {
        protected SpriteBatch spriteBatch;
        protected Vector2 position = Vector2.Zero;
        protected int width;
        protected int height;

        public Rectangle SourceRectangle { get; set; }
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }

        private bool visible = true;

        public bool Visible
        {
            set { visible = value; }
            get {return visible; }
        }

        public virtual float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public virtual float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public virtual Vector2 Position 
        {
           get { return position; }
           set { position = value; } 
        }

        public DrawableComponent()
        {
            visible = true;
        }

        // Sets the order where we want to draw the object.
        // Logically higher numbers will be drawn later
        int drawOrder = 0;

        public int DrawOrder
        {
            get { return drawOrder; }
            set
            {
                this.drawOrder = value;

                if (Parent != null)
                    Parent.PutComponentInOrder(this);
            }
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

        // TODO: does nothing
        public virtual void Show()
        {
            Visible = true;
            Enabled = true;
        }

        // TODO: does nothing
        public virtual void Hide()
        {
            Visible = false;
            Enabled = false;
        }

    }
}
