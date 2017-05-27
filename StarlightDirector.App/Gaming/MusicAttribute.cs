using System;
using System.ComponentModel;
using StarlightDirector.Beatmap;

namespace StarlightDirector.App.Gaming {
    [Flags]
    public enum MusicAttribute {

        [LocalizationKey("misc.music_attribute.cute")]
        [Description("Cute")]
        Cute = 0x01,
        [LocalizationKey("misc.music_attribute.cool")]
        [Description("Cool")]
        Cool = 0x02,
        [LocalizationKey("misc.music_attribute.passion")]
        [Description("Passion")]
        Passion = 0x04,
        [LocalizationKey("misc.music_attribute.multicolor")]
        [Description("Multicolor")]
        Multicolor = 0x08,
        [LocalizationKey("misc.music_attribute.event")]
        [Description("Event")]
        Event = 0x10,
        [LocalizationKey("misc.music_attribute.solo_ver")]
        [Description("Solo ver.")]
        Solo = 0x20

    }
}
