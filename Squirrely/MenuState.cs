// ********************
// CURRENT NOT USED!
// ********************

// MenuState.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Menu when you press esc.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquirrelGE;
using System.Drawing;
using System.Windows.Forms;

namespace SquirrelThePirate2
{   class MenuState : IGameState
    {   StringFormat stringFormat = new StringFormat();
        private Game game;
        private GameStateManager stateManager;
        private Color continueColor = Color.Gray;
        private Color restartColor = Color.Red;
        private MenuChoice userChoice = MenuChoice.Restart;
        private LevelManager levelManager;
        private Form menuForm;

        private enum MenuChoice
        {   Restart,
            BackToGame
        }

        public MenuState(GameStateManager stateManager, Game game, LevelManager levelManager)
        {   this.game = game;
            this.stateManager = stateManager;
            this.levelManager = levelManager;
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            menuForm = new Form();
            menuForm.Size = new Size(300, 300);
        }

        public void Run(double elapsedSeconds)
        {   if (game.Input.KeyDown == System.Windows.Forms.Keys.Enter)
            {   if (userChoice == MenuChoice.Restart)
                {   stateManager.ChangeGameState(GameStates.Main);
                    ((MainState)stateManager.CurrentGameState).CurrentLevel = levelManager.GetCurrentLevel();
                }
                else if (userChoice == MenuChoice.BackToGame)
                {   userChoice = MenuChoice.Restart;
                    stateManager.ChangeGameState(GameStates.Main);
                }
            }
            else if ((game.Input.KeyDown == System.Windows.Forms.Keys.Up || game.Input.KeyDown == System.Windows.Forms.Keys.Down))
            {   if (userChoice == MenuChoice.BackToGame)
                {   userChoice = MenuChoice.Restart;
                }
                else
                {   userChoice = MenuChoice.BackToGame;
                }

                Color temp = continueColor;
                continueColor = restartColor;
                restartColor = temp;
            }

            if (!menuForm.Visible) { menuForm.ShowDialog(); }

            // AHH, ugly

            //game.FillRectangle(new SolidBrush(Color.FromArgb(15, 50, 50, 50)), new Rectangle(0, 0, game.Width, game.Height));
            game.DrawString(
                "Continue", 
                new Font("Arial", 16), 
                new SolidBrush(continueColor), 
                new Rectangle(0, 0, game.Width, game.Height - 50), 
                stringFormat);

            game.DrawString("Restart", 
                new Font("Arial", 16), 
                new SolidBrush(restartColor), 
                new Rectangle(0, 0, game.Width, game.Height), 
                stringFormat);
        }
    }
}
