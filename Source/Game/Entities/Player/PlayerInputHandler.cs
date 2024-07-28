using AutoLoads;
using Godot;

namespace Game;

public partial class PlayerInputHandler : Node
{
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    [Export()] private float _inputHoldTime = 0.1f;
    private float _jumpInputStartTime;
    private GlobalVariables _globalVariables;

    public override void _EnterTree()
    {
        base._EnterTree();
        _globalVariables = _globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
    }

    public override void _PhysicsProcess (double delta)
    {
        base._PhysicsProcess(delta);
        if (!_globalVariables.PlayerInputEnabled) return;
        CheckForJumpInput();
        CheckForHorizontalInput();
        CheckForVerticalInput();
    }
        
    private void CheckForHorizontalInput() =>
        HorizontalInput = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
    private void CheckForVerticalInput() =>
        VerticalInput = Input.GetActionStrength("ui_up") - Input.GetActionStrength("ui_down");
        
    private void CheckForJumpInput()
    {
        CheckJumpInputHoldTime();
        if (Input.IsActionJustPressed("jump"))
        {
            JumpInput = true;
            JumpInputStop = false;
            _jumpInputStartTime = Time.GetTicksUsec();
        }

        if (Input.IsActionJustReleased("jump"))
        {
            JumpInputStop = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.GetTicksUsec() >= _jumpInputStartTime + _inputHoldTime)
        {
            JumpInput = false;
        }
    }
}