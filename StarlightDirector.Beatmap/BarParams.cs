using System;

namespace StarlightDirector.Beatmap {
    public sealed class BarParams {

        [Obsolete("This property is ignored since v0.5.0 alpha. Please consider Note with Note.Type == VariantBpm instead.")]
        public double? UserDefinedBpm { get; internal set; }

        public int? UserDefinedGridPerSignature { get; internal set; }

        public int? UserDefinedSignature { get; internal set; }

        internal bool CanBeSquashed => UserDefinedBpm == null && UserDefinedGridPerSignature == null && UserDefinedSignature == null;

    }
}
