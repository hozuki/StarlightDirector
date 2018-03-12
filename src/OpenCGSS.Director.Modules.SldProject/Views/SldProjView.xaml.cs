using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Overlay;
using OpenCGSS.Director.Common.Extensions;
using OpenCGSS.Director.Modules.MonoGame.Controls;
using OpenCGSS.Director.Modules.SldProject.Rendering;
using OpenCGSS.Director.Modules.SldProject.ViewModels;

namespace OpenCGSS.Director.Modules.SldProject.Views {
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    /// SldProjView.xaml 的交互逻辑
    /// </summary>
    public partial class SldProjView : UserControl {

        public SldProjView() {
            InitializeComponent();

            _fontManager = new FontManager();
            var controlFont = _fontManager.CreateFont(FontFamily.Source, global::MonoGame.Extended.Overlay.FontStyle.Regular);
            controlFont.Size = (float)FontSize;

            _scoreEditorRenderer = new ScoreEditorRenderer(controlFont);

            RegisterEventHandlers();
        }

        public double ScrollOffsetY {
            get => (double)GetValue(ScrollOffsetYProperty);
            set {
                if (value < 0) {
                    value = 0;
                } else {
                    var scoreFullHeight = ScoreEditorRenderer.GetFullScoreHeight();
                    var max = scoreFullHeight;

                    if (value > max) {
                        value = max;
                    }
                }

                SetValue(ScrollOffsetYProperty, value);
            }
        }

        public static readonly DependencyProperty ScrollOffsetYProperty = DependencyProperty.Register(nameof(ScrollOffsetY), typeof(double), typeof(SldProjView),
            new PropertyMetadata(0d, OnScrollOffsetYChanged));

        [NotNull]
        public ScoreEditorRenderer ScoreEditorRenderer => _scoreEditorRenderer;

        [CanBeNull]
        internal SldProjViewModel SldProject { private get; set; }

        internal void UpdateVerticalLength() {
            var sldProject = SldProject;
            var project = sldProject?.Project;

            if (project == null) {
                return;
            }

            ScoreEditorRenderer.Score = project.GetScore(sldProject.SelectedDifficulty);

            var scoreFullHeight = ScoreEditorRenderer.GetFullScoreHeight();

            VerticalScroll.Maximum = scoreFullHeight;
        }

        internal void Redraw() {
            Game.Invalidate();
        }

        private static void OnScrollOffsetYChanged([NotNull] DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) {
            var sldProj = (SldProjView)dependencyObject;

            sldProj.Redraw();
        }

        private void RegisterEventHandlers() {
            //WeakEventManager<D3DGraphicsDeviceControl, EventArgs>.AddHandler(control, nameof(D3DGraphicsDeviceControl.DeviceDraw), Game_Draw);

            this.AddWeakHandler<SldProjView, MouseWheelEventArgs>(nameof(MouseWheel), Self_MouseWheel);
            Game.AddWeakHandler<DrawingSurface, DrawEventArgs>(nameof(DrawingSurface.Draw), Game_Draw);
            Game.AddWeakHandler<DrawingSurface, EventArgs>(nameof(DrawingSurface.SizeChanged), Game_SizeChanged);
            Dispatcher.AddWeakHandler<Dispatcher, EventArgs>(nameof(Dispatcher.ShutdownStarted), Dispatcher_ShutdownStarted);
        }

        private void Game_SizeChanged(object sender, EventArgs e) {
            UpdateVerticalLength();
        }

        private void Game_Draw(object sender, DrawEventArgs e) {
            var sldProject = SldProject;
            var project = sldProject?.Project;

            if (project == null) {
                return;
            }

            var g = e.GraphicsDevice;

            g.Clear(Color.Transparent);

            var score = project.GetScore(sldProject.SelectedDifficulty);
            var clientSize = new Vector2((float)Game.ActualWidth, (float)Game.ActualHeight);
            var verticalPosition = (float)ScrollOffsetY;

            //verticalPosition += clientSize.Y - _scoreEditorRenderer.EditorConfig.FirstBarBottomY;
            verticalPosition += clientSize.Y / 2;

            ScoreEditorRenderer.Score = score;
            ScoreEditorRenderer.Render(g, verticalPosition, clientSize, SelectionRectangle);
        }

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e) {
            _scoreEditorRenderer?.Dispose();
            _fontManager?.Dispose();
        }

        private void Self_MouseWheel(object sender, MouseWheelEventArgs e) {
            var isCtrlDown = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);

            if (isCtrlDown) {
                // Only zooming, no scrolling.

                if (e.Delta > 0) {
                    ZoomIn();
                } else if (e.Delta < 0) {
                    ZoomOut();
                }

                return;
            }

            var isShiftDown = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
            var verticalChangeValue = isShiftDown ? VerticalScroll.LargeChange : VerticalScroll.SmallChange;

            VerticalScroll.Value -= e.Delta * verticalChangeValue;
        }

        private readonly FontManager _fontManager;
        private readonly ScoreEditorRenderer _scoreEditorRenderer;

        private const float ZoomScale = 1.2f;

    }
}
