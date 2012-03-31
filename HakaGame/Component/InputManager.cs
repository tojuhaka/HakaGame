using System;
using System.Collections.Generic;
using System.Linq;
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
    // Handles keyboard inputs
    public class InputManager : Component
    {
        #region Fields
        KeyboardState keyboardState;
        KeyboardState lastKeyboardState;
        #endregion

        #region Constructor
        public InputManager()
            : base()
        {
            keyboardState = Keyboard.GetState();
        }
        #endregion

        #region Override methods
        protected override void Load()
        {
            base.Load();
        }

        public override void Update()
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            base.Update();
        }
        #endregion


        #region Keyboard methods
        public  KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }
        public  KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }
        public  bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) &&
            lastKeyboardState.IsKeyDown(key);
        }
        public  bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) &&
            lastKeyboardState.IsKeyUp(key);
        }
        #endregion
    }
}