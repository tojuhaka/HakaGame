using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HakaGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace HakaGame
{
    // This screen is the main screen of all actions.
    // Shows the whole gameworld
    public class ActionScreen : GameScreen
    {
        #region Fields
        TileEngine tileEngine;
        AnimatedSprite playerSprite;
        Dictionary<AnimationKey, Animation> animations;

        #endregion

        #region Constructors
        public ActionScreen() : base()
        {
            
        }
        #endregion

        #region Override Methods

        public override void Update()
        { 
            HandleInput();

            playerSprite.LockToViewport();

            Session.Camera.LockToSprite(playerSprite);
            Session.Camera.LockCamera();
            base.Update();
        }

        private void HandleInput()
        {
            Vector2 motion = new Vector2();

            if (InputManager.KeyReleased(Keys.Escape))
            {
                Engine.ChangeGameScreen(Engine.GameRef.StartScreen);
            }


            if (InputManager.KeyDown(Keys.Up))
            {
                motion.Y = -1;
                playerSprite.CurrentAnimation = AnimationKey.Up;
            }
             if (InputManager.KeyDown(Keys.Down))
            {
                motion.Y = 1;
                playerSprite.CurrentAnimation = AnimationKey.Down;
            }
              if (InputManager.KeyDown(Keys.Left))
            {
                motion.X = -1;
                playerSprite.CurrentAnimation = AnimationKey.Left;
            }
              if (InputManager.KeyDown(Keys.Right))
            {
                motion.X = 1;
                playerSprite.CurrentAnimation = AnimationKey.Right;
            }

            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                motion *= playerSprite.Speed;
                playerSprite.IsAnimating = true;

            }
            else
            {
                playerSprite.IsAnimating = false;
            }

            // Check for collision. This method creates
            // "fake"-position and uses it to check if the
            // tile we're going to move is unpassable.
            if (!CheckUnWalkableTile(playerSprite, motion))
                playerSprite.Position += motion;

        }
        protected override void Load()
        {
            // TODO: Test worked, no just put it to use content pipeline

            // Load each tile used in map and ask for a TileList
            TileLoader loader = new TileLoader("Content/XMLmap/testi.xml", Engine.Content);
            TileList list = loader.GetTileList();

            // Load all the layers from file and ask for a maplayer list
            LayerLoader layerLoader = new LayerLoader("Content/XMLmap/layertesti.xml", list);
            List<MapLayer> layers = layerLoader.GetLayerList();

            tileEngine = new TileEngine(32, 32, Engine.GameRef);
          
            // Make map with all those layers
            Session.CurrentMap = new TileMap(layers);

            // Add components and controls etc
            animations = new Dictionary<AnimationKey, Animation>(); ;
            Texture2D playerTexture = Engine.Content.Load<Texture2D>("content/Sprites/malefighter");
            Animation animation = new Animation(3, 28, 28, 0, 0);
            animations.Add(AnimationKey.Down, animation);
            animation = new Animation(3, 28, 28, 0, 28);
            animations.Add(AnimationKey.Left, animation);
            animation = new Animation(3, 28, 28, 0, 56);
            animations.Add(AnimationKey.Right, animation);
            animation = new Animation(3, 28, 28, 0, 84);
            animations.Add(AnimationKey.Up, animation);
            playerSprite = new AnimatedSprite(playerTexture, animations);
            playerSprite.IsAnimating = true;
   
            AddComponent(playerSprite);

            SpriteFont spriteFont = Engine.Content.Load<SpriteFont>("Content/Fonts/Georgia");

            Label text = new Label(spriteFont, Engine.SpriteBatch);
            text.Text = "HakaGame 1.0";
            text.Size = spriteFont.MeasureString(text.Text);
            text.Position = new Vector2(100, 100);
            Controls.Add(text);
            base.Load();

        }

        public override void Draw()
        {
            Engine.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None);  
            Session.DrawMap(Engine.SpriteBatch);
            Engine.SpriteBatch.End();
            base.Draw();
        }

        public bool CheckUnWalkableTile(AnimatedSprite sprite, Vector2 motion)
        {
            Vector2 nextLocation = sprite.Position + motion;
            Rectangle nextRectangle = new Rectangle(
            (int)nextLocation.X,
            (int)nextLocation.Y,
            sprite.Width,
            sprite.Height);
            if (motion.Y < 0 && motion.X < 0)
            {
                return Session.CurrentMap.CheckUpAndLeft(nextRectangle);
            }
            else if (motion.Y < 0 && motion.X == 0)
            {
                return Session.CurrentMap.CheckUp(nextRectangle);
            }
            else if (motion.Y < 0 && motion.X > 0)
            {
                return Session.CurrentMap.CheckUpAndRight(nextRectangle);
            }
            else if (motion.Y == 0 && motion.X < 0)
            {
                return Session.CurrentMap.CheckLeft(nextRectangle);
            }
            else if (motion.Y == 0 && motion.X > 0)
            {
                return Session.CurrentMap.CheckRight(nextRectangle);
            }
            else if (motion.Y > 0 && motion.X < 0)
            {
                return Session.CurrentMap.CheckDownAndLeft(nextRectangle);
            }
            else if (motion.Y > 0 && motion.X == 0)
            {
                return Session.CurrentMap.CheckDown(nextRectangle);
            }
            return Session.CurrentMap.CheckDownAndRight(nextRectangle);
        }
        #endregion
    }
}
