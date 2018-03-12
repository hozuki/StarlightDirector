using System.Windows;
using System.Windows.Controls;

namespace OpenCGSS.Director.Shell.Fixes {
    // http://svetoslavsavov.blogspot.nl/2009/09/user-control-inheritance-in-wpf.html
    // https://havefuncoding.wordpress.com/2014/10/30/inheritance-of-usercontrol-wpf/
    public partial class TrickeryControl : UserControl {

        static TrickeryControl() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TrickeryControl), new FrameworkPropertyMetadata(typeof(TrickeryControl)));
        }

    }
}
