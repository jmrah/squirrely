// LevelWonState.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  GameState when the you beat a level!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquirrelGE;
using System.Drawing;

namespace SquirrelThePirate2
{   class LevelWonState : IGameState
    {   StringFormat stringFormat = new StringFormat();
        private Game game;
        private GameStateManager stateManager;
        private LevelManager levelManager;

        public LevelWonState(GameStateManager stateManager, Game game, LevelManager levelManager)
        {   this.game = game;
            this.stateManager = stateManager;
            this.levelManager = levelManager;
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }

        public void Run(double elapsedSeconds)
        {
            if (game.Input.KeyDown == System.Windows.Forms.Keys.Enter)
            {   
                Level nextLevel = levelManager.GetNextLevel();
                if (levelManager.IsGameOver())
                {
                    stateManager.ChangeGameState(GameStates.TheEnd);
                    ((EndState)stateManager.CurrentGameState).Level = nextLevel;
                }
                else
                {
                    stateManager.ChangeGameState(GameStates.Main);
                    ((MainState)stateManager.CurrentGameState).CurrentLevel = nextLevel;
                }
            }

            //
            // Very ugly..it was a quick fix at the time.  
            // Need to change this later
            //

            game.DrawString(
                "Level Complete!", 
                new Font("Arial", 24), 
                Brushes.Black, 
                new Rectangle(0, 0, game.Width + 4, game.Height + 4), 
                stringFormat);
            game.DrawString(
                "Level Complete!", 
                new Font("Arial", 24), 
                Brushes.Red, 
                new Rectangle(0, 0, game.Width, game.Height), 
                stringFormat);

            game.DrawString(
                "Press [Enter] to continue", 
                new Font("Arial", 12), 
                Brushes.Black, 
                new Rectangle(0, 0, game.Width + 3, game.Height + 50 + 3), 
                stringFormat);
            game.DrawString(
                "Press [Enter] to continue", 
                new Font("Arial", 12), 
                Brushes.White, 
                new Rectangle(0, 0, game.Width, game.Height + 50), 
                stringFormat);       
                
            game.DrawString(
                "Password: " + levelManager.GetPassword(), 
                new Font("Arial", 18), 
                Brushes.Black, 
                new Rectangle(0, 0, game.Width + 2, game.Height + 190 + 2), 
                stringFormat);
            game.DrawString(
                "Password: " + levelManager.GetPassword(), 
                new Font("Arial", 18), 
                Brushes.Pink, 
                new Rectangle(0, 0, game.Width, game.Height + 190), 
                stringFormat);                  
        }
    }
}
