using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HakaGame;
using RPGLibrary.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace RPGLibrary.Screens
{
    // TODO: DELETE THIS CLASS IF NEEDED
    public class CreateCharacterScreen : GameScreen
    {
        MenuComponent menu;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        int difficultyLevel = 1;
        int name;
        bool gender;

        string[] menuItems = {
            "Select New Name",
            "Make Him a Woman",
            "Change Difficulty Level",
            "Return to Start Screen",
            "Begin the Adventure" };
        string[] difficultyLevels = {
            "Easy",
            "Normal",
            "Hard",
            "Ultimate" };
        string[] maleNames = {
            "Aris",
            "Barton",
            "Evander",
            "Kalven",
            "Llelwyn" };
        string[] femaleNames = {
            "Anwyn",
            "Cantrinia",
            "Julia",
            "Lucy",
            "Zoey" };

        public int Name
        {
            get { return name; }
        }

        public bool Gender
        {
            get { return gender; }
        }

        public int DifficultyLevel
        {
            get { return difficultyLevel; }
        }

        public int SelectedIndex
        {
            get { return menu.SelectedIndex; }
        }

        public CreateCharacterScreen()
        {

        }

        public void ChangeName()
        {
            Random random = new Random();
            name++;
            if (name == femaleNames.GetLength(0))
                name = 0;
        }
        public void ChangeGender()
        {
            if (gender)
                menuItems[1] = "Make Him a Woman";
            else
                menuItems[1] = "Make Her a Man";
            menu.SetMenuItems(menuItems);
            gender = !gender;
        }

        public void ChangeDifficulty()
        {
            difficultyLevel++;
            if (difficultyLevel == difficultyLevels.GetLength(0))
                difficultyLevel = 0;
        }



        protected override void Load()
        {
            string[] menuStrings = { "New Game", "Load Game", "Exit" };
            spriteFont = Engine.Content.Load<SpriteFont>("Content/Georgia");
            menu = new MenuComponent(spriteFont, menuStrings);
            spriteBatch = Engine.SpriteBatch;
            menu.Position = new Vector2((Engine.GraphicsDevice.PresentationParameters.BackBufferWidth -
            menu.Width) / 2,(Engine.GraphicsDevice.PresentationParameters.BackBufferHeight- menu.Height)/2 );
            menu.SetMenuItems(menuItems);
            AddComponent(menu);
            base.Load();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            Vector2 position = new Vector2();
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None);
            position = new Vector2(200, 100 - spriteFont.LineSpacing - 5);
            spriteBatch.DrawString(spriteFont,"Name", position,
            Color.Yellow);

            position.X = 340;
            spriteBatch.DrawString(spriteFont,
            "Gender",
            position,
            Color.Yellow);

            position.X = 500;
            spriteBatch.DrawString(spriteFont,
            "Game Mode",
            position,
            Color.Yellow);
            position = new Vector2(200, 100);
            if (gender)
            {
                spriteBatch.DrawString(spriteFont,
                femaleNames[name],
                position,
                Color.White);
                position.X = 340;
                spriteBatch.DrawString(spriteFont,
                "Female",
                position,
                Color.White);
            }
            else
            {
                spriteBatch.DrawString(spriteFont,
                maleNames[name],
                position,
                Color.White);
                position.X = 340;
                spriteBatch.DrawString(spriteFont,
                "Male",
                position,
                Color.White);
            }
            position.X = 500;
            spriteBatch.DrawString(spriteFont,
            difficultyLevels[difficultyLevel],
            position,
            Color.White);
            spriteBatch.End();
            base.Draw();
        }
    }
}
