using System;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Input {
    public interface ICommand {

        event EventHandler<QueryCanExecuteEventArgs> QueryCanExecute;

        event EventHandler<QueryCanRevertEventArgs> QueryCanRevert;

        event EventHandler<QueryRecordToHistoryEventArgs> QueryRecordToHistory;

        event EventHandler<ExecutedEventArgs> Executed;

        event EventHandler<RevertedEventArgs> Reverted;

        void Execute([CanBeNull] object sender, [CanBeNull] object parameter);

        void Revert([CanBeNull] object sender, [CanBeNull] object parameter);

        bool CanExecute { get; }

        bool CanRevert { get; }

        bool RecordToHistory { get; }

    }
}
