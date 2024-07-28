using AutoLoads;
using Godot;

namespace Game;

public partial class Interactable: Area2D
{
    protected GlobalEvents GlobalEvents;
    protected GlobalVariables GlobalVariables;

    public override void _EnterTree()
    {
        GlobalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        GlobalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");

        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
    }

    private void OnBodyEntered(Node _body)
    {
        if (_body is CharacterBody2D && _body.IsInGroup("Player"))
        {
            var player = _body as IPlayer;
            BodyEnteredAction(player);
        }
    }

    protected virtual void BodyEnteredAction(IPlayer player)
    {
        
    }
}