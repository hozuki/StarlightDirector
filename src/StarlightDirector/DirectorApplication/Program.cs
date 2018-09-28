using System;
using System.Globalization;
using System.Windows.Forms;
using CommandLine;
using JetBrains.Annotations;
using OpenCGSS.StarlightDirector.UI.Forms;

namespace OpenCGSS.StarlightDirector.DirectorApplication {
    internal static class Program {

        public static StartupOptions StartupOptions { get; private set; }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main([NotNull, ItemNotNull] string[] args) {
            var parser = new Parser(settings => {
                settings.CaseSensitive = false;
                settings.ParsingCulture = CultureInfo.InvariantCulture;
                settings.IgnoreUnknownArguments = true;
            });

            StartupOptions startupOptions;
            var parserResult = parser.ParseArguments<StartupOptions>(args);

            switch (parserResult.Tag) {
                case ParserResultType.Parsed:
                    startupOptions = ((Parsed<StartupOptions>)parserResult).Value;
                    break;
                case ParserResultType.NotParsed:
                    startupOptions = StartupOptions.Default;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            StartupOptions = startupOptions;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FMain());
        }

    }
}
