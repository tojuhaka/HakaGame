using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HakaGame
{
    // Controls are common "objects" on the screen
    // like texts, labels, buttons etc. Controls are like components
    // but more "smaller" and "lighter".
    public abstract class Control
    {
        #region Fields
        protected string name;
        protected string text;
        protected Vector2 size;
        protected Vector2 position;
        protected object value;
        protected bool hasFocus;
        protected bool enabled;
        protected bool visible;
        protected bool tabStop;
        protected SpriteFont spriteFont;
        protected Color color;

        public event EventHandler Selected;

        // GameScreen where the component has been added.
        // Make sure we wont cause infinte loop or anything
        // else screwing the parent or component

        //TODO: Check if we really need this
        GameScreen parent;

        public GameScreen Parent
        {
            get { return parent; }
            set
            {
                if (parent == value)
                    return;

                if (parent != null)
                    parent.RemoveControl(this);

                parent = value;
                if (value != null)
                    parent.AddControl(this);
            }
        }


        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
        public virtual string Text
        {
            get { return text; }
            set { text = value; }
        }
        public virtual Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }
        public virtual Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public virtual object Value
        {
            get { return value; }
            set { this.value = value; }
        }
        public virtual bool HasFocus
        {
            get { return hasFocus; }
            set { hasFocus = value; }
        }
        public virtual bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
        public virtual bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
        public virtual bool TabStop
        {
            get { return tabStop; }
            set { tabStop = value; }
        }
        public virtual SpriteFont SpriteFont
        {
            get { return spriteFont; }
            set { spriteFont = value; }
        }
        public virtual Color Color
        {
            get { return color; }
            set { color = value; }
        }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        protected virtual void OnSelected(EventArgs e)
        {
            if (Selected != null)
            {
                Selected(this, e);
            }
        }
        #endregion

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw()
        {
        }

            
        public virtual void HandleInput()
        {
        }
    }
}
