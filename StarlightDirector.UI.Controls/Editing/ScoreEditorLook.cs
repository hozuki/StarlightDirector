using System;
using System.Diagnostics;

namespace StarlightDirector.UI.Controls.Editing {
    public sealed class ScoreEditorLook {

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

        private float _barLineSpaceUnit = DefaultBarLineSpaceUnit;

    }
}
