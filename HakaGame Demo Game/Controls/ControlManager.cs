using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace HakaGame
{
    public class ControlManager : List<Control>
    {
        #region Fields and Properties
        int selectedControl = 0; // Keeps track of the selected control
        #endregion

        // Compulsory constructors needed for List
        #region Constructors
        public ControlManager() : base() { }
        public ControlManager(int capacity) : base(capacity) { }
        public ControlManager(IEnumerable<Control> collection) : base(collection) { }
        #endregion
        #region Methods

        // TODO Check playerIndex
        public void Update(GameTime gameTime)
        {
            if (Count == 0)
                return;
            foreach (Control c in this)
            {
                if (c.Enabled)
                    c.Update(gameTime);
                if (c.HasFocus)
                    c.HandleInput();
            }
            if (InputManager.KeyPressed(Keys.Up))
                PreviousControl();
            if (InputManager.KeyPressed(Keys.Down))
                NextControl();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control c in this)
            {
                if (c.Visible)
                    c.Draw();
            }
        }
        public void NextControl()
        {
            if (Count == 0)
                return;
            int currentControl = selectedControl;
            this[selectedControl].HasFocus = false;
            do
            {
                selectedControl++;
                if (selectedControl == Count)
                    selectedControl = 0;
                if (this[selectedControl].TabStop && this[selectedControl].Enabled)
                    break;
            } while (currentControl != selectedControl);
            this[selectedControl].HasFocus = true;
        }
        public void PreviousControl()
        {
            if (Count == 0)
                return;
            int currentControl = selectedControl;
            this[selectedControl].HasFocus = false;
            do
            {
                selectedControl--;
                if (selectedControl < 0)
                    selectedControl = Count - 1;
                if (this[selectedControl].TabStop && this[selectedControl].Enabled)
                    break;
            } while (currentControl != selectedControl);
            this[selectedControl].HasFocus = true;
        }
        #endregion
    }
}