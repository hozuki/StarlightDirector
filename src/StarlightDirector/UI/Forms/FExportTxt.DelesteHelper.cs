using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.Models.Deleste;
using OpenCGSS.StarlightDirector.Models.Gaming;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FExportTxt {

        private static class DelesteHelper {

            public static void WriteDelesteBeatmap([NotNull] TextWriter writer, [NotNull] Score score, Difficulty difficulty,
                [NotNull] string title, [NotNull] string composer, [NotNull] string lyricist, [NotNull] string bg,
                [NotNull] string song, int level, MusicColor color, int bgmVolume, int seVolume) {
                WriteBeatmapHeader(writer, difficulty, title, composer, lyricist, bg, song, level, color, bgmVolume, seVolume);
                WriteEntries(score, writer);
            }

            private static void WriteBeatmapHeader([NotNull] TextWriter writer, Difficulty difficulty,
                [NotNull] string title, [NotNull] string composer, [NotNull] string lyricist, [NotNull] string bg,
                [NotNull] string song, int level, MusicColor color, int bgmVolume, int seVolume) {
                writer.WriteLine("#utf8");
                writer.WriteLine("#Title {0}", title);
                writer.WriteLine("#Lyricist {0}", lyricist);
                writer.WriteLine("#Composer {0}", composer);
                writer.WriteLine("#Background {0}", bg);
                writer.WriteLine("#Song {0}", song);
                writer.WriteLine("#Lyrics lyrics.lyr");
                // Using the 240/0 preset as discussed at 2016-10-09. May be updated when a new version of Deleste Viewer is released.
                //writer.WriteLine("#BPM {0:F2}", score.Project.Settings.GlobalBpm);
                //writer.WriteLine("#Offset {0}", (int)Math.Round(score.Project.Settings.StartTimeOffset * 1000));
                writer.WriteLine("#BPM 240");
                // Don't know why, but the 40 ms lag shows up from nowhere.
                writer.WriteLine("#Offset -40");

                string s;
                switch (difficulty) {
                    case Difficulty.Debut:
                    case Difficulty.Regular:
                    case Difficulty.Pro:
                    case Difficulty.Master:
                        s = difficulty.ToString();
                        break;
                    case Difficulty.MasterPlus:
                        s = "Master+";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                writer.WriteLine("#Difficulty {0}", s);

                writer.WriteLine("#Level {0}", level);
                writer.WriteLine("#BGMVolume {0}", bgmVolume);
                writer.WriteLine("#SEVolume {0}", seVolume);

                switch (color) {
                    case MusicColor.Multicolor:
                        s = "All";
                        break;
                    case MusicColor.Cute:
                    case MusicColor.Cool:
                    case MusicColor.Passion:
                        s = color.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(color), color, null);
                }
                writer.WriteLine("#Attribute {0}", s);

                // Use the undocumented "Convert" format for CSV-converted beatmaps.
                writer.WriteLine("#Format Convert");
                writer.WriteLine();
            }

            private static void WriteEntries(Score score, TextWriter writer) {
                var notes = score.GetAllNotes();
                var groupIdMap = ComputeGroupIDs(notes);

                // group_id,measure_index:deleste_note_type:start_position:finish_position
                // * measure_index can be floating point numbers: 000.000000
                foreach (var note in notes) {
                    if (!note.Helper.IsGaming) {
                        continue;
                    }

                    var groupId = groupIdMap.ContainsKey(note) ? groupIdMap[note] : 0;
                    var hitTiming = (float)note.Temporary.HitTiming.TotalSeconds;
                    var noteType = TranslateNoteType(note);
                    var startPosition = note.Basic.StartPosition;
                    var finishPosition = note.Basic.FinishPosition;

                    writer.WriteLine("#{0},{1:000.000000}:{2}:{3}:{4}", groupId, hitTiming, (int)noteType, (int)startPosition, (int)finishPosition);
                }
            }

            private static IReadOnlyDictionary<Note, int> ComputeGroupIDs([NotNull, ItemNotNull] IReadOnlyList<Note> notes) {
                var result = new Dictionary<Note, int>();

                // ReSharper disable once InconsistentNaming
                // Then, calculate group IDs.
                var currentGroupID = 1;

                foreach (var note in notes) {
                    if (result.ContainsKey(note) && result[note] != 0) {
                        continue;
                    }

                    if (!note.Helper.IsFlick && !note.Helper.IsSlide) {
                        continue;
                    }

                    if (note.Helper.IsFlick) {
                        var n = note;

                        while (n != null) {
                            result[n] = currentGroupID;
                            n = n.Editor.NextFlick;
                        }

                        ++currentGroupID;
                    } else if (note.Helper.IsSlide) {
                        var n = note;
                        var n2 = n;

                        while (n != null) {
                            result[n] = currentGroupID;
                            n2 = n;
                            n = n.Editor.NextSlide;
                        }

                        // A slide group, directly followed by a flick group.
                        if (n2.Helper.HasNextFlick) {
                            n = n2.Editor.NextFlick;

                            while (n != null) {
                                result[n] = currentGroupID;
                                n = n.Editor.NextFlick;
                            }
                        }

                        ++currentGroupID;
                    }
                }

                return result;
            }

            private static DelesteNoteType TranslateNoteType(Note note) {
                switch (note.Basic.Type) {
                    case NoteType.TapOrFlick:
                        switch (note.Basic.FlickType) {
                            case NoteFlickType.None:
                                return DelesteNoteType.Tap;
                            case NoteFlickType.Left:
                                return DelesteNoteType.FlickLeft;
                            case NoteFlickType.Right:
                                return DelesteNoteType.FlickRight;
                            default:
                                // Should have thrown an exception.
                                return DelesteNoteType.Tap;
                        }
                    case NoteType.Hold:
                        return DelesteNoteType.Hold;
                    case NoteType.Slide:
                        if (note.Helper.HasNextFlick) {
                            var nextFlick = note.Editor.NextFlick;
                            if (nextFlick.Basic.FinishPosition > note.Basic.FinishPosition) {
                                return DelesteNoteType.FlickRight;
                            } else if (nextFlick.Basic.FinishPosition < note.Basic.FinishPosition) {
                                return DelesteNoteType.FlickLeft;
                            } else {
                                throw new ArgumentOutOfRangeException("Unsupported flick type for slide notes.");
                            }
                        } else {
                            return DelesteNoteType.Slide;
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }


        }

    }
}
