// SquirrelyThePirate.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Main launch point of the game (besides the main function)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquirrelGE;

namespace SquirrelThePirate2
{   static class Squirrely
    {   public static void Run()
        {
            Game game = GameFactory.CreateGdiPlusGame(
                "Squirrely the Lost Squirrel",
                20 * 20, 
                20 * 12);
            LevelManager levelManager = new LevelManager();
            
            //
            // Add all of our game states
            //

            GameStates stateManager = new GameStates();
            stateManager.AddGameState(
                GameStates.Main, 
                new MainState(stateManager, game, levelManager));
            stateManager.AddGameState(
                GameStates.LevelWon, 
                new LevelWonState(stateManager, game, levelManager));
            stateManager.AddGameState(
                GameStates.Menu, 
                new MenuState(stateManager, game, levelManager));
            stateManager.AddGameState(
                GameStates.Intro, 
                new IntroState(stateManager, game));
            stateManager.AddGameState(
                GameStates.TheEnd, 
                new EndState(game));
            
            stateManager.ChangeGameState(GameStates.Intro);
            game.Run(stateManager);
        }
    }
}
