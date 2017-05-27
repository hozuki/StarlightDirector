using System;
using StarlightDirector.Beatmap;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI {
    public static class SpecialTranslations {

        public static string Difficulty(LanguageManager manager, Difficulty difficulty) {
            var defaultTranslation = DescribedEnumConverter.GetEnumDescription(difficulty);
            if (manager == null) {
                return defaultTranslation;
            }
            string translation;
            switch (difficulty) {
                case Beatmap.Difficulty.Invalid:
                    throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, "Invalid difficulty for translation.");
                case Beatmap.Difficulty.Debut:
                    translation = manager.GetString("misc.difficulty.debut", null);
                    break;
                case Beatmap.Difficulty.Regular:
                    translation = manager.GetString("misc.difficulty.regular", null);
                    break;
                case Beatmap.Difficulty.Pro:
                    translation = manager.GetString("misc.difficulty.pro", null);
                    break;
                case Beatmap.Difficulty.Master:
                    translation = manager.GetString("misc.difficulty.master", null);
                    break;
                case Beatmap.Difficulty.MasterPlus:
                    translation = manager.GetString("misc.difficulty.master_plus", null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null);
            }
            return translation ?? defaultTranslation;
        }

        public static string NotePosition(LanguageManager manager, NotePosition position) {
            var defaultTranslation = DescribedEnumConverter.GetEnumDescription(position);
            if (manager == null) {
                return defaultTranslation;
            }
            string translation;
            switch (position) {
                case Beatmap.NotePosition.Default:
                    translation = manager.GetString("misc.note_position.default", null);
                    break;
                case Beatmap.NotePosition.P1:
                    translation = manager.GetString("misc.note_position.p1", null);
                    break;
                case Beatmap.NotePosition.P2:
                    translation = manager.GetString("misc.note_position.p2", null);
                    break;
                case Beatmap.NotePosition.P3:
                    translation = manager.GetString("misc.note_position.p3", null);
                    break;
                case Beatmap.NotePosition.P4:
                    translation = manager.GetString("misc.note_position.p4", null);
                    break;
                case Beatmap.NotePosition.P5:
                    translation = manager.GetString("misc.note_position.p5", null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
            return translation ?? defaultTranslation;
        }

    }
}
