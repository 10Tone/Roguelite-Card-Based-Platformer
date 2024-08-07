using Godot;

namespace Game.WorldBuilding;

public partial class Platform : StaticBody2D, IBuildItem
{
    private BuildItemResource _buildItemResource;

    // [Export()] public int BuildItemValue { get; set; }
    // [Export()] public BuildItemTypes BuildItemType { get; set; }
    [Export] public string ResourcePath { get; set; }

    BuildItemResource IBuildItem.BuildItemResource
    {
        get => _buildItemResource;
        set => _buildItemResource = value;
    }

    Resource BuildItemResource { get; set; }

    public override void _Ready()
    {
        BuildItemResource = GD.Load<Resource>(ResourcePath);
    }
}