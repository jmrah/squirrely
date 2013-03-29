using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquirrelGE;

namespace SquirrelThePirate2 
{   class EndState : IGameState
    {   private Level level;
        private Game game;

        public EndState(Game game)
        {
            this.game = game;
        }

        public Level Level
        {
            set 
            { 
                level = value; 
                level.Squirrel.Direction = Direction.Down;
                level.Squirrel.Speed = 0;
                level.Squirrel.Fps = 4;
                level.Squirrel.CurrentSequence = Squirrel.Celebrating;
            }
        }
        
        public void Run(double elapsedSeconds)
        {   


            level.Squirrel.Animate(elapsedSeconds);

            game.TileSprite(level.GrassSprite);


            foreach (BitmapSprite sprite in level.SandSprites)
            {   game.RenderSprite(sprite);
            }
            foreach (BitmapSprite sprite in level.WaterSprites)
            {   game.RenderSprite(sprite);
            }
            foreach (BitmapSprite sprite in level.ShoreSprites)
            {   game.RenderSprite(sprite);
            }
            foreach (BitmapSprite sprite in level.RockSprites)
            {   game.RenderSprite(sprite);
            }
            foreach (BitmapSprite sprite in level.AcornSprites)
            {   game.RenderSprite(sprite);
            }
            foreach (BitmapSprite sprite in level.BushSprites)
            {   game.RenderSprite(sprite);
            }
            foreach (BitmapSprite sprite in level.TreeSprites)
            {   game.RenderSprite(sprite);
            }
            game.RenderSprite(level.Squirrel);

            //game.DrawString("Level " + (levelManager.LevelNumber + 1), levelFont, Brushes.Black, levelFontShadowPoint);
            //game.DrawString("Level " + (levelManager.LevelNumber + 1), levelFont, Brushes.White, levelFontPoint);
        }
    }
}
