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
    public class InputManager 
    {
        #region Fields
        static KeyboardState keyboardState;
        static KeyboardState lastKeyboardState;
        #endregion

        #region Constructor
        public InputManager()
            : base()
        {
            keyboardState = Keyboard.GetState();
        }
        #endregion

        #region Override methods

        public  void Update()
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
 
        }
        #endregion


        #region Keyboard methods
        public static  KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }
        public static KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }
        public static bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) &&
            lastKeyboardState.IsKeyDown(key);
        }
        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) &&
            lastKeyboardState.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public static void Flush()
        {
            lastKeyboardState = keyboardState;
        }
        #endregion
    }
}