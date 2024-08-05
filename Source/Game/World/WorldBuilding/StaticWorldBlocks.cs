using Godot;

namespace Game.WorldBuilding;

public partial class StaticWorldBlocks : TileMapLayer
{
	// Method to check if a position is available
	public bool IsPositionAvailable(Vector2I tilePosition)
	{
		// Check if there's no tile at the given position
		return GetCellSourceId(tilePosition) == -1;
	}
}