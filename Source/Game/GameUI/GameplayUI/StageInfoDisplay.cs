using Godot;
using System;
using AutoLoads;
using Game.LevelSystem;

public partial class StageInfoDisplay : Control
{
	[Export] private NodePath _stageNumberLabelPath, _minValueToAdvanceLabelPath, _currentBuildValueLabelPath,_maxPlaceablePlatformsLabelPath;
	
	private Label _stageNumberLabel, _minValueToAdvanceLabel, _currentBuildValueLabel, _maxPlaceablePlatformsLabel;
	
	private GlobalEvents _globalEvents;
	private GlobalVariables _globalVariables;

	[Export] private Color _negativeColor, _positiveColor;

	public override void _EnterTree()
	{
		_globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
		_globalVariables = GetNode<GlobalVariables>("/root/GlobalVariables");
		
		_globalEvents.StageValueUpdated += OnStageValueUpdated;
	}

	public override void _Ready()
	{
		_stageNumberLabel = GetNode<Label>(_stageNumberLabelPath);
		_minValueToAdvanceLabel = GetNode<Label>(_minValueToAdvanceLabelPath);
		_currentBuildValueLabel = GetNode<Label>(_currentBuildValueLabelPath);
		_maxPlaceablePlatformsLabel = GetNode<Label>(_maxPlaceablePlatformsLabelPath);
	}

	private void OnStageValueUpdated(int value, StageData stageData)
	{
		_stageNumberLabel.Text = stageData.StageNumber.ToString();
		_minValueToAdvanceLabel.Text = stageData.MinScoreToAdvance.ToString();
		_currentBuildValueLabel.Text = value.ToString();
		_maxPlaceablePlatformsLabel.Text = stageData.MaxPlaceablePlatforms.ToString();
		
		UpdateFontColor(value, stageData.MinScoreToAdvance, _currentBuildValueLabel);
	}

	private void UpdateFontColor(int x, int y, Label label)
	{
		if (x < y)
        {
            label.LabelSettings.FontColor = _negativeColor;
        }
        else if (x >= y)
        {
	        label.LabelSettings.FontColor =_positiveColor;
        }
        // else
        // {
	       //  label.LabelSettings.FontColor = new Color(1, 1, 1);
        // }
	}
}
