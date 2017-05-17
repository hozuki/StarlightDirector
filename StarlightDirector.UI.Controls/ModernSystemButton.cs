using System;
using System.Drawing;
using System.Windows.Forms;

namespace StarlightDirector.UI.Controls {
    public sealed class ModernSystemButton : ModernButton {

        public ModernSystemButton() {
            IconColor = Color.Black;
            HoveringIconColor = Color.Navy;
            PressedIconColor = Color.White;
            SetStyle(ControlStyles.Selectable, false);
        }

        public event EventHandler<EventArgs> IconChanged;

        public ModernSystemButtonIcon Icon {
            get => _icon;
            set {
                var b = value != _icon;
                if (b) {
                    _icon = value;
                    OnIconChanged(EventArgs.Empty);
                }
            }
        }

        public Color IconColor { get; set; }

        public Color HoveringIconColor { get; set; }

        public Color PressedIconColor { get; set; }

        public Size IconSize { get; set; } = new Size(8, 8);

        public float IconStrokeWidth { get; set; } = 2;

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            if (Icon == ModernSystemButtonIcon.None) {
                return;
            }

            Color iconColor;
            switch (State) {
                case ModernButtonState.Normal:
                    iconColor = IconColor;
                    break;
                case ModernButtonState.Hovering:
                    iconColor = HoveringIconColor;
                    break;
                case ModernButtonState.Pressed:
                    iconColor = PressedIconColor;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var iconSize = IconSize;
            var clientRectangle = ClientRectangle;
            var x1 = (float)(clientRectangle.Width - iconSize.Width) / 2;
            var y1 = (float)(clientRectangle.Height - iconSize.Height) / 2;
            var x2 = x1 + iconSize.Width;
            var y2 = y1 + iconSize.Height;
            var graphics = e.Graphics;

            using (var p = new Pen(iconColor, IconStrokeWidth)) {
                switch (Icon) {
                    case ModernSystemButtonIcon.Close:
                        graphics.DrawLine(p, x1, y1, x2, y2);
                        graphics.DrawLine(p, x1, y2, x2, y1);
                        break;
                    case ModernSystemButtonIcon.Maximize:
                        graphics.DrawRectangle(p, x1, y1, iconSize.Width, iconSize.Height);
                        break;
                    case ModernSystemButtonIcon.Restore:
                        var w3 = (float)iconSize.Width / 3;
                        var h3 = (float)iconSize.Height / 3;
                        graphics.DrawRectangle(p, x1, y1 + h3, w3 * 2, h3 * 2);
                        var restorePoints = new[] { new PointF(x1 + w3, y1 + h3), new PointF(x1 + w3, y1), new PointF(x2, y1), new PointF(x2, y2 - h3), new PointF(x2 - w3, y2 - h3) };
                        graphics.DrawLines(p, restorePoints);
                        break;
                    case ModernSystemButtonIcon.Minimize:
                        graphics.DrawLine(p, x1, y2, x2, y2);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void OnIconChanged(EventArgs e) {
            IconChanged?.Invoke(this, e);
            Invalidate();
        }

        private ModernSystemButtonIcon _icon = ModernSystemButtonIcon.None;

    }
}
