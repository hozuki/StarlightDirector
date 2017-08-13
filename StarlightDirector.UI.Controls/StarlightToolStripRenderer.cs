using System.Drawing;
using System.Windows.Forms;
using StarlightDirector.UI.Controls.Extensions;

namespace StarlightDirector.UI.Controls {
    public sealed class StarlightToolStripRenderer : ToolStripRenderer {

        public StarlightToolStripRenderer(ColorScheme colorScheme) {
            _scheme = colorScheme;
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e) {
            var scheme = _scheme;
            Color backColor;
            if (e.ToolStrip is MenuStrip) {
                backColor = scheme.MenuBarBackground;
            } else if (e.ToolStrip is ToolStripDropDownMenu) {
                backColor = scheme.MenuItemBackground;
            } else {
                backColor = scheme.ToolbarBackground;
            }
            e.Graphics.FillRectangle(backColor, e.AffectedBounds);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e) {
            var scheme = _scheme;
            Color backColor;
            if (e.ToolStrip is MenuStrip) {
                backColor = scheme.MenuBarBackground;
            } else if (e.ToolStrip is ToolStripDropDownMenu) {
                backColor = scheme.MenuItemBackground;
            } else {
                backColor = scheme.ToolbarItemBackground;
            }
            e.Graphics.FillRectangle(backColor, e.AffectedBounds);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e) {
            var scheme = _scheme;
            var g = e.Graphics;
            var item = e.Item;
            if (item.Enabled) {
                if (e.Item.Pressed) {
                    item.ForeColor = scheme.ToolbarItemPressedText;
                    g.Clear(scheme.ToolbarItemPressedBackground);
                } else if (item.Selected) {
                    item.ForeColor = scheme.ToolbarItemHoveringText;
                    g.Clear(scheme.ToolbarItemHoveringBackground);
                } else if (item.SafeGetChecked()) {
                    item.ForeColor = scheme.ToolbarItemPressedText;
                    g.Clear(scheme.ToolbarItemPressedBackground);
                } else {
                    item.ForeColor = scheme.ToolbarItemText;
                    g.Clear(scheme.ToolbarItemBackground);
                }
            } else {
                Color backColor;
                if (e.ToolStrip is MenuStrip) {
                    backColor = scheme.MenuBarBackground;
                } else if (e.ToolStrip is ToolStripDropDownMenu) {
                    backColor = scheme.MenuItemDisabledBackground;
                } else {
                    backColor = scheme.ToolbarItemDisabledBackground;
                }
                item.ForeColor = scheme.ToolbarItemDisabledText;
                g.Clear(backColor);
            }
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e) {
            var scheme = _scheme;
            var g = e.Graphics;
            var item = e.Item;
            if (item.Enabled) {
                if (e.Item.Pressed) {
                    item.ForeColor = scheme.ToolbarItemPressedText;
                    g.Clear(scheme.ToolbarItemPressedBackground);
                } else if (item.Selected) {
                    item.ForeColor = scheme.ToolbarItemHoveringText;
                    g.Clear(scheme.ToolbarItemHoveringBackground);
                } else {
                    item.ForeColor = scheme.ToolbarItemText;
                    g.Clear(scheme.ToolbarItemBackground);
                }
            } else {
                Color backColor;
                if (e.ToolStrip is MenuStrip) {
                    backColor = scheme.MenuBarBackground;
                } else if (e.ToolStrip is ToolStripDropDownMenu) {
                    backColor = scheme.MenuItemDisabledBackground;
                } else {
                    backColor = scheme.ToolbarItemDisabledBackground;
                }
                item.ForeColor = scheme.ToolbarItemDisabledText;
                g.Clear(backColor);
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
            var scheme = _scheme;
            var g = e.Graphics;
            var item = e.Item;
            var innerRect = new Rectangle(2, 0, item.Width - 3, item.Height);
            if (!(e.ToolStrip is MenuStrip)) {
                g.Clear(scheme.MenuItemBackground);
            }
            if (item.Enabled) {
                if (item.Pressed) {
                    if (e.ToolStrip is MenuStrip) {
                        item.ForeColor = scheme.MenuBarPressedText;
                        g.Clear(scheme.MenuBarPressedBackground);
                        using (var p = new Pen(scheme.ToolStripBorder, 1)) {
                            var rect = new Rectangle(0, 0, item.Width - 1, item.Height - 1);
                            g.DrawRectangle(p, rect);
                        }
                    } else {
                        item.ForeColor = scheme.MenuItemPressedText;
                        g.FillRectangle(scheme.MenuItemPressedBackground, innerRect);

                    }
                } else if (item.Selected) {
                    if (e.ToolStrip is MenuStrip) {
                        item.ForeColor = scheme.MenuBarHoveringText;
                        g.Clear(scheme.MenuBarHoveringBackground);
                    } else {
                        item.ForeColor = scheme.MenuItemHoveringText;
                        g.FillRectangle(scheme.MenuItemHoveringBackground, innerRect);
                    }
                } else {
                    if (e.ToolStrip is MenuStrip) {
                        item.ForeColor = scheme.MenuBarText;
                        g.Clear(scheme.MenuBarBackground);
                    } else {
                        item.ForeColor = scheme.MenuItemText;
                        g.FillRectangle(scheme.MenuItemBackground, innerRect);
                    }
                }
            } else {
                if (e.ToolStrip is MenuStrip) {
                    item.ForeColor = scheme.MenuItemDisabledText;
                    g.Clear(scheme.MenuBarBackground);
                } else {
                    item.ForeColor = scheme.MenuItemDisabledText;
                    g.Clear(scheme.MenuItemDisabledBackground);
                }
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) {
            var scheme = _scheme;
            if (e.ToolStrip is ToolStripDropDownMenu) {
                using (var p = new Pen(scheme.ToolStripBorder, 2)) {
                    e.Graphics.DrawRectangle(p, e.AffectedBounds);
                }
                if (e.ConnectedArea != Rectangle.Empty) {
                    e.Graphics.FillRectangle(scheme.MenuItemBackground, e.ConnectedArea);
                }
            }
        }

        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e) {
            var scheme = _scheme;
            e.Graphics.Clear(scheme.MenuItemBackground);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) {
            var g = e.Graphics;
            var scheme = _scheme;
            var item = e.Item;
            Color backColor;
            if (e.ToolStrip is MenuStrip) {
                backColor = scheme.MenuBarBackground;
            } else if (e.ToolStrip is ToolStripDropDownMenu) {
                backColor = scheme.MenuItemBackground;
            } else {
                backColor = scheme.ToolbarItemBackground;
            }
            g.Clear(backColor);
            if (e.Vertical) {
                g.DrawLine(scheme.Separator, (item.Width - 2) / 2, 2, (item.Width - 2) / 2, item.Height - 4);
            } else {
                g.DrawLine(scheme.Separator, 2, (item.Height - 2) / 2, item.Width - 4, (item.Height - 2) / 2);
            }
        }

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e) {
            var scheme = _scheme;
            var g = e.Graphics;
            var item = (ToolStripSplitButton)e.Item;
            var itemBounds = item.Bounds;
            var itemButtonBounds = item.ButtonBounds;
            var buttonRect = new Rectangle(itemButtonBounds.Left, itemBounds.Top, item.Width - item.DropDownButtonWidth, itemBounds.Height);
            var arrowRect = new Rectangle(item.Width - item.DropDownButtonWidth, itemBounds.Top, item.DropDownButtonWidth, itemBounds.Height);

            if (item.Enabled) {
                // Standard background.
                Color standardButtonBackground;
                if (item.ButtonPressed) {
                    item.ForeColor = scheme.ToolbarItemPressedText;
                    standardButtonBackground = scheme.ToolbarItemPressedBackground;
                } else if (item.ButtonSelected) {
                    item.ForeColor = scheme.ToolbarItemHoveringText;
                    standardButtonBackground = scheme.ToolbarItemHoveringBackground;
                } else {
                    item.ForeColor = scheme.ToolbarItemText;
                    standardButtonBackground = scheme.ToolbarItemBackground;
                }
                g.FillRectangle(standardButtonBackground, buttonRect);

                // Draw drop down button background.
                Color dropDownButtonBackground;
                if (item.DropDownButtonPressed) {
                    dropDownButtonBackground = scheme.ToolbarItemPressedBackground;
                } else if (item.DropDownButtonSelected) {
                    dropDownButtonBackground = scheme.ToolbarItemHoveringBackground;
                } else {
                    dropDownButtonBackground = scheme.ToolbarItemBackground;
                }
                g.FillRectangle(dropDownButtonBackground, arrowRect);
            } else {
                Color backColor;
                if (e.ToolStrip is MenuStrip) {
                    backColor = scheme.MenuBarBackground;
                } else if (e.ToolStrip is ToolStripDropDownMenu) {
                    backColor = scheme.MenuItemDisabledBackground;
                } else {
                    backColor = scheme.ToolbarItemDisabledBackground;
                }
                item.ForeColor = scheme.ToolbarItemDisabledText;
                g.Clear(backColor);
            }

            // Draw separator.
            var splitterRect = item.SplitterBounds;
            splitterRect.Inflate(0, -2);
            splitterRect.Offset(0, 1);
            g.FillRectangle(scheme.Separator, splitterRect);

            // Draw arrow.
            var arrowEventArgs = new ToolStripArrowRenderEventArgs(e.Graphics, e.Item, arrowRect, item.ForeColor, ArrowDirection.Down);
            DrawArrow(arrowEventArgs);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e) {
            var item = e.Item as ToolStripMenuItem;
            if (item?.Image == null || !item.Checked) {
                base.OnRenderItemCheck(e);
            } else {
                var scheme = _scheme;
                var g = e.Graphics;
                var selectRect = e.ImageRectangle;
                selectRect.X -= 2;
                selectRect.Y -= 2;
                selectRect.Width += 3;
                selectRect.Height += 3;
                g.FillRectangle(scheme.MenuItemCheckBoxBackground, selectRect);
                g.DrawRectangle(scheme.MenuItemCheckBoxBorder, selectRect);
            }
        }

        private readonly ColorScheme _scheme;

    }
}
