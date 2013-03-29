// Game.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Provides a public interface to start a Game.
//           It is intended that GameFactory return a concrete implementation
//           of this class.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SquirrelGE
{   public abstract class Game
    {   private GameLoop gameLoop;
        public GameInput Input;     //this is a quick hack i need to fix later

        internal Game()
        {
        }
        
        /// <summary>
        /// Starts the game running </summary>
        /// <param name="gameStateManager">
        /// A GameStateManager with at lease one IGameState added. </param>
        /// <remarks>
        /// Overriders of this method should start the game loop after the game
        /// is up and running. </remarks>
        public virtual void Run(GameStateManager gameStateManager)
        {   gameLoop = new GameLoop(this, gameStateManager);
        }

        internal GameLoop GameLoop
        {   get { return gameLoop; }
        }

        public abstract int Width { get; }
        public abstract int Height { get; }

        /// <summary>
        /// Clears the screen. </summary>
        /// <param name="color">
        /// The color to clear the screen to. </param>
        public abstract void Clear(Color color);

        //
        // Hmmm...I don't like how these are taking a BitmapSprite
        // I think i will have to try and abstract the BitmapSprite
        // up to a Sprite
        //

        public abstract void TileSprite(BitmapSprite sprite);

        public abstract void RenderSprite(BitmapSprite sprite);

		public abstract void DrawString(
			string text,
			Font font,
			Brush brush,
			PointF point
		);

        public abstract void DrawString(
            string s,
            Font font,
            Brush brush,
            RectangleF layoutRectangle,
            StringFormat format
        );

        /// <summary>
        /// Refreshes the game screen.
        /// </summary>
        /// <remarks>
        /// This method is intended to be called only by GameLoop</remarks>
        internal abstract void Refresh();
    }
}
