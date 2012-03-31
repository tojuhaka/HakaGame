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
using System.Collections.Specialized;
using HakaGame;

namespace HakaGame
{
    // Creates a menu from the string-array given
    public class MenuComponent : DrawableComponent
    {

        #region Field and Property Region
        string[] menuItems;
        int selectedIndex;
        SpriteFont spriteFont;

        public Color NormalColor
        {
            get;
            set;
        }
        public Color HiliteColor
        {
            get;
            set;
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
        }
        #endregion


        #region Constructor Region
        public MenuComponent(SpriteFont spriteFont, string[] items)
        {
            this.spriteFont = spriteFont;
            SetMenuItems(items);
            NormalColor = Color.White;
            HiliteColor = Color.Red;
        }

        #endregion


        #region Method Region

        public void SetPostion(Vector2 position)
        {
            this.position = position;
        }

        public void SetMenuItems(string[] items)
        {
            menuItems = (string[])items.Clone();
            MeasureMenu();
        }
        private void MeasureMenu()
        {
            width = 0;
            height = 0;
            foreach (string s in menuItems)
            {
                if (width < spriteFont.MeasureString(s).X)
                    this.Width = (int)spriteFont.MeasureString(s).X; //TODO CHECK THIS
                height += spriteFont.LineSpacing;
            }
        }
        public override void Update()
        {
            // TODO this isnt very good, component calling something from engine
            // Put this in parentscreen instead

            if (InputManager.KeyReleased(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }
            if (InputManager.KeyReleased(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = 0;
            } 
        }

     
        public override void Draw()
        {
            Vector2 menuPosition = position;

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                {
                    spriteBatch.DrawString(spriteFont, menuItems[i], menuPosition, HiliteColor);
                }
                else
                {
                    spriteBatch.DrawString(spriteFont, menuItems[i], menuPosition, NormalColor);
                }
                menuPosition.Y += spriteFont.LineSpacing;

            }
        }
        #endregion
    }
}
