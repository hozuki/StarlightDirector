using System;
using System.Diagnostics;
using JetBrains.Annotations;
using OpenCGSS.Director.Common;
using OpenCGSS.Director.Modules.SldProject.Core;

namespace OpenCGSS.Director.Modules.SldProject.Models {
    public sealed class ScoreEditorLook : NotifyPropertyChangedBase, ICloneable, ICloneable<ScoreEditorLook> {

        public float BarLineSpaceUnit {
            [DebuggerStepThrough]
            get => _barLineSpaceUnit;
            [DebuggerStepThrough]
            set {
                var b = !value.Equals(_barLineSpaceUnit);

                if (b) {
                    _barLineSpaceUnit = value;
                    NotifyOfPropertyChange(nameof(BarLineSpaceUnit));
                }
            }
        }

        public bool IndicatorsVisible {
            [DebuggerStepThrough]
            get => _indicatorsVisible;
            [DebuggerStepThrough]
            set {
                var b = value != _indicatorsVisible;

                if (b) {
                    _indicatorsVisible = value;
                    NotifyOfPropertyChange(nameof(IndicatorsVisible));
                }
            }
        }

        public PrimaryBeatMode PrimaryBeatMode {
            [DebuggerStepThrough]
            get => _primaryBeatMode;
            [DebuggerStepThrough]
            set {
                var b = _primaryBeatMode != value;

                if (b) {
                    _primaryBeatMode = value;
                    NotifyOfPropertyChange(nameof(PrimaryBeatMode));
                }
            }
        }

        public static readonly float DefaultBarLineSpaceUnit = 7;

        [NotNull]
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

        internal bool TimeInfoVisible { get; set; }

        object ICloneable.Clone() {
            return Clone();
        }

        private float _barLineSpaceUnit = DefaultBarLineSpaceUnit;
        private bool _indicatorsVisible = true;
        private PrimaryBeatMode _primaryBeatMode = PrimaryBeatMode.EveryFourBeats;

    }
}
