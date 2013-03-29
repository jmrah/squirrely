// GameStates.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Just provides string constants for the different game states.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquirrelGE;

namespace SquirrelThePirate2
{   class GameStates : GameStateManager
    {   public const string Main = "main";
        public const string LevelWon = "levelWon";
        public const string Menu = "menu";
        public const string Intro = "intro";
        public const string TheEnd = "theend";
    }
}
