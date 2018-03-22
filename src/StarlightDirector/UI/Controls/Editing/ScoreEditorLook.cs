using System;
using System.Diagnostics;

namespace OpenCGSS.StarlightDirector.UI.Controls.Editing {
    public sealed class ScoreEditorLook : ICloneable<ScoreEditorLook> {

        internal event EventHandler<EventArgs> BarLineSpaceUnitChanged;

        public float BarLineSpaceUnit {
            [DebuggerStepThrough]
            get => _barLineSpaceUnit;
            set {
                var b = !value.Equals(_barLineSpaceUnit);
                if (b) {
                    _barLineSpaceUnit = value;
                    BarLineSpaceUnitChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool IndicatorsVisible { get; set; } = true;

        public PrimaryBeatMode PrimaryBeatMode { get; set; } = PrimaryBeatMode.EveryFourBeats;

        public static readonly float DefaultBarLineSpaceUnit = 7;

        public ScoreEditorLook Clone() {
            var result = new ScoreEditorLook {
                BarLineSpaceUnit = BarLineSpaceUnit,
                IndicatorsVisible = IndicatorsVisible,
                BarInfoTextVisible = BarInfoTextVisible,
                PrimaryBeatMode = PrimaryBeatMode,
                TimeInfoVisible = TimeInfoVisible
            };
            return result;
        }

        internal bool BarInfoTextVisible { get; set; } = true;

        internal bool TimeInfoVisible { get; set; } = false;

        private float _barLineSpaceUnit = DefaultBarLineSpaceUnit;

    }
}
