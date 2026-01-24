using Godot;

namespace DungeonCharlie.UI
{
    /// <summary>
    /// Animated damage number that floats up and fades out
    /// </summary>
    public partial class DamageNumber : Control
    {
        private Label _label;
        private float _lifetime = 1.5f;
        private float _elapsed = 0f;
        private Vector2 _velocity = new Vector2(0, -50);

        public override void _Ready()
        {
            _label = GetNode<Label>("Label");
        }

        /// <summary>
        /// Show damage number with specific value and color
        /// </summary>
        public void Show(int value, Color color, bool isCritical = false)
        {
            if (_label != null)
            {
                _label.Text = value.ToString();
                _label.AddThemeColorOverride("font_color", color);
                
                if (isCritical)
                {
                    _label.AddThemeFontSizeOverride("font_size", 48);
                    _label.Text = "!" + _label.Text + "!";
                }
            }
        }

        /// <summary>
        /// Show healing number
        /// </summary>
        public void ShowHealing(int value)
        {
            Show(value, new Color(0, 1, 0), false);
        }

        /// <summary>
        /// Show damage number
        /// </summary>
        public void ShowDamage(int value, bool isCritical = false)
        {
            Show(value, new Color(1, 0.2f, 0.2f), isCritical);
        }

        public override void _Process(double delta)
        {
            _elapsed += (float)delta;
            
            // Move up
            Position += _velocity * (float)delta;
            
            // Fade out
            float alpha = 1.0f - (_elapsed / _lifetime);
            Modulate = new Color(1, 1, 1, alpha);
            
            // Destroy when done
            if (_elapsed >= _lifetime)
            {
                QueueFree();
            }
        }
    }
}
