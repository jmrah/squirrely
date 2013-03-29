// IntroState.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  GameState when the game starts up

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquirrelGE;
using System.Drawing;

namespace SquirrelThePirate2
{   class IntroState : IGameState
    {   StringFormat stringFormat = new StringFormat();
        private Game game;
        private GameStateManager stateManager;

        public IntroState(GameStateManager stateManager, Game game)
        {   this.game = game;
            this.stateManager = stateManager;
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }

        public void Run(double elapsedSeconds)
        {   if (game.Input.KeyDown != System.Windows.Forms.Keys.None)
            {   stateManager.ChangeGameState(GameStates.Main);
                //((MainState)stateManager.CurrentGameState).NextLevel();
            }

            //
            // Very ugly..it was a quick fix at the time.  
            // Need to change this later
            //
            game.Clear(Color.Beige);

            game.DrawString(
                "Squirrely the Lost Squirrel", 
                new Font("Arial", 24), 
                Brushes.Black, 
                new Rectangle(0, 0, game.Width + 4, game.Height - 150 + 4), 
                stringFormat);
            game.DrawString(
                "Squirrely the Lost Squirrel", 
                new Font("Arial", 24), 
                Brushes.Red, 
                new Rectangle(0, 0, game.Width, game.Height - 150), 
                stringFormat);

            /*game.DrawString(
                "Poor Squirrely has been ship wrecked and needs to find his way back home!  Help Squirrely hide all the acorns on each island to so he can continue his journey home.", 
                new Font("Arial", 10), 
                Brushes.Black, 
                new Rectangle(0, 0, game.Width + 1, game.Height + 50 + 1), 
                stringFormat);*/
            game.DrawString(
                "Poor Squirrely has been ship wrecked and needs to find his way back home!  Help Squirrely hide all the acorns on each island so he can continue his journey home.", 
                new Font("Arial", 10), 
                Brushes.DarkOrange, 
                new Rectangle(0, 0, game.Width, game.Height - 50), 
                stringFormat);
                /*
            game.DrawString(
                "Instructions: Hide your acorns in the bushes!", 
                new Font("Arial", 10), 
                Brushes.Black, 
                new Rectangle(0, 0, game.Width + 2, game.Height + 50 + 2), 
                stringFormat);
            game.DrawString(
                "Instructions: Hide your acorns in the bushes!", 
                new Font("Arial", 10), 
                Brushes.Orange, 
                new Rectangle(0, 0, game.Width, game.Height + 50), 
                stringFormat);
                 * */

            game.DrawString(
                "Controls: Left, Right, Up, Down | [Esc] for game menu.", 
                new Font("Arial", 10), 
                Brushes.Black, 
                new Rectangle(0, 0, game.Width + 2, game.Height + 100 + 2), 
                stringFormat);
            game.DrawString("Controls: Left, Right, Up, Down | [Esc] for game menu.", 
                new Font("Arial", 10), 
                Brushes.Orange, 
                new Rectangle(0, 0, game.Width, game.Height + 100), 
                stringFormat);

            game.DrawString("Press any key to begin!", 
                new Font("Arial", 10), 
                Brushes.Black, 
                new Rectangle(0, 0, game.Width + 1, game.Height + 150 + 1), 
                stringFormat);
            game.DrawString("Press any key to begin!", 
                new Font("Arial", 10), 
                Brushes.White, 
                new Rectangle(0, 0, game.Width, game.Height + 150), 
                stringFormat);
                 
        }
    }
}
