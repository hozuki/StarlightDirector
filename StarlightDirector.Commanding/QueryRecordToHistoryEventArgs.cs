using System;

namespace StarlightDirector.Commanding {
    public sealed class QueryRecordToHistoryEventArgs : EventArgs {

        public bool RecordToHistory { get; set; }

    }
}
