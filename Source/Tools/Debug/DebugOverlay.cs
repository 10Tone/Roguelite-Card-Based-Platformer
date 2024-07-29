using Godot;
using System.Collections.Generic;

namespace Tools
{
    public partial class DebugOverlay : CanvasLayer
    {
        private static DebugOverlay _instance;
        public static DebugOverlay Instance => _instance;
        
        private Label _debugLabel;
        private List<string> _debugMessages = new List<string>();
        private const int MaxMessages = 10;

        public override void _EnterTree()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                QueueFree();
            }
        }
    
        public override void _Ready()
        {
            _debugLabel = new Label();
            _debugLabel.Modulate = new Color(231, 221, 0);
            _debugLabel.Position = new Vector2(20, 20);
            AddChild(_debugLabel);
        }
    
        public void DebugPrint(string message)
        {
            AddDebugMessage(message);
            // Also print to console for additional debugging
            GD.Print(message);
        }
    
        private void AddDebugMessage(string message)
        {
            _debugMessages.Add(message);
            if (_debugMessages.Count > MaxMessages)
                _debugMessages.RemoveAt(0);
    
            UpdateDebugLabel();
        }
    
        private void UpdateDebugLabel()
        {
            _debugLabel.Text = string.Join("\n", _debugMessages);
        }
    }
}

