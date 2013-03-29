// GameStateManager.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Manages the different game states that are in a game.
//           The different game states are created by the user by implementing
//           IGameState.  Those IGameState's should then be added to a
//           GameStateManager.  The game loop will run the current IGameState
//           accessing it through GameStateManager.

using System.Collections.Generic;
using System;

namespace SquirrelGE
{   public class GameStateManager
    {   private IGameState currentGameState = null;
        private Dictionary<string, IGameState> gameStateDictionary;

        public GameStateManager()
        {   gameStateDictionary = new Dictionary<string, IGameState>();
        }

        public IGameState CurrentGameState
        {   get { return currentGameState; }
        }

        /// <summary>
        /// Adds an IGameState</summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException">
        /// If duplicate key added.</exception>
        public void AddGameState(string key, IGameState value)
        {   gameStateDictionary.Add(key, value);
            if (currentGameState == null) { currentGameState = value; }
        }

        /// <summary>
        /// Changes the current IGameState being run by the game loop
        /// </summary>
        /// <param name="key">The key associated with the IGameState</param>
        /// <exception cref="ArgumentException">
        /// If the key does not exist.</exception>
        public void ChangeGameState(string key)
        {   if (!gameStateDictionary.TryGetValue(key, out currentGameState))
            {   throw new ArgumentException(
                    "No IGameState, " + key + ", exists", "key");
            }
        }
    }
}
