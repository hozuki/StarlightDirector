using System;
using System.Windows.Forms;
using OpenCGSS.StarlightDirector.UI.Forms;

namespace OpenCGSS.StarlightDirector.DirectorApplication {
    internal static class Program {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FMain());
        }

    }
}
