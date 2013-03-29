// GameLoop.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Controls the main game loop, refreshing the Game screen with
//           each iteration.

using System.Diagnostics;
using System;
using System.Windows.Forms;
using System.Threading;

namespace SquirrelGE
{   internal class GameLoop
    {   private Game game;   
        private GameStateManager stateManager;
        private Stopwatch stopwatch = new Stopwatch();

        internal GameLoop(Game game, GameStateManager stateManager)
        {   this.game = game;
            this.stateManager = stateManager;
        }

        /// <summary>
        /// Starts the main game loop</summary>
        /// <exception cref="InvalidOperationException">
        /// If no IGameState was added to the GameStateManager</exception>
        internal void Start()
        {   if (stateManager.CurrentGameState == null)
            {   throw new InvalidOperationException(
                    "No instance of an IGameState has been added to the " +
                    "GameStateManager class.");
            }

            //
            // For whatever reason, Stopwatch.Restart() doesn't give
            // accurate (or gives strange) time intervals within this loop, 
            // so I had toc calulate delta manually
            //

            stopwatch.Start();
            double previousTimeStamp = stopwatch.ElapsedTicks;

            while (stopwatch.IsRunning)
            {   double currentTimeStamp = stopwatch.ElapsedTicks;

                stateManager.CurrentGameState.Run(
                    (currentTimeStamp - previousTimeStamp) /
                    Stopwatch.Frequency);
                game.Refresh();
                previousTimeStamp = currentTimeStamp;
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Stops the game loop </summary>
        internal void Stop()
        {    stopwatch.Reset();
        }
    }
}