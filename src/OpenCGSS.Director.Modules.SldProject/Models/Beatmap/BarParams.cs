using System;
using JetBrains.Annotations;

namespace OpenCGSS.Director.Modules.SldProject.Models.Beatmap {
    public sealed class BarParams {

        [Obsolete("This property is ignored since v0.5.0 alpha. Please consider Note with Note.Type == VariantBpm instead.")]
        [CanBeNull]
        public double? UserDefinedBpm { get; internal set; }

        [CanBeNull]
        public int? UserDefinedGridPerSignature { get; internal set; }

        [CanBeNull]
        public int? UserDefinedSignature { get; internal set; }

#pragma warning disable 618
        internal bool CanBeSquashed => UserDefinedBpm == null && UserDefinedGridPerSignature == null && UserDefinedSignature == null;
#pragma warning restore 618

    }
}
