using System;
using System.Runtime.CompilerServices;
using System.Windows;
using JetBrains.Annotations;

namespace OpenCGSS.Director.Common.Extensions {
    public static class WeakEventExtensions {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddWeakHandler<TSender, TEventArgs>([NotNull] this TSender sender, [NotNull] string eventName, [NotNull] EventHandler<TEventArgs> handler)
            where TSender : class
            where TEventArgs : EventArgs {
            WeakEventManager<TSender, TEventArgs>.AddHandler(sender, eventName, handler);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveWeakHandler<TSender, TEventArgs>([NotNull] this TSender sender, [NotNull] string eventName, [NotNull] EventHandler<TEventArgs> handler)
            where TSender : class
            where TEventArgs : EventArgs {
            WeakEventManager<TSender, TEventArgs>.RemoveHandler(sender, eventName, handler);
        }

    }
}
