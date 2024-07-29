using AutoLoads;
using Godot;
using Tools;


public partial class MainManager : Node2D
{
   private GlobalEvents _globalEvents;
   private GlobalVariables _globalVariables;

   public override void _EnterTree()
   {
      _globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
      _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
      // _globalEvents.Connect(nameof(GlobalEvents.GameReadyEventHandler), new Callable(this, nameof(OnGameReady)));
      _globalEvents.GameReady += OnGameReady;
   }

   private void OnGameReady()
   {
      DebugOverlay.Instance.DebugPrint("Game Loaded");
      // GD.Print("Game Loaded");
   }
}
