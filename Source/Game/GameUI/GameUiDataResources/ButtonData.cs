using Godot;
using System;
using Game;

[GlobalClass]
public partial class ButtonData : Resource
{
    [Export()] private ButtonType _buttonType;
    public ButtonType ButtonType => _buttonType;
}


