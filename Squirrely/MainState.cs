// LevelWonState.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Main game state

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquirrelGE;
using System.Drawing;
using System.Windows.Forms;

namespace SquirrelThePirate2 
{   class MainState : IGameState 
    {   private Game game;
        private GameStateManager stateManager;
        private bool levelWon = false;
        private LevelManager levelManager;
        private Level currentLevel;
        private Font levelFont = new Font("Arial", 8);
        private Point levelFontShadowPoint = new Point(4, 4);
        private Point levelFontPoint = new Point(3, 3);
        private bool menuActive = false;

        public MainState(GameStateManager stateManager, Game game, LevelManager levelManager)
        {   this.stateManager = stateManager;
            this.game = game;
            this.levelManager = levelManager;
            currentLevel = levelManager.GetLevel(30);
        }

        public Level CurrentLevel
        {
            set { currentLevel = value; }
        }
        
        public void Run(double elapsedSeconds)
        {   
            CheckForWin();
            CheckForInput();
            if (!menuActive && elapsedSeconds < 0.05)
            { 
                SetAcornPushableDirections();
                CheckForSquirrelCollision();
                PushAcorns();

                foreach (BitmapSprite acorn in currentLevel.AcornSprites)
                {   acorn.Move(elapsedSeconds);
                }

                currentLevel.Squirrel.Animate(elapsedSeconds);
                currentLevel.Squirrel.Move(elapsedSeconds);

                game.TileSprite(currentLevel.SandSprite);

                foreach (BitmapSprite sprite in currentLevel.WaterSprites)
                {   game.RenderSprite(sprite);
                }
                foreach (BitmapSprite sprite in currentLevel.ShoreSprites)
                {   game.RenderSprite(sprite);
                }
                foreach (BitmapSprite sprite in currentLevel.RockSprites)
                {   game.RenderSprite(sprite);
                }
                foreach (BitmapSprite sprite in currentLevel.AcornSprites)
                {   game.RenderSprite(sprite);
                }
                foreach (BitmapSprite sprite in currentLevel.BushSprites)
                {   game.RenderSprite(sprite);
                }
                game.RenderSprite(currentLevel.Squirrel);

                game.DrawString("Level " + (levelManager.LevelNumber + 1), levelFont, Brushes.Black, levelFontShadowPoint);
                game.DrawString("Level " + (levelManager.LevelNumber + 1), levelFont, Brushes.White, levelFontPoint);
            }
        }

        public void CheckForWin()
        {   levelWon = true;
            foreach(BitmapSprite acorn in currentLevel.AcornSprites)
            {   if (!currentLevel.BushCoords.Contains(new Point((int)acorn.X, (int)acorn.Y)))
                {   levelWon = false;
                    break;
                }
            }
            if (levelWon)
            {   stateManager.ChangeGameState(GameStates.LevelWon);
            }
        }

        private void CheckForInput()
        {   switch (game.Input.KeyUp)
            {   case Keys.Up:
                    currentLevel.Squirrel.Speed = 0;
                    currentLevel.Squirrel.RotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    currentLevel.Squirrel.Direction = Direction.Up;
                    currentLevel.Squirrel.CurrentSequence = Squirrel.UpAndStopped;
                    break;
                case Keys.Right:
                    currentLevel.Squirrel.Speed = 0;
                    currentLevel.Squirrel.RotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    currentLevel.Squirrel.Direction = Direction.Right;
                    currentLevel.Squirrel.CurrentSequence = Squirrel.RightAndStopped;
                    break;
                case Keys.Down:
                    currentLevel.Squirrel.Speed = 0;
                    currentLevel.Squirrel.RotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    currentLevel.Squirrel.Direction = Direction.Down;
                    currentLevel.Squirrel.CurrentSequence = Squirrel.DownAndStopped;
                    break;
                case Keys.Left:
                    currentLevel.Squirrel.Speed = 0;
                    currentLevel.Squirrel.RotateFlipType = RotateFlipType.RotateNoneFlipX;
                    currentLevel.Squirrel.Direction = Direction.Left;
                    currentLevel.Squirrel.CurrentSequence = Squirrel.LeftAndStopped;
                    break;
            }

            switch (game.Input.KeyDown)
            {   case Keys.Up:
                    currentLevel.Squirrel.Speed = 80;
                    currentLevel.Squirrel.RotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    currentLevel.Squirrel.Direction = Direction.Up;
                    currentLevel.Squirrel.CurrentSequence = Squirrel.UpAndRunning;
                    break;
                case Keys.Right:
                    currentLevel.Squirrel.Speed = 80;
                    currentLevel.Squirrel.RotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    currentLevel.Squirrel.Direction = Direction.Right;
                    currentLevel.Squirrel.CurrentSequence = Squirrel.RightAndRunning;
                    break;
                case Keys.Down:
                    currentLevel.Squirrel.Speed = 80;
                    currentLevel.Squirrel.RotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    currentLevel.Squirrel.Direction = Direction.Down;
                    currentLevel.Squirrel.CurrentSequence = Squirrel.DownAndRunning;
                    break;
                case Keys.Left:
                    currentLevel.Squirrel.Speed = 80;
                    currentLevel.Squirrel.RotateFlipType = RotateFlipType.RotateNoneFlipX;
                    currentLevel.Squirrel.Direction = Direction.Left;
                    currentLevel.Squirrel.CurrentSequence = Squirrel.LeftAndRunning;
                    break;
                case Keys.Escape:
                    menuActive = true;
                    MenuForm menu = new MenuForm();
                    menu.ShowDialog();
                    MenuChoices choice = menu.MenuChoice;
                    menuActive = false;
                    if (choice == MenuChoices.Restart)
                    {   currentLevel = levelManager.GetCurrentLevel();
                    }
                    else if (choice == MenuChoices.Submit)
                    {   Level nextLevel = levelManager.GetLevel(menu.Password);
                        if (levelManager.IsGameOver())
                        {
                            stateManager.ChangeGameState(GameStates.TheEnd);
                            ((EndState)stateManager.CurrentGameState).Level = nextLevel;
                        }
                        else
                        {
                            this.CurrentLevel = nextLevel;
                        }
                    }                 
                    break;
            }
        }

        public void SetAcornPushableDirections()
        {   foreach (BitmapSprite acorn in currentLevel.AcornSprites)
            {   acorn.SetMoveable(Direction.Up, true);
                acorn.SetMoveable(Direction.Left, true);
                acorn.SetMoveable(Direction.Down, true);
                acorn.SetMoveable(Direction.Right, true);

                foreach (BitmapSprite innerAcorn in currentLevel.AcornSprites)
                {   if (acorn == innerAcorn) { continue; }

                    // if touching left and right
                    if (acorn.GameRectangle.Top == innerAcorn.GameRectangle.Top)
                    {   if (acorn.GameRectangle.Left == innerAcorn.GameRectangle.Right)
                        {   acorn.SetMoveable(Direction.Left, false);
                        }
                        else if (acorn.GameRectangle.Right == innerAcorn.GameRectangle.Left)
                        {   acorn.SetMoveable(Direction.Right, false);
                        }
                    }
                    // if touching up and down
                    if (acorn.GameRectangle.Left == innerAcorn.GameRectangle.Left)
                    {   if (acorn.GameRectangle.Top == innerAcorn.GameRectangle.Bottom)
                        {   acorn.SetMoveable(Direction.Up, false);
                        }
                        if (acorn.GameRectangle.Bottom == innerAcorn.GameRectangle.Top)
                        {   acorn.SetMoveable(Direction.Down, false);
                        }
                    }
                }

                foreach (BitmapSprite shoreLine in currentLevel.ShoreSprites)
                {   // touching left and right
                    if (acorn.GameRectangle.Top == shoreLine.GameRectangle.Top)
                    {   if (acorn.GameRectangle.Left == shoreLine.GameRectangle.Right)
                        {   acorn.SetMoveable(Direction.Left, false);
                        }
                        if (acorn.GameRectangle.Right == shoreLine.GameRectangle.Left)
                        {   acorn.SetMoveable(Direction.Right, false);
                        }
                    }
                    // touching up and down
                    if (acorn.GameRectangle.Left == shoreLine.GameRectangle.Left)
                    {   if (acorn.GameRectangle.Top == shoreLine.GameRectangle.Bottom)
                        {   acorn.SetMoveable(Direction.Up, false);
                        }
                        if (acorn.GameRectangle.Bottom == shoreLine.GameRectangle.Top)
                        {   acorn.SetMoveable(Direction.Down, false);
                        }
                    }
                }

                foreach (BitmapSprite rock in currentLevel.RockSprites)
                {   // touching left and right   
                    if (acorn.GameRectangle.Top == rock.GameRectangle.Top)
                    {   if (acorn.GameRectangle.Left == rock.GameRectangle.Right)
                        {   acorn.SetMoveable(Direction.Left, false);
                        }
                        if (acorn.GameRectangle.Right == rock.GameRectangle.Left)
                        {   acorn.SetMoveable(Direction.Right, false);
                        }
                    }
                    if (acorn.GameRectangle.Left == rock.GameRectangle.Left)
                    {   if (acorn.GameRectangle.Top == rock.GameRectangle.Bottom)
                        {   acorn.SetMoveable(Direction.Up, false);
                        }
                        if (acorn.GameRectangle.Bottom == rock.GameRectangle.Top)
                        {   acorn.SetMoveable(Direction.Down, false);
                        }
                    }
                }
            }
        }

        public void CheckForSquirrelCollision()
        {   Rectangle rect = currentLevel.Squirrel.GameAnchor;

            foreach (BitmapSprite shoreLine in currentLevel.ShoreSprites)
            {   // touching left and right
                if (rect.Top == shoreLine.GameRectangle.Top)
                {   if((rect.Right == shoreLine.GameRectangle.Left && currentLevel.Squirrel.Direction == Direction.Right) ||
                        (rect.Left == shoreLine.GameRectangle.Right && currentLevel.Squirrel.Direction == Direction.Left))
                    {   currentLevel.Squirrel.Speed = 0;
                        break;
                    }
                }
                // touching up and down
                if (rect.Left == shoreLine.GameRectangle.Left)
                {   if ((rect.Top == shoreLine.GameRectangle.Bottom && currentLevel.Squirrel.Direction == Direction.Up) ||                    
                        (rect.Bottom == shoreLine.GameRectangle.Top && currentLevel.Squirrel.Direction == Direction.Down))                    
                    {   currentLevel.Squirrel.Speed = 0;
                        break;
                    }
                }
            }

            foreach (BitmapSprite rock in currentLevel.RockSprites)
            {   // touching left and right
                if (rect.Top == rock.GameRectangle.Top)
                {   if ((rect.Right == rock.GameRectangle.Left && currentLevel.Squirrel.Direction == Direction.Right) ||
                        (rect.Left == rock.GameRectangle.Right && currentLevel.Squirrel.Direction == Direction.Left))
                    {   currentLevel.Squirrel.Speed = 0;
                        break;
                    }
                }
                // touching up and down
                if (rect.Left == rock.GameRectangle.Left)
                {   if ((rect.Top == rock.GameRectangle.Bottom && currentLevel.Squirrel.Direction == Direction.Up) ||
                        (rect.Bottom == rock.GameRectangle.Top && currentLevel.Squirrel.Direction == Direction.Down))
                    {   currentLevel.Squirrel.Speed = 0;
                        break;
                    }
                }
            }

            // a hack to fix a bug.  need to fix later
            foreach (BitmapSprite acorn in currentLevel.AcornSprites)
            {   if (rect.IntersectsWith(acorn.GameRectangle) && !acorn.IsMoveable(currentLevel.Squirrel.Direction))
                {   currentLevel.Squirrel.ForceSpeed(0);
                }
            }
        }

        public void PushAcorns()
        {   Rectangle rect = currentLevel.Squirrel.GameAnchor;
            foreach (BitmapSprite acorn in currentLevel.AcornSprites)
            {   if (acorn.IsMoveable(currentLevel.Squirrel.Direction) && rect.IntersectsWith(acorn.GameRectangle))
                {   acorn.Speed = currentLevel.Squirrel.Speed;
                    acorn.Direction = currentLevel.Squirrel.Direction;
                }
                else { acorn.Speed = 0; }
            }
        }
    }
}
