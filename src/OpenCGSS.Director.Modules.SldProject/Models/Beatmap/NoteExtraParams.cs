using System;
using System.Globalization;
using JetBrains.Annotations;

namespace OpenCGSS.Director.Modules.SldProject.Models.Beatmap {
    public sealed class NoteExtraParams {

        internal NoteExtraParams([NotNull] Note note) {
            Note = note;
        }

        public double NewBpm { get; set; } = 120;

        [NotNull]
        public string GetDataString() {
            switch (Note.Basic.Type) {
                case NoteType.VariantBpm:
                    return NewBpm.ToString(CultureInfo.InvariantCulture);
                default:
                    throw new ArgumentOutOfRangeException(nameof(Note.Basic.Type));
            }
        }

        public void UpdateByDataString([NotNull] string s) {
            var note = Note;
            switch (note.Basic.Type) {
                case NoteType.VariantBpm:
                    NewBpm = double.Parse(s);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(note.Basic.Type));
            }
        }

        [CanBeNull]
        [ContractAnnotation("str:null => null")]
        public static NoteExtraParams FromDataString([CanBeNull] string str, [NotNull] Note note) {
            if (string.IsNullOrEmpty(str)) {
                return null;
            }

            var p = new NoteExtraParams(note);

            switch (note.Basic.Type) {
                case NoteType.VariantBpm:
                    p.NewBpm = double.Parse(str);
                    break;
                default:
                    break;
            }

            return p;
        }

        [NotNull]
        public Note Note { get; }

    }
}
