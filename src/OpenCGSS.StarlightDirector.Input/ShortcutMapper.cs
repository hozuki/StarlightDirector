using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Input {
    public static class ShortcutMapper {

        public static Shortcut Map(Keys keys) {
            return (Shortcut)(int)keys;
        }

        public static Keys Map(Shortcut shortcut) {
            return (Keys)(int)shortcut;
        }

        [NotNull]
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

            string keyName;

            if (Keys.NumPad0 <= keys && keys <= Keys.NumPad9) {
                keyName = (keys - Keys.NumPad0).ToString();
            } else {
                switch (keys) {
                    case Keys.Oemtilde:
                        keyName = "~";
                        break;
                    case Keys.Oemplus:
                        keyName = "=";
                        break;
                    case Keys.OemMinus:
                        keyName = "-";
                        break;
                    default:
                        keyName = Enum.GetName(typeof(Keys), keys);
                        break;
                }
            }

            s += keyName;

            return s;
        }

        [NotNull]
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

        public static Shortcut ParseShortcut([NotNull] string shortcut) {
            Guard.ArgumentNotNull(shortcut, nameof(shortcut));

            var s = shortcut.Replace("+", string.Empty);
            var e = (Shortcut)Enum.Parse(typeof(Shortcut), s);

            return e;
        }

        public static Keys ParseShortcutKeys([NotNull] string shortcut) {
            Guard.ArgumentNotNull(shortcut, nameof(shortcut));

            var segs = shortcut.Split('+');
            var k = Keys.None;

            foreach (var seg in segs) {
                var s = seg;

                if (seg == "Ctrl") {
                    s = "Control";
                } else if (seg == "-") {
                    s = "OemMinus";
                } else if (seg == "=") {
                    // This is a spelling mistake in .NET Framework.
                    s = "Oemplus";
                } else if (int.TryParse(seg, out var num)) {
                    if (0 <= num && num <= 9) {
                        s = "D" + num;
                    } else {
                        throw new ArgumentOutOfRangeException(nameof(num), "Invalid key for shortcut.");
                    }
                }

                var e = (Keys)Enum.Parse(typeof(Keys), s, false);

                k |= e;
            }

            return k;
        }

    }
}
