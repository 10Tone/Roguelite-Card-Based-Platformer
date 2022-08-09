using AutoLoads;
using Godot;

namespace Game;

public class Interactable: Area2D
{
    protected GlobalEvents GlobalEvents;
    protected GlobalVariables GlobalVariables;

    public override void _EnterTree()
    {
        GlobalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        GlobalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");

        Connect("body_entered", this, nameof(OnBodyEntered));
    }

    protected virtual void OnBodyEntered(Node _body)
    {
        if (_body is KinematicBody2D && _body.IsInGroup("Player"))
        {
            var player = _body as IPlayer;
            
        }
    }
}