using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;

namespace HakaGame
{
    public class TitleScreen : GameScreen
    {
        #region Fields and Properties

        Label label;
        #endregion
        #region Constructors
        public TitleScreen()
            : base()
        {
   
            Controls = new ControlManager();
        }
        #endregion
        #region Methods
        protected override void Load()
        {
            SpriteFont spriteFont = Engine.Content.Load<SpriteFont>("Content/Fonts/Georgia");
            label = new Label(spriteFont, Engine.SpriteBatch);
            label.Text = "PRESS START TO BEGIN";
            label.Size = spriteFont.MeasureString(label.Text);
            label.Position = new Vector2(
            (Engine.GameRef.Window.ClientBounds.Width - label.Size.X) / 2,
            (int)((Engine.GameRef.Window.ClientBounds.Height - label.Size.Y) / 2));
            Controls.Add(label);
            base.Load();
        }
        public override void Update()
        {
        
            Guide.BeginShowStorageDeviceSelector(GuideCallback, null);
            base.Update();
        }
        private void GuideCallback(IAsyncResult result)
        {
            StorageDevice selectedDevice;
            if (result.IsCompleted)
            {
                selectedDevice = Guide.EndShowStorageDeviceSelector(result);
                if (selectedDevice != null)
                {
                    Session.StorageDevice = selectedDevice;
                    Engine.PushGameScreen(Engine.GameRef.StartScreen);
                }
            }
        }
        public override void Draw()
        {
            Controls.Draw(Engine.SpriteBatch);
            base.Draw();
        }
        #endregion
    }
}