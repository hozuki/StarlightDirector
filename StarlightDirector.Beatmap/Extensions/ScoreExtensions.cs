using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using StarlightDirector.Core;

namespace StarlightDirector.Beatmap.Extensions {
    public static class ScoreExtensions {

        [DebuggerStepThrough]
        public static CompiledScore Compile(this Score score) {
            return Compile(score, null);
        }

        [DebuggerStepThrough]
        public static CompiledScore Compile(this Score score, TimeSpan userDefinedEnding) {
            return Compile(score, (TimeSpan?)userDefinedEnding);
        }

        [DebuggerStepThrough]
        public static Bar AppendBar(this Score score) {
            return AppendBar(score, Guid.NewGuid());
        }

        [DebuggerStepThrough]
        public static Bar AppendBar(this Score score, Guid id) {
            var bar = new Bar(score, score.Bars.Count, id);
            score.Bars.Add(bar);
            return bar;
        }

        [DebuggerStepThrough]
        public static Bar AppendBar(this Score score, int id) {
            var guid = StarlightID.GetGuidFromInt32(id);
            return AppendBar(score, guid);
        }

        [DebuggerStepThrough]
        public static Bar RemoveBar(this Score score, Bar bar) {
            if (!score.Bars.Contains(bar)) {
                throw new ArgumentException("Assigned bar is not in the score.", nameof(bar));
            }
            var barIndex = bar.Basic.Index;
            foreach (var b in score.Bars) {
                if (b.Basic.Index > barIndex) {
                    --b.Basic.Index;
                }
            }
            foreach (var note in bar.Notes) {
                note.ResetAsTap();
            }
            score.Bars.Remove(bar);
            return bar;
        }

        [DebuggerStepThrough]
        public static void RemoveBars(this Score score, IEnumerable<Bar> bars) {
            var barArr = bars.ToArray();
            foreach (var bar in barArr) {
                if (!score.Bars.Contains(bar)) {
                    throw new ArgumentException("Assigned bar is not in the score.", nameof(bar));
                }
            }
            var barIndices = barArr.Select(b => b.Basic.Index).ToArray();
            foreach (var bar in barArr) {
                foreach (var note in bar.Notes) {
                    note.ResetAsTap();
                }
                score.Bars.Remove(bar);
            }
            foreach (var bar in score.Bars) {
                var greaterNum = barIndices.Count(index => bar.Basic.Index > index);
                bar.Basic.Index -= greaterNum;
            }
        }

        [DebuggerStepThrough]
        public static Note FindNoteByID(this Score score, Guid id) {
            return score.Bars.SelectMany(bar => bar.Notes).FirstOrDefault(note => note.StarlightID == id);
        }

        public static TimeSpan CalculateDuration(this Score score) {
            var notes = score.GetAllNotes();
            var allBpmNotes = notes.Where(n => n.Basic.Type == NoteType.VariantBpm).ToArray();
            var currentTiming = score.Project.Settings.StartTimeOffset;
            var currentBpm = score.Project.Settings.BeatPerMinute;
            var currentInterval = MathHelper.BpmToInterval(currentBpm);
            foreach (var bar in score.Bars) {
                var currentGridPerSignature = bar.GetGridPerSignature();
                var numGrids = bar.GetNumberOfGrids();
                if (allBpmNotes.Any(n => n.Basic.Bar == bar)) {
                    var bpmNotesInThisBar = allBpmNotes.Where(n => n.Basic.Bar == bar).ToList();
                    bpmNotesInThisBar.Sort((n1, n2) => n1.Basic.IndexInGrid.CompareTo(n2.Basic.IndexInGrid));
                    var bpmNoteIndex = 0;
                    for (var i = 0; i < numGrids; ++i) {
                        if (bpmNoteIndex < bpmNotesInThisBar.Count) {
                            var bpmNote = bpmNotesInThisBar[bpmNoteIndex];
                            if (i == bpmNote.Basic.IndexInGrid) {
                                currentBpm = bpmNote.Params.NewBpm;
                                currentInterval = MathHelper.BpmToInterval(currentBpm);
                                ++bpmNoteIndex;
                            }
                        }
                        currentTiming += currentInterval * i / currentGridPerSignature;
                    }
                } else {
                    var currentSignature = bar.GetSignature();
                    currentTiming += currentInterval * currentSignature;
                }
            }
            return TimeSpan.FromSeconds(currentTiming);
        }

        public static void UpdateAllStartTimes(this Score score) {
            var notes = score.GetAllNotes();
            var allBpmNotes = notes.Where(n => n.Basic.Type == NoteType.VariantBpm).ToArray();
            var currentTiming = score.Project.Settings.StartTimeOffset;
            var currentBpm = score.Project.Settings.BeatPerMinute;
            var currentInterval = MathHelper.BpmToInterval(currentBpm);
            var currentBarIndex = 0;
            foreach (var bar in score.Bars) {
                bar.Temporary.StartTime = TimeSpan.FromSeconds(currentTiming);
                if (currentBarIndex == score.Bars.Count - 1) {
                    break;
                }
                var currentGridPerSignature = bar.GetGridPerSignature();
                var numGrids = bar.GetNumberOfGrids();
                if (allBpmNotes.Any(n => n.Basic.Bar == bar)) {
                    var bpmNotesInThisBar = allBpmNotes.Where(n => n.Basic.Bar == bar).ToList();
                    bpmNotesInThisBar.Sort((n1, n2) => n1.Basic.IndexInGrid.CompareTo(n2.Basic.IndexInGrid));
                    var bpmNoteIndex = 0;
                    for (var i = 0; i < numGrids; ++i) {
                        if (bpmNoteIndex < bpmNotesInThisBar.Count) {
                            var bpmNote = bpmNotesInThisBar[bpmNoteIndex];
                            if (i == bpmNote.Basic.IndexInGrid) {
                                currentBpm = bpmNote.Params.NewBpm;
                                currentInterval = MathHelper.BpmToInterval(currentBpm);
                                ++bpmNoteIndex;
                            }
                        }
                        currentTiming += currentInterval * i / currentGridPerSignature;
                    }
                } else {
                    var currentSignature = bar.GetSignature();
                    currentTiming += currentInterval * currentSignature;
                }
                ++currentBarIndex;
            }
        }

        internal static void UpdateNoteHitTimings(this Score score) {
            var notes = score.GetAllNotes();

            // First, calculate all timing at the grid lines.
            var allBpmNotes = notes.Where(n => n.Basic.Type == NoteType.VariantBpm).ToArray();
            var timings = new Dictionary<Bar, double[]>();
            var currentTiming = score.Project.Settings.StartTimeOffset;
            var currentBpm = score.Project.Settings.BeatPerMinute;
            var currentInterval = MathHelper.BpmToInterval(currentBpm);
            foreach (var bar in score.Bars) {
                var currentGridPerSignature = bar.GetGridPerSignature();
                var numGrids = bar.GetNumberOfGrids();
                var t = new double[numGrids];
                if (allBpmNotes.Any(n => n.Basic.Bar == bar)) {
                    // If there are variant BPM notes, we have to do some math...
                    var bpmNotesInThisBar = allBpmNotes.Where(n => n.Basic.Bar == bar).ToList();
                    bpmNotesInThisBar.Sort((n1, n2) => n1.Basic.IndexInGrid.CompareTo(n2.Basic.IndexInGrid));
                    var bpmNoteIndex = 0;
                    for (var i = 0; i < numGrids; ++i) {
                        if (bpmNoteIndex < bpmNotesInThisBar.Count) {
                            var bpmNote = bpmNotesInThisBar[bpmNoteIndex];
                            if (i == bpmNote.Basic.IndexInGrid) {
                                // Yes! We have a visitor: a variant BPM note!
                                currentBpm = bpmNote.Params.NewBpm;
                                currentInterval = MathHelper.BpmToInterval(currentBpm);
                                ++bpmNoteIndex;
                            }
                        }
                        t[i] = currentTiming;
                        currentTiming += currentInterval * i / currentGridPerSignature;
                    }
                } else {
                    // If there are no variant BPM notes, things get a lot easier.
                    for (var i = 0; i < numGrids; ++i) {
                        t[i] = currentTiming + currentInterval * i / currentGridPerSignature;
                    }
                    var currentSignature = bar.GetSignature();
                    currentTiming += currentInterval * currentSignature;
                }
                timings[bar] = t;
            }

            // Update the HitTiming property of each gaming note.
            foreach (var note in notes) {
                if (!note.Helper.IsGaming) {
                    continue;
                }
                note.Temporary.HitTiming = TimeSpan.FromSeconds(timings[note.Basic.Bar][note.Basic.IndexInGrid]);
            }
        }

        private static CompiledScore Compile(Score score, TimeSpan? userDefinedEnding) {
            var notes = score.GetAllNotes();

            // First, calculate all timing at the grid lines.
            var allBpmNotes = notes.Where(n => n.Basic.Type == NoteType.VariantBpm).ToArray();
            var timings = new Dictionary<Bar, double[]>();
            var currentTiming = score.Project.Settings.StartTimeOffset;
            var currentBpm = score.Project.Settings.BeatPerMinute;
            var currentInterval = MathHelper.BpmToInterval(currentBpm);
            foreach (var bar in score.Bars) {
                var currentGridPerSignature = bar.GetGridPerSignature();
                var numGrids = bar.GetNumberOfGrids();
                var t = new double[numGrids];
                if (allBpmNotes.Any(n => n.Basic.Bar == bar)) {
                    // If there are variant BPM notes, we have to do some math...
                    var bpmNotesInThisBar = allBpmNotes.Where(n => n.Basic.Bar == bar).ToList();
                    bpmNotesInThisBar.Sort((n1, n2) => n1.Basic.IndexInGrid.CompareTo(n2.Basic.IndexInGrid));
                    var bpmNoteIndex = 0;
                    for (var i = 0; i < numGrids; ++i) {
                        if (bpmNoteIndex < bpmNotesInThisBar.Count) {
                            var bpmNote = bpmNotesInThisBar[bpmNoteIndex];
                            if (i == bpmNote.Basic.IndexInGrid) {
                                // Yes! We have a visitor: a variant BPM note!
                                currentBpm = bpmNote.Params.NewBpm;
                                currentInterval = MathHelper.BpmToInterval(currentBpm);
                                ++bpmNoteIndex;
                            }
                        }
                        t[i] = currentTiming;
                        currentTiming += currentInterval * i / currentGridPerSignature;
                    }
                } else {
                    // If there are no variant BPM notes, things get a lot easier.
                    for (var i = 0; i < numGrids; ++i) {
                        t[i] = currentTiming + currentInterval * i / currentGridPerSignature;
                    }
                    var currentSignature = bar.GetSignature();
                    currentTiming += currentInterval * currentSignature;
                }
                timings[bar] = t;
            }

            // Then, create a list and fill in basic information.
            var compiledNotes = new List<CompiledNote>();
            var noteMap1 = new Dictionary<CompiledNote, Note>();
            var noteMap2 = new Dictionary<Note, CompiledNote>();
            foreach (var note in notes) {
                if (!note.Helper.IsGaming) {
                    continue;
                }
                var compiledNote = new CompiledNote {
                    Type = note.Basic.Type,
                    HitTiming = timings[note.Basic.Bar][note.Basic.IndexInGrid],
                    StartPosition = note.Basic.StartPosition,
                    FinishPosition = note.Basic.FinishPosition,
                    FlickType = note.Basic.FlickType,
                    IsSync = note.Helper.IsSync
                };
                compiledNotes.Add(compiledNote);
                noteMap1[compiledNote] = note;
                noteMap2[note] = compiledNote;
            }
            compiledNotes.Sort((n1, n2) => n1.HitTiming.CompareTo(n2.HitTiming));

            // Then, calculate group IDs.
            var currentGroupID = 1;
            foreach (var compiledNote in compiledNotes) {
                if (compiledNote.GroupID != 0) {
                    continue;
                }
                var note = noteMap1[compiledNote];
                if (!note.Helper.IsFlick && !note.Helper.IsSlide) {
                    continue;
                }
                var cn = compiledNote;
                if (note.Helper.IsFlick) {
                    var n = note;
                    while (n != null) {
                        cn.GroupID = currentGroupID;
                        n = n.Editor.NextFlick;
                        if (n != null) {
                            cn = noteMap2[n];
                        }
                    }
                    ++currentGroupID;
                } else if (note.Helper.IsSlide) {
                    var n = note;
                    var n2 = n;
                    while (n != null) {
                        cn.GroupID = currentGroupID;
                        n2 = n;
                        n = n.Editor.NextSlide;
                        if (n != null) {
                            cn = noteMap2[n];
                        }
                    }
                    // A slide group, directly followed by a flick group.
                    if (n2.Helper.HasNextFlick) {
                        n = n2.Editor.NextFlick;
                        while (n != null) {
                            cn = noteMap2[n];
                            cn.GroupID = currentGroupID;
                            n = n.Editor.NextFlick;
                        }
                    }
                    ++currentGroupID;
                }
            }

            // Then, add three key notes. (Keynotes, hahaha.)
            var totalNoteCount = compiledNotes.Count;
            var scoreInfoNote = new CompiledNote {
                Type = NoteType.NoteCount,
                // Here I used a trick. This will run well while violating the meaning of enum.
                FlickType = (NoteFlickType)totalNoteCount
            };
            var songStartNote = new CompiledNote {
                Type = NoteType.MusicStart
            };
            var endTiming = userDefinedEnding?.TotalSeconds ?? currentTiming;
            var songEndNote = new CompiledNote {
                Type = NoteType.MusicEnd,
                HitTiming = endTiming
            };
            compiledNotes.Insert(0, scoreInfoNote);
            compiledNotes.Insert(1, songStartNote);
            compiledNotes.Add(songEndNote);

            // Finally, ID them.
            var currentNoteID = 1;
            foreach (var note in compiledNotes) {
                note.ID = currentNoteID;
                ++currentNoteID;
            }

            // We did it! Oh yeah!
            var compiledScore = new CompiledScore(compiledNotes);
            return compiledScore;
        }

    }
}
