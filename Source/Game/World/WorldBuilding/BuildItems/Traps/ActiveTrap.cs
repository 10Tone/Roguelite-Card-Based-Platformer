using Godot;
using System;
using Game.WorldBuilding;

namespace Game.WorldBuilding;

public partial class ActiveTrap: Trap
{
    [Export] private NodePath _animatedSprite2DPath;
    [Export] private NodePath _idleTimerPath;
    [Export] private NodePath _activeTimerPath;
    [Export] private bool _oneShot;
    
    private AnimatedSprite2D _animatedSprite;
    private Timer _idleTimer;
    private Timer _activeTimer;
    
    public override void _EnterTree()
    {
        base._EnterTree();
        _animatedSprite = GetNode<AnimatedSprite2D>(_animatedSprite2DPath);
        _animatedSprite.AnimationFinished += OnAnimationFinished;
        _animatedSprite.Play("idle");
        
        _idleTimer = GetNode<Timer>(_idleTimerPath);
        _activeTimer = GetNode<Timer>(_activeTimerPath);
        _idleTimer.Timeout += OnIdleTimerTimeout;
        _activeTimer.Timeout += OnActiveTimerTimeout;
    }

    private void OnActiveTimerTimeout()
    {
        if (_oneShot)
        {
            return;}
        // GD.Print("active timer finished!");
        _animatedSprite.Play("idle");   
        _animatedSprite.Stop();
        _idleTimer.Start();
    }
    

    private void OnAnimationFinished()
    {
        if (!_oneShot)
        {
            return;}
        // GD.Print("animation finished!");
        _animatedSprite.Play("idle");
        _animatedSprite.Stop();
        _idleTimer.Start();
    }

    private void OnIdleTimerTimeout()
    {
        // GD.Print("idle timer finished!");
        _animatedSprite.Play("active");
        _activeTimer.Start();
    }
}