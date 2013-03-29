// GameTextures.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Implementation of a Game that uses the Gdi+ graphics.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SquirrelThePirate2
{   static class GameTextures
    {   public static readonly Bitmap TileMap = new Bitmap("img/Sprites.png");
        public static readonly Bitmap SquirrelMap = new Bitmap("img/Nuts.png");

        static GameTextures()
        {   TileMap.MakeTransparent(Color.FromArgb(0, 255, 0));
            SquirrelMap.MakeTransparent(Color.FromArgb(0, 255, 0));
        }
    }
}
