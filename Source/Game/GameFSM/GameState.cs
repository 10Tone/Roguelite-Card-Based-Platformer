using AutoLoads;
using Tools;

namespace Game;

public partial class GameState: State
{
   protected GlobalEvents GlobalEvents { get; }
   protected GlobalVariables GlobalVariables { get; }
   
   protected GameState(GlobalEvents globalEvents, GlobalVariables globalVariables)
   {
      GlobalEvents = globalEvents;
      GlobalVariables = globalVariables;
   }
}