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

namespace HakaGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    // TODO: remove engine passing from components and screens, just pass the texture from content.
    // TODO: Make folders for assets
    // TODO: Passing spritebatch in draw method
    // TODO: PopUpScreen and ButtonMenu, found from textbox tutorial
    // TODO: Refactor tilesets, maplayer and tilemap
    // TODO: Make tilesets to use same tileset, so we dont have to load the texture for every tile that
    // belongs to tileset
    // TODO: Make xml to use XNA content pipeline
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
  
        StartScreen startScreen;
        public StartScreen StartScreen
        {
            get { return startScreen; }
        }

        ActionScreen actionScreen;
        public ActionScreen ActionScreen
        {
            get { return actionScreen; }
        }

        HelpScreen helpScreen;
        public HelpScreen HelpScreen
        {
            get { return helpScreen; }
        }

        CharacterGeneratorScreen cgScreen;
        public CharacterGeneratorScreen CGScreen
        {
            get { return cgScreen; }
        }

        TitleScreen titleScreen;
        public TitleScreen TitleScreen
        {
            get { return titleScreen; }
        }

        public Session Session = Session.Instance;
        Engine engine;

        KeyboardState newState;
        KeyboardState oldState;

        public MainGame()

        {

            graphics = new GraphicsDeviceManager(this);

            Session.Initialize(this);
        }

        protected override void LoadContent()
        {
            
            engine = new Engine(this, graphics);

            startScreen = new StartScreen();
            helpScreen = new HelpScreen();
            actionScreen = new ActionScreen();
            cgScreen = new CharacterGeneratorScreen();
            titleScreen = new TitleScreen();

            engine.PushGameScreen(startScreen);
        }

        protected override void Update(GameTime gameTime)
        {
            newState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
            ButtonState.Pressed)
                this.Exit();

            oldState = newState;

            base.Update(gameTime);
            // Make the engine update itself. 
            // While updating itself it's also updating
            // screens and screens are updating components
            // and so on
            engine.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
            engine.Draw(gameTime);

  
        }

    }

}
