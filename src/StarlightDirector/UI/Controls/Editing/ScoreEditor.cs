using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Overlay;
using MonoGame.Extended.WinForms.WindowsDX;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.Models.Beatmap.Extensions;
using OpenCGSS.StarlightDirector.Models.Editor;
using OpenCGSS.StarlightDirector.UI.Controls.Rendering;

namespace OpenCGSS.StarlightDirector.UI.Controls.Editing {
    public sealed partial class ScoreEditor : D3DGameControl {

        public ScoreEditor() {
            _editorRenderer = new ScoreEditorRenderer();
            Look.BarLineSpaceUnitChanged += Look_BarLineSpaceUnitChanged;
            _graphicsBatch = new GraphicsBatch();
        }

        ~ScoreEditor() {
            Look.BarLineSpaceUnitChanged -= Look_BarLineSpaceUnitChanged;
        }

        public event EventHandler<EventArgs> EditModeChanged;

        [Browsable(false)]
        public ScrollBar ScrollBar { get; internal set; }

        public int ScrollOffsetX { get; set; }

        public int ScrollOffsetY {
            [DebuggerStepThrough]
            get => _scrollOffsetY;
            internal set {
                var scrollBar = ScrollBar;
                if (scrollBar != null) {
                    value = MathHelper.Clamp(value, scrollBar.Minimum, scrollBar.Maximum);
                }
                var b = value != _scrollOffsetY;
                if (b) {
                    _scrollOffsetY = value;
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        public ProjectDocument Project {
            [DebuggerStepThrough]
            get => _project;
            set {
                var b = value != _project;
                if (b) {
                    _project = value;
                    RecalcLayout();
                    UpdateBarStartTimeText();
                }
            }
        }

        [Browsable(false)]
        public Difficulty Difficulty {
            [DebuggerStepThrough]
            get => _difficulty;
            set {
                var b = value != _difficulty;
                if (b) {
                    _difficulty = value;
                    RecalcLayout();
                    UpdateBarStartTimeText();
                }
            }
        }

        [Browsable(false)]
        [CanBeNull]
        public Score CurrentScore {
            [DebuggerStepThrough]
            get => Project?.Project.GetScore(Difficulty);
        }

        [Browsable(false)]
        public ScoreEditMode EditMode {
            get => _editMode;
            set {
                var b = value != _editMode;
                if (b) {
                    _editMode = value;
                    OnEditModeChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        public NotePosition NoteStartPosition { get; set; }

        [Browsable(false)]
        public ScoreEditorConfig Config { get; } = new ScoreEditorConfig();

        [Browsable(false)]
        public ScoreEditorLook Look { get; } = new ScoreEditorLook();

        public float GetFullHeight() {
            var score = CurrentScore;
            if (score == null) {
                return 0;
            }
            var look = Look;
            var height = score.Bars.Sum(bar => look.BarLineSpaceUnit * bar.GetNumberOfGrids());
            return height;
        }

        protected override void OnClientSizeChanged(EventArgs e) {
            base.OnClientSizeChanged(e);
            RecalcLayout();
        }

        protected override void OnDraw(GameTime gameTime) {
            var graphicsDevice = GraphicsDevice;

            if (graphicsDevice != null) {
                graphicsDevice.Clear(Color.Transparent);

                _graphicsBatch.Update(graphicsDevice);

                var spriteBatch = _graphicsBatch.SpriteBatch;
                var graphics = _graphicsBatch.Graphics;

                if (spriteBatch != null && graphics != null) {
                    spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.LinearClamp, depthStencilState: DepthStencilState.Default);

                    graphics.Clear(Color.Transparent);

                    _editorRenderer.Render(graphics, CurrentScore, Config, Look, ScrollOffsetY, SelectionRectangle);

                    if (graphics.UpdateBackBuffer()) {
                        spriteBatch.Draw(graphics.BackBuffer, Vector2.Zero, Color.White);
                    }

                    spriteBatch.End();
                }
            }

            base.OnDraw(gameTime);
        }

        protected override void OnLoadContents() {
            base.OnLoadContents();

            _fontManager = new FontManager();

            var controlFont = Font;
            var baseFont = _fontManager.CreateFont(controlFont.OriginalFontName, FontStyle.Regular);
            baseFont.Size = controlFont.SizeInPoints;

            _editorRenderer.CreateResources(baseFont);
        }

        protected override void OnUnloadContents() {
            _editorRenderer.DisposeResources();
            _fontManager?.Dispose();
            _graphicsBatch?.Dispose();

            base.OnUnloadContents();
        }

        private void Look_BarLineSpaceUnitChanged(object sender, EventArgs eventArgs) {
            RecalcLayout();
            Invalidate();
        }

        private void OnEditModeChanged(EventArgs e) {
            EditModeChanged?.Invoke(this, e);
        }

        // This is used for scaling. It can be different with signature*gps.
        private static readonly int MaxNumberOfGrids = 96;


        private ProjectDocument _project;
        private Difficulty _difficulty = Difficulty.Debut;
        private ScoreEditMode _editMode;
        private int _scrollOffsetY;
        private readonly ScoreEditorRenderer _editorRenderer;

        private readonly GraphicsBatch _graphicsBatch;

        private FontManager _fontManager;

    }
}
