using System;
using System.Diagnostics;
using StarlightDirector.Core;

namespace StarlightDirector.Beatmap {
    public sealed class Bar : IStarlightObject {

        internal Bar(Score score, int index)
            : this(score, index, Guid.NewGuid()) {
        }

        internal Bar(Score score, int index, Guid id) {
            Basic = new BarBasicProperties(score) {
                ID = id,
                Index = index
            };
            Editor = new BarEditorProperties();
            Helper = new BarHelperProperties(this);
            Temporary = new BarTemporaryProperties();
        }

        public BarBasicProperties Basic { get; }

        public BarEditorProperties Editor { get; }

        public BarHelperProperties Helper { get; }

        public BarTemporaryProperties Temporary { get; }

        public BarParams Params { get; internal set; }

        public InternalList<Note> Notes { get; } = new InternalList<Note>();

        public Guid StarlightID {
            [DebuggerStepThrough]
            get { return Basic.ID; }
        }

        public override string ToString() {
            return $"Bar (Index={Basic.Index}, ID={StarlightID}, Notes={Notes.Count}, Start={Temporary.StartTime:mm\\:ss\\.fff})";
        }

        public sealed class BarBasicProperties {

            internal BarBasicProperties(Score score) {
                Score = score;
            }

            public Score Score { get; }

            public int Index { get; internal set; }

            public Guid ID { get; internal set; }

        }

        public sealed class BarEditorProperties {

            internal BarEditorProperties() {
            }

            public bool IsSelected { get; internal set; }

        }

        public sealed class BarHelperProperties {

            internal BarHelperProperties(Bar bar) {
                _bar = bar;
            }

            public bool HasAnyNote {
                [DebuggerStepThrough]
                get { return _bar.Notes.Count > 0; }
            }

            private readonly Bar _bar;

        }

        public sealed class BarTemporaryProperties {

            internal BarTemporaryProperties() {
            }

            public TimeSpan StartTime { get; internal set; }

        }

    }
}
