using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using StarlightDirector.Core;

namespace StarlightDirector.Commanding {
    public sealed class Command : DisposableBase {

        public event EventHandler<QueryCanExecuteEventArgs> QueryCanExecute;
        public event EventHandler<QueryCanRevertEventArgs> QueryCanRevert;
        public event EventHandler<QueryRecordToHistoryEventArgs> QueryRecordToHistory;
        public event EventHandler<ExecutedEventArgs> Executed;
        public event EventHandler<RevertedEventArgs> Reverted;

        internal Command() {
        }

        public Keys ShortcutKeys { get; internal set; }

        public void Execute(object sender, object parameter) {
            if (!CanExecute) {
                return;
            }
            OnExecuted(sender, new ExecutedEventArgs(parameter));
        }

        public void Revert(object sender, object parameter) {
            if (!CanRevert) {
                throw new InvalidOperationException("This command cannot revert.");
            }
            OnReverted(sender, new RevertedEventArgs(parameter));
        }

        public bool CanExecute { get; private set; } = true;

        public bool CanRevert { get; private set; } = true;

        public bool RecordToHistory { get; private set; } = true;

        public string Description { get; set; } = string.Empty;

        internal void SubscribeControl(Component control, bool setShortcut) {
            if (control == null) {
                return;
            }
            if (_subscribedControls.Contains(control)) {
                return;
            }
            var canExecute = CanExecute;
            switch (control) {
                case ButtonBase button:
                    button.Click += OnControlInteract;
                    button.Enabled = canExecute;
                    break;
                case MenuItem menuItem:
                    menuItem.Click += OnControlInteract;
                    menuItem.Enabled = canExecute;
                    if (setShortcut && ShortcutKeys != Keys.None) {
                        menuItem.Shortcut = ShortcutMapper.Map(ShortcutKeys);
                    }
                    break;
                case ToolStripButton button:
                    button.Click += OnControlInteract;
                    button.Enabled = canExecute;
                    if (setShortcut && ShortcutKeys != Keys.None) {
                        button.AutoToolTip = false;
                        button.ToolTipText = $"{button.Text} ({ShortcutMapper.GetDescription(ShortcutKeys)})";
                    }
                    break;
                case ToolStripSplitButton button:
                    button.ButtonClick += OnControlInteract;
                    button.Enabled = canExecute;
                    if (setShortcut && ShortcutKeys != Keys.None) {
                        button.AutoToolTip = false;
                        button.ToolTipText = $"{button.Text} ({ShortcutMapper.GetDescription(ShortcutKeys)})";
                    }
                    break;
                case ToolStripOverflowButton button:
                    button.Click += OnControlInteract;
                    button.Enabled = canExecute;
                    if (setShortcut && ShortcutKeys != Keys.None) {
                        button.AutoToolTip = false;
                        button.ToolTipText = $"{button.Text} ({ShortcutMapper.GetDescription(ShortcutKeys)})";
                    }
                    break;
                case ToolStripMenuItem menuItem:
                    menuItem.Click += OnControlInteract;
                    menuItem.Enabled = canExecute;
                    if (setShortcut && ShortcutKeys != Keys.None) {
                        menuItem.ShortcutKeys = ShortcutKeys;
                        menuItem.ShortcutKeyDisplayString = ShortcutMapper.GetDescription(ShortcutKeys);
                    }
                    break;
                default:
                    throw new NotSupportedException($"The type of control ({control.GetType().Name}) is not supported.");
            }
            _subscribedControls.Add(control);
            _setShortcuts[control] = setShortcut;
        }

        internal void UnsubscribeControl(Component control) {
            if (control == null) {
                return;
            }
            if (!_subscribedControls.Contains(control)) {
                return;
            }
            var setShortcut = _setShortcuts.ContainsKey(control) && _setShortcuts[control];
            switch (control) {
                case ButtonBase button:
                    button.Click -= OnControlInteract;
                    break;
                case MenuItem menuItem:
                    menuItem.Click -= OnControlInteract;
                    if (setShortcut && ShortcutKeys != Keys.None) {
                        menuItem.Shortcut = Shortcut.None;
                    }
                    break;
                case ToolStripButton button:
                    button.Click -= OnControlInteract;
                    if (setShortcut && ShortcutKeys != Keys.None) {
                        button.ToolTipText = string.Empty;
                        button.AutoToolTip = true;
                    }
                    break;
                case ToolStripSplitButton button:
                    button.ButtonClick -= OnControlInteract;
                    if (setShortcut && ShortcutKeys != Keys.None) {
                        button.ToolTipText = string.Empty;
                        button.AutoToolTip = true;
                    }
                    break;
                case ToolStripOverflowButton button:
                    button.Click -= OnControlInteract;
                    if (setShortcut && ShortcutKeys != Keys.None) {
                        button.ToolTipText = string.Empty;
                        button.AutoToolTip = true;
                    }
                    break;
                case ToolStripMenuItem menuItem:
                    menuItem.Click -= OnControlInteract;
                    if (setShortcut && ShortcutKeys != Keys.None) {
                        menuItem.ShortcutKeys = Keys.None;
                        menuItem.ShortcutKeyDisplayString = string.Empty;
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }
            _subscribedControls.Remove(control);
            _setShortcuts.Remove(control);
        }

        internal void UnsubscribeAllControls() {
            var controls = _subscribedControls.ToArray();
            foreach (var control in controls) {
                UnsubscribeControl(control);
            }
        }

        internal void UpdateCanExecute() {
            var e = new QueryCanExecuteEventArgs();
            OnQueryCanExecute(this, e);

            var canExecute = e.CanExecute;
            CanExecute = canExecute;

            foreach (var control in _subscribedControls) {
                switch (control) {
                    case ButtonBase button:
                        button.Enabled = canExecute;
                        break;
                    case MenuItem menuItem:
                        menuItem.Enabled = canExecute;
                        break;
                    case ToolStripButton button:
                        button.Enabled = canExecute;
                        break;
                    case ToolStripSplitButton button:
                        button.Enabled = canExecute;
                        break;
                    case ToolStripOverflowButton button:
                        button.Enabled = canExecute;
                        break;
                    case ToolStripMenuItem menuItem:
                        menuItem.Enabled = canExecute;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        internal void UpdateCanRevert() {
            var e = new QueryCanRevertEventArgs();
            OnQueryCanRevert(this, e);
            CanRevert = e.CanRevert;
        }

        internal void UpdateRecordToHistory() {
            var e = new QueryRecordToHistoryEventArgs();
            OnQueryRecordToHistory(this, e);
            RecordToHistory = e.RecordToHistory;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                UnsubscribeAllControls();
            }
        }

        private void OnQueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            QueryCanExecute?.Invoke(sender, e);
        }

        private void OnQueryCanRevert(object sender, QueryCanRevertEventArgs e) {
            QueryCanRevert?.Invoke(sender, e);
        }

        private void OnQueryRecordToHistory(object sender, QueryRecordToHistoryEventArgs e) {
            QueryRecordToHistory?.Invoke(sender, e);
        }

        private void OnControlInteract(object sender, EventArgs e) {
            var comp = sender as Component;
            if (comp == null) {
                throw new InvalidCastException("Activation listening is only available on subclasses of Component.");
            }
            var param = comp.GetParameter();
            Execute(sender, param);
        }

        private void OnExecuted(object sender, ExecutedEventArgs e) {
            Executed?.Invoke(sender, e);
            CommandManager.UpdateCommandListStatus();
        }

        private void OnReverted(object sender, RevertedEventArgs e) {
            Reverted?.Invoke(sender, e);
            CommandManager.UpdateCommandListStatus();
        }

        private readonly List<Component> _subscribedControls = new List<Component>();
        private readonly Dictionary<Component, bool> _setShortcuts = new Dictionary<Component, bool>();

    }
}
