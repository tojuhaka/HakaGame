using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using HakaGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HakaGame
{
    public class Engine
    {
        #region Fields
        // Reference for GameObject
        Game gameRef;

        public Game GameRef
        {
            get { return gameRef; }
        }

        InputManager input;

        public InputManager Input
        {
            get { return input; }
        }
        // Happens when screen is changed
        public event EventHandler<ScreenEventArgs> OnScreenChange;

        // Game time provides us information on timing
        // such as the total elapsed time and the time since 
        // the last update
        GameTime gameTime = null;

        public GameTime GameTime
        {
            get { return gameTime; }
        }

        // Interacts with XNA's content pipeline
        // to load assets the game needs like models,
        // textures and fonts.
        IEContentManager content = null;
        public IEContentManager Content { get { return content; } }

        // Link to the graphics card
        GraphicsDevice graphicsDevice = null;
        public GraphicsDevice GraphicsDevice { get { return graphicsDevice; } }

        // Similar as above, but it handles all the 2D drawing and doesnt
        // Actually link directly to GraphicsCard
        SpriteBatch spriteBatch = null;
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }

        // Keeps tracks of objects used by many classes at once, like
        // Camera or physics
        IServiceContainer services = null;
        public IServiceContainer Services { get { return services; } }

        // Used as a stack handling the screens, this is used
        // for showing the screens, other list is meant to keep
        // the references of the screen, so we dont have to create
        // them again. This way we dont need a game reference in our
        // engine.
        private List<GameScreen> gameScreens = new List<GameScreen>();

        // this one stores the references of the screen so we can use them 
        // easily, alternative solution, maybe I will come up with something later
        // on

        private List<GameScreen> usedScreens = new List<GameScreen>();

        #endregion

        public Engine(Game gameRef, GraphicsDeviceManager Graphics)
        {
            this.input = new InputManager();
            this.gameRef = gameRef;
            services = new ServiceContainer();
            services.AddService(typeof(IGraphicsDeviceService), Graphics);
            services.AddService(typeof(IGraphicsDeviceManager), Graphics);
            this.graphicsDevice = Graphics.GraphicsDevice;
            content = new IEContentManager(Services);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.graphicsDevice = GraphicsDevice;

        }

        public void Update(GameTime GameTime)
        {
            this.input.Update();
            this.gameTime = GameTime;
            
            // Again we need to copy the list or we may
            // have some issues during iterate
            List<GameScreen> copy = new List<GameScreen>();

            foreach (GameScreen screen in gameScreens)
                copy.Add(screen);


            foreach (GameScreen screen in copy)
                screen.Update();
        }

        public void Draw(GameTime GameTime)
        {
            this.gameTime = GameTime;

            foreach (GameScreen screen in gameScreens)
                screen.Draw();
        }

        public void AddScreen(GameScreen screen)
        {
            usedScreens.Add(screen);
        }
        // Push a gamescreen on top of the stack
        public void PushGameScreen(GameScreen gameScreen)
        {
            //Throw exception if the GameScreen is already in use 
            if (gameScreen.Engine != null)
            {
                throw new Exception ("This GameScreen already exists on the stack of another " + 
                    "Engine instance");
            }
            
            if (!gameScreens.Contains(gameScreen))
            {
                gameScreens.Add(gameScreen);
                gameScreen.engine = this;
                gameScreen.LoadGameScreen();

                if (OnScreenChange != null)
                    OnScreenChange(this, new ScreenEventArgs(gameScreen));

            }
        }

        // Take the top screen of the stack
        public GameScreen PopGameScreen()
        {
            if (gameScreens.Count == 0)
                return null;

            GameScreen gameScreen = gameScreens[gameScreens.Count - 1];

            if (OnScreenChange != null)
                OnScreenChange(this, new ScreenEventArgs(gameScreen));

            gameScreen.engine = null;

            gameScreens.Remove(gameScreen);
            return gameScreen;
        }

        // less code with this :)
        public void ChangeGameScreen(GameScreen screen)
        {
            PopGameScreen();
            PushGameScreen(screen);
        }

        // When the screen calls this method, we just 
        // Check what is next. Temporal solution right now.
        public void QuitScreen(GameScreen oldScreen)
        {
            if (oldScreen is ActionScreen)
            {
                foreach (GameScreen newScreen in usedScreens)
                {
                    if (newScreen is StartScreen)
                    {
                        ChangeGameScreen(newScreen);
                    }
                }
            }
            return;
        }
        
    }
}
