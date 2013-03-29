// GameFactory.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Factory methods for returning concrete implementations of a Game
//           It is intended that the external user only be able to interact
//           with Game types provided by the GameFactory

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquirrelGE
{   public static class GameFactory
    {   /// <summary>
        /// Creates a Gdi+ Game</summary>
        /// <param name="title">the title to appear on the window</param>
        /// <param name="width">Width of the client in pixels</param>
        /// <param name="height">Height of the client in pixels</param>
        /// <returns>A Game</returns>
        public static Game CreateGdiPlusGame(string title, int width, 
            int height)
        {   return new GdiPlusGame(title, width, height);
        }
    }
}
