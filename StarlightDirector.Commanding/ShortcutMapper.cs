using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StarlightDirector.Commanding {
    public static class ShortcutMapper {

        public static Shortcut Map(Keys keys) {
            return (Shortcut)(int)keys;
        }

        public static Keys Map(Shortcut shortcut) {
            return (Keys)(int)shortcut;
        }

        public static string GetDescription(Keys keys) {
            var s = string.Empty;
            if ((keys & Keys.Control) != 0) {
                keys &= ~Keys.Control;
                s += "Ctrl+";
            }
            if ((keys & Keys.Shift) != 0) {
                keys &= ~Keys.Shift;
                s += "Shift+";
            }
            if ((keys & Keys.Alt) != 0) {
                keys &= ~Keys.Alt;
                s += "Alt+";
            }
            var raw = Enum.GetName(typeof(Keys), keys);
            s += raw;
            return s;
        }

        public static string GetDescription(Shortcut shortcut) {
            if (shortcut == Shortcut.None) {
                return string.Empty;
            }

            var enumName = Enum.GetName(typeof(Shortcut), shortcut);
            if (string.IsNullOrEmpty(enumName)) {
                return string.Empty;
            }
            var segs = new List<string>();
            var n = 0;
            for (var i = 0; i < enumName.Length; ++i) {
                if (i == 0) {
                    continue;
                }
                if (char.IsNumber(enumName, i)) {
                    if (char.IsNumber(enumName, i - 1) || enumName[i - 1] == 'F') {
                        continue;
                    } else {
                        segs.Add(enumName.Substring(n, i - n));
                        n = i;
                        continue;
                    }
                }
                if (char.IsUpper(enumName, i)) {
                    segs.Add(enumName.Substring(n, i - n));
                    n = i;
                }
            }
            segs.Add(enumName.Substring(n, enumName.Length - n));
            return segs.Aggregate((f, v) => f == null ? v : (v == "Arrow" ? f + v : f + '+' + v));
        }

        public static Shortcut ParseShortcut(string shortcut) {
            var s = shortcut.Replace("+", string.Empty);
            var e = (Shortcut)Enum.Parse(typeof(Shortcut), s);
            return e;
        }

        public static Keys ParseShortcutKeys(string shortcut) {
            var segs = shortcut.Split('+');
            var k = Keys.None;
            foreach (var seg in segs) {
                var s = seg;
                if (string.Equals("Ctrl", seg, StringComparison.InvariantCultureIgnoreCase)) {
                    s = "Control";
                }
                var e = (Keys)Enum.Parse(typeof(Keys), s, false);
                k |= e;
            }
            return k;
        }

    }
}
