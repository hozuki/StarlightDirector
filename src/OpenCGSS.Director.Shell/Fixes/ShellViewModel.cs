//using System.ComponentModel.Composition;
//using System.Diagnostics;
//using Gemini.Framework.Services;
//using Gemini.Modules.Shell.Views;

//namespace OpenCGSS.Director.Shell.Fixes {
//    //[Export(typeof(IShell))]
//    public class ShellViewModel : Gemini.Modules.Shell.ViewModels.ShellViewModel {

//        protected override void OnViewLoaded(object view) {
//            Debug.Assert(view is ShellView);

//            var shellView = (ShellView)view;

//            var dockingManager = AvalonDockFix.GetDockingManager(shellView);

//            if (dockingManager != null) {
//                dockingManager.LayoutUpdateStrategy = new HackedLayoutInitializer();
//            }

//            //base.OnViewLoaded(shellView.InnerShell);
//        }

//    }
//}
