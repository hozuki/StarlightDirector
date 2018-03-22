using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.UI.Controls {
    public class ModernButton : Button {

        public ModernButton() {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ContainerControl | ControlStyles.Opaque, false);

            HoveringTextColor = Color.Black;
            PressedTextColor = Color.White;
            HoveringBackColor = Color.FromArgb(0xff, 0x17, 0xb5, 0xfe);
            PressedBackColor = Color.FromArgb(0xff, 0x10, 0xa0, 0xc0);
            _stringFormat = new StringFormat {
                FormatFlags = StringFormatFlags.MeasureTrailingSpaces,
                HotkeyPrefix = HotkeyPrefix.Show,
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisWord
            };
        }

        public event EventHandler<EventArgs> HoveringBackColorChanged;
        public event EventHandler<EventArgs> PressedBackColorChanged;
        public event EventHandler<EventArgs> HoveringTextColorChanged;
        public event EventHandler<EventArgs> PressedTextColorChanged;
        public event EventHandler<EventArgs> StateChanged;

        [CanBeNull]
        public Bitmap HoveringImage { get; set; }

        [CanBeNull]
        public Bitmap PressedImage { get; set; }

        public Color HoveringTextColor {
            get => _hoveringTextColor;
            set {
                var b = value != _hoveringTextColor;
                if (b) {
                    _hoveringTextColor = value;
                    OnHoveringTextColorChanged(EventArgs.Empty);
                }
            }
        }

        public Color PressedTextColor {
            get => _pressedTextColor;
            set {
                var b = value != _pressedTextColor;
                if (b) {
                    _pressedTextColor = value;
                    OnPressedTextColorChanged(EventArgs.Empty);
                }
            }
        }

        public Color HoveringBackColor {
            get => _hoveringBackColor;
            set {
                var b = value != _hoveringBackColor;
                if (b) {
                    _hoveringBackColor = value;
                    OnHoveringBackColorChanged(EventArgs.Empty);
                }
            }
        }

        public Color PressedBackColor {
            get => _pressedBackColor;
            set {
                var b = value != _pressedBackColor;
                if (b) {
                    _pressedBackColor = value;
                    OnPressedBackColorChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        public ModernButtonState State {
            get => _state;
            private set {
                var b = value != _state;
                if (b) {
                    _state = value;
                    OnStateChanged(EventArgs.Empty);
                }
            }
        }

        public bool Pressed {
            get => State == ModernButtonState.Pressed;
            set => State = value ? ModernButtonState.Pressed : ModernButtonState.Normal;
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            OnBackColorChanged(EventArgs.Empty);
            OnForeColorChanged(EventArgs.Empty);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _hoveringBackBrush?.Dispose();
                _hoveringTextBrush?.Dispose();
                _pressedBackBrush?.Dispose();
                _pressedTextBrush?.Dispose();
                _stringFormat?.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnBackColorChanged(EventArgs e) {
            _backBrush?.Dispose();
            _backBrush = new SolidBrush(BackColor);
            base.OnBackColorChanged(e);
        }

        protected override void OnForeColorChanged(EventArgs e) {
            _textBrush?.Dispose();
            _textBrush = new SolidBrush(ForeColor);
            base.OnForeColorChanged(e);
        }

        protected virtual void OnHoveringBackColorChanged(EventArgs e) {
            _hoveringBackBrush?.Dispose();
            _hoveringBackBrush = new SolidBrush(_hoveringBackColor);
            HoveringBackColorChanged?.Invoke(this, e);
        }

        protected virtual void OnPressedBackColorChanged(EventArgs e) {
            _pressedBackBrush?.Dispose();
            _pressedBackBrush = new SolidBrush(_pressedBackColor);
            PressedBackColorChanged?.Invoke(this, e);
        }

        protected virtual void OnHoveringTextColorChanged(EventArgs e) {
            _hoveringTextBrush?.Dispose();
            _hoveringTextBrush = new SolidBrush(_hoveringTextColor);
            HoveringTextColorChanged?.Invoke(this, e);
        }

        protected virtual void OnPressedTextColorChanged(EventArgs e) {
            _pressedTextBrush?.Dispose();
            _pressedTextBrush = new SolidBrush(_pressedTextColor);
            PressedTextColorChanged?.Invoke(this, e);
        }

        protected virtual void OnStateChanged(EventArgs e) {
            StateChanged?.Invoke(this, e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            DrawButton(e.Graphics, _state);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            if (Enabled) {
                State = ModernButtonState.Pressed;
            } else {
                Invalidate();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            if (Enabled) {
                if (ControlGeometryHelper.PointInRect(e.Location, ClientRectangle)) {
                    State = ModernButtonState.Hovering;
                } else {
                    State = ModernButtonState.Normal;
                }
            } else {
                Invalidate();
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e) {
            if (Enabled) {
                State = ModernButtonState.Hovering;
            } else {
                Invalidate();
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e) {
            if (Enabled) {
                State = ModernButtonState.Normal;
            } else {
                Invalidate();
            }
            base.OnMouseLeave(e);
        }

        private void DrawButton(Graphics graphics, ModernButtonState state, bool drawImage = true) {
            var clientRectangle = ClientRectangle;
            if (Enabled) {
                switch (state) {
                    case ModernButtonState.Normal:
                        graphics.FillRectangle(_backBrush, clientRectangle);
                        break;
                    case ModernButtonState.Hovering:
                        graphics.FillRectangle(_hoveringBackBrush, clientRectangle);
                        break;
                    case ModernButtonState.Pressed:
                        graphics.FillRectangle(_pressedBackBrush, clientRectangle);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(state), state, null);
                }
            } else {
                graphics.FillRectangle(_backBrush, clientRectangle);
            }
            var text = Text;
            if (!string.IsNullOrEmpty(text)) {
                switch (state) {
                    case ModernButtonState.Normal:
                        graphics.DrawString(text, Font, _textBrush, clientRectangle, _stringFormat);
                        break;
                    case ModernButtonState.Hovering:
                        graphics.DrawString(text, Font, _hoveringTextBrush, clientRectangle, _stringFormat);
                        break;
                    case ModernButtonState.Pressed:
                        graphics.DrawString(text, Font, _pressedTextBrush, clientRectangle, _stringFormat);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(state), state, null);
                }
            }
            if (drawImage) {
                var image = Image;
                switch (state) {
                    case ModernButtonState.Normal:
                        if (image != null) {
                            graphics.DrawImageUnscaled(image, (clientRectangle.Width - image.Width) / 2, (clientRectangle.Height - image.Height) / 2);
                        }
                        break;
                    case ModernButtonState.Hovering:
                        var hoveringImage = HoveringImage;
                        if (hoveringImage != null) {
                            graphics.DrawImageUnscaled(hoveringImage, (clientRectangle.Width - hoveringImage.Width) / 2, (clientRectangle.Height - hoveringImage.Height) / 2);
                        } else if (image != null) {
                            graphics.DrawImageUnscaled(image, (clientRectangle.Width - image.Width) / 2, (clientRectangle.Height - image.Height) / 2);
                        }
                        break;
                    case ModernButtonState.Pressed:
                        var pressedImage = PressedImage;
                        if (pressedImage != null) {
                            graphics.DrawImageUnscaled(pressedImage, (clientRectangle.Width - pressedImage.Width) / 2, (clientRectangle.Height - pressedImage.Height) / 2);
                        } else if (image != null) {
                            graphics.DrawImageUnscaled(image, (clientRectangle.Width - image.Width) / 2, (clientRectangle.Height - image.Height) / 2);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(state), state, null);
                }
            }
        }

        private Color _hoveringTextColor = Color.Transparent;
        private Color _pressedTextColor = Color.Transparent;
        private Color _hoveringBackColor = Color.Transparent;
        private Color _pressedBackColor = Color.Transparent;

        private SolidBrush _hoveringTextBrush;
        private SolidBrush _pressedTextBrush;
        private SolidBrush _hoveringBackBrush;
        private SolidBrush _pressedBackBrush;
        private SolidBrush _textBrush;
        private SolidBrush _backBrush;
        private readonly StringFormat _stringFormat;

        private ModernButtonState _state = ModernButtonState.Normal;

    }
}
