using Godot;

namespace Tools;

public partial class State: GodotObject
{
    protected float StartTime;

    public virtual void Enter()
    {
        DoChecks();
        StartTime = Time.GetTicksUsec();
    }

    public virtual void Exit()
    {
            
    }

    public virtual void LogicUpdate(double delta)
    {
            
    }

    public virtual void PhysicsUpdate(double delta)
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {
            
    }
}