// IGameState.cs
// Author:   Jamal Rahhali
// Date:     3/27/2013
// Purpose:  Any user code that wants to be run by the game must
//           implement this interface.  

namespace SquirrelGE
{   public interface IGameState
    {   
    /// <param name="elapedSeconds">
    /// The elapsed seconds since the last iteration of the main game loop
    /// </param>    
    void Run(double elapedSeconds);
    }
}
