using Godot;

namespace Tools;

public partial class StateMachine: GodotObject
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        if(CurrentState is null){GD.PushWarning("CurrentState is null!");
            return;
        }
        CurrentState.Enter();
    }

    public virtual void ChangeState(State newState)
    {
        if(newState is null){GD.PushWarning("NewState is null!");
            return;
        }
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}