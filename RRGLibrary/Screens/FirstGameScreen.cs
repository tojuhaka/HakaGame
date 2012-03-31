using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HakaGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGLibrary.Components;

namespace RPGLibrary.Screens
{
    public class FirstGameScreen : GameScreen
    {
        const int SCREEN_WIDTH = 1024;
        const int SCREEN_HEIGHT = 768;
        AnimatedSprite Explosion;


        public FirstGameScreen()
        {

        }

        protected override void Load()
        {
            SpriteFont font  = Engine.Content.Load<SpriteFont>("Content/Georgia");

          
            

            Explosion = new AnimatedSprite(0, 0, 0,"Content/explosions",-4,0,64, 64, 16);
            Explosion.NextSheetLine(0);
            this.AddComponent(Explosion);

            // Used for getting keyevents
         /*   KeyboardInput kInput = new KeyboardInput();
            kInput.Name = "keyboard";
            this.AddComponent(kInput);*/

            Sprite sprite = new Sprite(300, 300, 0, "Content/puppy");
            sprite.Name = "puppy";
            this.AddComponent(sprite);
            sprite.SourceRectangle = new Rectangle(0, 0, 160, 150);
            sprite.Scale = 2f;
 

            Sprite sprite2 = new Sprite(200, 300, 0, "Content/puppy");
            sprite2.Name = "puppy2";
            this.AddComponent(sprite2);
            sprite2.SourceRectangle = new Rectangle(50, 40, 160, 150);
            sprite2.Scale = 2f;
 

            Sprite sprite3 = new Sprite(200, 1000, 0, "Content/puppy");
            sprite3.Name = "puppy3";
            this.AddComponent(sprite3);
            sprite3.SourceRectangle = new Rectangle(0, 0, 160, 150);
            sprite3.Scale = 2f;

        }

        public override void Update()
        {
            ProcessInput(this.Engine.GameTime);

            base.Update();
        }

        public override void Draw()
        {
            Sprite puppy = (Sprite)this["puppy"];
            Matrix transformMatrix = Matrix.CreateTranslation( -puppy.X + SCREEN_WIDTH /2 ,
                                                              -puppy.Y + SCREEN_HEIGHT/2 , 1);

            Engine.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None, transformMatrix);
            foreach (Component c in components)
            {
                if (c is DrawableComponent && ((DrawableComponent)c).Visible)
                {
                    ((DrawableComponent)c).Draw();
                }
            }
            Engine.SpriteBatch.End();
        }

        private void ProcessInput(GameTime gameTime)
        {

            KeyboardState newState = Keyboard.GetState();
            Keys[] PressedKeys = newState.GetPressedKeys();

            float vertMovement = 0.0f;
            float horzMovement = 0.0f;

            foreach (Keys key in PressedKeys)
            {
                switch (key)
                {
                    case Keys.A:
                        vertMovement -= 0.3f;
                        break;
                    case Keys.D:
                        vertMovement += 0.3f;
                        break;
                    case Keys.W:
                        horzMovement -= 0.3f;
                        break;
                    case Keys.S:
                        horzMovement += 0.3f;
                        break;
                    case Keys.Escape:
                       
                        break;
                    default:
                        break;
                }
            }

            Sprite puppy = (Sprite)this["puppy"];
            puppy.Move(this.Engine.GameTime, new Vector2(500), vertMovement, horzMovement);
        }
    }
}
