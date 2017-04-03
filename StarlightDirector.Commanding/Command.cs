using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using StarlightDirector.Core;

namespace StarlightDirector.Commanding {
    public sealed class Command : DisposableBase {

        public event QueryCanExecuteEventHandler QueryCanExecute;
        public event QueryCanRevertEventHandler QueryCanRevert;
        public event QueryRecordToHistoryEventHandler QueryRecordToHistory;
        public event EventHandler<EventArgs> Executed;
        public event EventHandler<EventArgs> Reverted;

        internal Command() {
        }

        public void Execute(object sender) {
            if (!CanExecute) {
                return;
            }
            OnExecuted(sender, EventArgs.Empty);
        }

        public void Revert(object sender) {
            if (!CanRevert) {
                throw new InvalidOperationException("This command cannot revert.");
            }
            OnReverted(sender, EventArgs.Empty);
        }

        public bool CanExecute { get; private set; } = true;

        public bool CanRevert { get; private set; } = true;

        public bool RecordToHistory { get; private set; } = true;

        public string Description { get; set; } = string.Empty;

        internal void SubscribeControl(Component control) {
            if (control == null) {
                return;
            }
            if (_subscribedControls.Contains(control)) {
                return;
            }
            var canExecute = CanExecute;
            switch (control) {
                case ButtonBase button:
                    button.Click += OnExecuted;
                    button.Enabled = canExecute;
                    break;
                case MenuItem menuItem:
                    menuItem.Click += OnExecuted;
                    menuItem.Enabled = canExecute;
                    break;
                case ToolStripItem toolStripItem:
                    toolStripItem.Click += OnExecuted;
                    toolStripItem.Enabled = canExecute;
                    break;
                default:
                    throw new NotSupportedException();
            }
            _subscribedControls.Add(control);
        }

        internal void UnsubscribeControl(Component control) {
            if (control == null) {
                return;
            }
            if (!_subscribedControls.Contains(control)) {
                return;
            }
            switch (control) {
                case ButtonBase button:
                    button.Click -= OnExecuted;
                    break;
                case MenuItem menuItem:
                    menuItem.Click -= OnExecuted;
                    break;
                case ToolStripItem toolStripItem:
                    toolStripItem.Click -= OnExecuted;
                    break;
                default:
                    throw new NotSupportedException();
            }
            _subscribedControls.Remove(control);
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
                    case ToolStripItem toolStripItem:
                        toolStripItem.Enabled = canExecute;
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

        private void OnExecuted(object sender, EventArgs e) {
            Executed?.Invoke(sender, e);
            CommandManager.UpdateCommandListStatus();
        }

        private void OnReverted(object sender, EventArgs e) {
            Reverted?.Invoke(sender, e);
            CommandManager.UpdateCommandListStatus();
        }

        private readonly List<Component> _subscribedControls = new List<Component>();

    }
}
