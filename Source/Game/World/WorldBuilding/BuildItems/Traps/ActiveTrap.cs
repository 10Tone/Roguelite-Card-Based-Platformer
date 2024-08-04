using Godot;
using System;
using Game.WorldBuilding;

namespace Game.WorldBuilding;

public partial class ActiveTrap: Trap
{
    [Export] private NodePath _animatedSprite2DPath;
    [Export] private NodePath _timerPath;
    
    private AnimatedSprite2D _animatedSprite;
    private Timer _timer;
    
    public override void _EnterTree()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>(_animatedSprite2DPath);
        _animatedSprite.AnimationFinished += OnAnimationFinished;
        _animatedSprite.Play("idle");
        
        _area2D = GetNode<Area2D>(_area2DPath);
        _area2D.BodyEntered += OnPlayerEntered;
        
        _timer = GetNode<Timer>(_timerPath);
        _timer.Timeout += OnTimerTimeout;
    }

    public override void _Ready()
    {

    }

    private void OnAnimationFinished()
    {
        _animatedSprite.Play("idle");
        _timer.Start(5f);
    }

    private new void OnPlayerEntered(Node2D player)
    {
        throw new NotImplementedException();
    }

    private void OnTimerTimeout()
    {
        _animatedSprite.Play("snap");
    }
}