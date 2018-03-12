//using System.Reflection;
//using JetBrains.Annotations;
//using Xceed.Wpf.AvalonDock;

//namespace OpenCGSS.Director.Shell.Fixes {
//    internal static class AvalonDockFix {

//        [CanBeNull]
//        internal static DockingManager GetDockingManager([CanBeNull] ShellView shellView) {
//            if (shellView == null) {
//                return null;
//            }

//            //var inner = shellView.InnerShell;

//            var managerProperty = shellView.GetType().GetField("Manager", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

//            if (managerProperty == null) {
//                return null;
//            }

//            var dockingManager = (DockingManager)managerProperty.GetValue(shellView);

//            return dockingManager;
//        }

//    }
//}
