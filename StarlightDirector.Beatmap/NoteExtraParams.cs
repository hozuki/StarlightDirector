using System;
using System.Globalization;

namespace StarlightDirector.Beatmap {
    public sealed class NoteExtraParams {

        internal NoteExtraParams() {
        }

        public double NewBpm { get; set; } = 120;

        public string GetDataString() {
            switch (Note.Basic.Type) {
                case NoteType.VariantBpm:
                    return NewBpm.ToString(CultureInfo.InvariantCulture);
                default:
                    throw new ArgumentOutOfRangeException(nameof(Note.Basic.Type));
            }
        }

        public void UpdateByDataString(string s) {
            UpdateByDataString(s, Note);
        }

        public void UpdateByDataString(string s, Note note) {
            Note = note;
            switch (note.Basic.Type) {
                case NoteType.VariantBpm:
                    NewBpm = double.Parse(s);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(note.Basic.Type));
            }
        }

        public static NoteExtraParams FromDataString(string str, Note note) {
            if (string.IsNullOrEmpty(str)) {
                return null;
            }
            var p = new NoteExtraParams {
                Note = note
            };
            switch (note.Basic.Type) {
                case NoteType.VariantBpm:
                    p.NewBpm = double.Parse(str);
                    break;
                default:
                    break;
            }
            return p;
        }

        public Note Note { get; internal set; }

    }
}
