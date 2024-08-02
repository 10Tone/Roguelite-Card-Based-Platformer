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
        private const int MaxMessages = 30;

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
            // _debugLabel.Modulate = new Color(231, 221, 0);
            _debugLabel.Position = new Vector2(8, 8);
            var settings = new LabelSettings();
            settings.FontColor = new Color(231, 221, 0);
            settings.FontSize = 8;
            var font = GD.Load("res://Content Assets/Fonts/roboto.tres") as Godot.Font;
            settings.Font = font;
            _debugLabel.LabelSettings = settings;
            // _debugLabel.AddThemeFontSizeOverride("font_size", 6);
            AddChild(_debugLabel);
        }

        public void DebugPrint(string message)
        {
            string callingClassName = "Unknown";
            try
            {
                var frame = new System.Diagnostics.StackFrame(1, false);
                var method = frame.GetMethod();
                if (method != null && method.ReflectedType != null)
                {
                    callingClassName = method.ReflectedType.Name;
                }
            }
            catch (System.Exception)
            {
                // Silently handle any exceptions
            }

            string formattedMessage = $"[{callingClassName}] {message}";
            AddDebugMessage(formattedMessage);
            // Also print to console for additional debugging
            GD.Print(formattedMessage);
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
            // _debugLabel.Text = string.Join("\n", _debugMessages);
        }
    }
}