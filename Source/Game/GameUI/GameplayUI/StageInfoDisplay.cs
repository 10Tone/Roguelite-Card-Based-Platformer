using Godot;
using System;

public partial class StageInfoDisplay : Control
{
	[Export] private NodePath _stageNumberLabelPath, _minValueToAdvanceLabelPath, _currentBuildValueLabelPath,_maxPlaceablePlatformsLabelPath;
	
	private Label _stageNumberLabel, _minValueToAdvanceLabel, _currentBuildValueLabel, _maxPlaceablePlatformsLabel;
	
	
}
