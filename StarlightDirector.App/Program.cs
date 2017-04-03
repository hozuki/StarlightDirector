using System;
using System.Windows.Forms;
using StarlightDirector.App.UI.Forms;

namespace StarlightDirector.App {
    internal static class Program {
        
        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FMain());
        }

    }
}
