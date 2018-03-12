//using System;
//using System.Linq;
//using System.Windows;
//using System.Windows.Controls;
//using Gemini.Framework;
//using Gemini.Framework.Services;
//using Xceed.Wpf.AvalonDock.Layout;

//namespace OpenCGSS.Director.Shell.Fixes {
//    public class HackedLayoutInitializer : ILayoutUpdateStrategy {

//        public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow, ILayoutContainer destinationContainer) {
//            if (!(anchorableToShow.Content is ITool tool)) {
//                return false;
//            }

//            var preferredLocation = tool.PreferredLocation;
//            var paneName = GetPaneName(preferredLocation);
//            var toolsPane = layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault(d => d.Name == paneName);

//            if (toolsPane == null) {
//                switch (preferredLocation) {
//                    case PaneLocation.Left:
//                        toolsPane = CreateAnchorablePane(layout, Orientation.Horizontal, paneName, InsertPosition.Start);
//                        break;
//                    case PaneLocation.Right:
//                        toolsPane = CreateAnchorablePane(layout, Orientation.Horizontal, paneName, InsertPosition.End);
//                        break;
//                    case PaneLocation.Bottom:
//                        toolsPane = CreateAnchorablePane(layout, Orientation.Vertical, paneName, InsertPosition.End);
//                        break;
//                    default:
//                        throw new ArgumentOutOfRangeException();
//                }
//            }

//            toolsPane.Children.Add(anchorableToShow);

//            return true;
//        }

//        private static string GetPaneName(PaneLocation location) {
//            switch (location) {
//                case PaneLocation.Left:
//                    return "LeftPane";
//                case PaneLocation.Right:
//                    return "RightPane";
//                case PaneLocation.Bottom:
//                    return "BottomPane";
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(location));
//            }
//        }

//        private static LayoutAnchorablePane CreateAnchorablePane(LayoutRoot layout, Orientation orientation,
//            string paneName, InsertPosition position) {
//            var parent = layout.Descendents().OfType<LayoutPanel>().FirstOrDefault(d => d.Orientation == orientation);
//            var toolsPane = new LayoutAnchorablePane { Name = paneName };

//            // https://github.com/tgjones/gemini/issues/281
//            if (parent == null) {
//                var newLayoutPanel = new LayoutPanel { Orientation = orientation };

//                if (layout.RootPanel != null) {
//                    newLayoutPanel.Children.Add(layout.RootPanel);
//                }

//                layout.RootPanel = newLayoutPanel;
//                parent = newLayoutPanel;
//            }

//            if (position == InsertPosition.Start) {
//                parent.InsertChildAt(0, toolsPane);
//            } else {
//                parent.Children.Add(toolsPane);
//            }

//            return toolsPane;
//        }

//        private enum InsertPosition {
//            Start,
//            End
//        }

//        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown) {
//            // If this is the first anchorable added to this pane, then use the preferred size.
//            if (!(anchorableShown.Content is ITool tool)) {
//                return;
//            }

//            if (!(anchorableShown.Parent is LayoutAnchorablePane anchorablePane) || anchorablePane.ChildrenCount != 1) {
//                return;
//            }

//            switch (tool.PreferredLocation) {
//                case PaneLocation.Left:
//                case PaneLocation.Right:
//                    anchorablePane.DockWidth = new GridLength(tool.PreferredWidth, GridUnitType.Pixel);
//                    break;
//                case PaneLocation.Bottom:
//                    anchorablePane.DockHeight = new GridLength(tool.PreferredHeight, GridUnitType.Pixel);
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException();
//            }
//        }

//        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer) {
//            return false;
//        }

//        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown) {
//        }

//    }
//}
