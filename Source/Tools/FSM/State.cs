using Godot;

namespace Tools;

public class State
{
    protected float StartTime;

    public virtual void Enter()
    {
        DoChecks();
        StartTime = OS.GetTicksUsec();
    }

    public virtual void Exit()
    {
            
    }

    public virtual void LogicUpdate(float delta)
    {
            
    }

    public virtual void PhysicsUpdate(float delta)
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {
            
    }
}