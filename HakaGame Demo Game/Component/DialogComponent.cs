using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HakaGame
{
    // Simple box for dialogs in game
    public class DialogComponent : DrawableComponent
    {
        Texture2D dialogTexture;
        Rectangle aPosition;
        IEContentManager content;
        int resWidth;
        int resHeight;
        string filename = @"GUI\conversationbox";

        public DialogComponent(IEContentManager content, string textureFileName)
        {
            this.content = content;
            this.filename = textureFileName;
        }

        public DialogComponent(IEContentManager content)
        {
            this.content = content;
        }

        public void SetResolution(int width, int height) 
        {
            this.resWidth = width;
            this.resHeight = height;
        }

        protected override void Load()
        {
            dialogTexture = content.Load<Texture2D>(filename);
            aPosition = new Rectangle((resWidth - dialogTexture.Width) / 2,
            (resHeight - dialogTexture.Height) / 2,
            dialogTexture.Width,
            dialogTexture.Height);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            spriteBatch.Draw(dialogTexture, position, Color.White);
        }

    }
}
