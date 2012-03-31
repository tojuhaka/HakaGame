using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HakaGame
{
    public class ScreenEventArgs : EventArgs
    {
        #region Fields and Properties
        GameScreen gameScreen;
        public GameScreen GameScreen
        {
            get { return gameScreen; }
            private set { gameScreen = value; }
        }
        #endregion

        #region Constructor
        public ScreenEventArgs(GameScreen gameScreen)
        {
            GameScreen = gameScreen;
        }
        #endregion
    }
}
