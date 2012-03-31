using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HakaGame
{
    // Just a test class
    public class CharacterGeneratorScreen : GameScreen
    {
        #region Fields and Properties
        LeftRightSelector genderSelector;
        LeftRightSelector classSelector;
        string[] genderItems = { "Male", "Female" };
        string[] classItems = { "Fighter", "Wizard", "Rogue", "Priest" };
        #endregion
        #region Constructors
        public CharacterGeneratorScreen()
            : base()
        {
            
            Controls = new ControlManager();
        }
        #endregion
        #region Methods
        protected override void Load()
        {
            BackgroundComponent bgComponent = new BackgroundComponent(Engine.Content.Load < Texture2D > ("Content/GUI/blackbackground"));
            SpriteFont spriteFont = Engine.Content.Load<SpriteFont>("Content/Fonts/MenuFont");
            Texture2D leftArrow = Engine.Content.Load<Texture2D>("Content/Controls/leftarrow");
            Texture2D rightArrow = Engine.Content.Load<Texture2D>("Content/Controls/rightarrow");
            Label label1 = new Label(spriteFont, Engine.SpriteBatch);
            
            AddComponent(bgComponent);
            label1.Text = "Make your Character";
            label1.Size = spriteFont.MeasureString(label1.Text);
            label1.Position = new Vector2((Engine.GameRef.Window.ClientBounds.Width - label1.Size.X) / 2, 100);
            Controls.Add(label1);

            genderSelector = new LeftRightSelector(
            spriteFont,
            leftArrow,
            rightArrow, Engine.SpriteBatch);
            genderSelector.SetItems(genderItems, 125);
            genderSelector.Position = new Vector2(label1.Position.X, 150);
            Controls.Add(genderSelector);

            classSelector = new LeftRightSelector(
            spriteFont,
            leftArrow,
            rightArrow, Engine.SpriteBatch);
            classSelector.SetItems(classItems, 125);
            classSelector.Position = new Vector2(label1.Position.X, 200);
            Controls.Add(classSelector);

            Label labelName = new Label(spriteFont, Engine.SpriteBatch);
            labelName.Text = "Name: ";
            labelName.Size = spriteFont.MeasureString(labelName.Text);
            labelName.Position = new Vector2(label1.Position.X, 270);
            Controls.Add(labelName);

            TextBox textbox = new TextBox(spriteFont, Engine.Content, Engine.SpriteBatch);
            textbox.Position = new Vector2(label1.Position.X, 320);
            Controls.Add(textbox);

            LinkLabel linkLabel1 = new LinkLabel(spriteFont, Engine.SpriteBatch);
            linkLabel1.Text = "Accept this character";
            linkLabel1.Position = new Vector2(textbox.Position.X, 370);
            Controls.Add(linkLabel1);

            linkLabel1.Selected += new EventHandler(linkLabel1_Selected);
            Controls.NextControl();
            base.Load();
        }
        void linkLabel1_Selected(object sender, EventArgs e)
        {
            InputManager.Flush();

            SaveData data = new SaveData();

            try
            {
                Session.SaveGame("newbeginning.xml");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            data.Gender = genderSelector.SelectedItem;
            data.Class = classSelector.SelectedItem;
            this.Engine.ChangeGameScreen(engine.GameRef.ActionScreen);
        }
        public override void Update()
        {
            if (InputManager.KeyPressed(Keys.Escape))
            {
                InputManager.Flush();
                Engine.ChangeGameScreen(Engine.GameRef.StartScreen);
            }
            base.Update();
        }
        public override void Draw()
        {
            base.Draw();
    
        }
        #endregion

    }
}
