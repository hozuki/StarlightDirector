#region File Description
//-----------------------------------------------------------------------------
// Copyright 2011, Nick Gravelyn.
// Licensed under the terms of the Ms-PL: 
// http://www.microsoft.com/opensource/licenses.mspx#Ms-PL
//-----------------------------------------------------------------------------
#endregion

using System;
using JetBrains.Annotations;
using Microsoft.Xna.Framework.Graphics;

namespace OpenCGSS.Director.Modules.MonoGame.Controls {
    /// <summary>
    /// Arguments used for Device related events.
    /// </summary>
    public class GraphicsDeviceEventArgs : EventArgs {

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new GraphicsDeviceEventArgs.
        /// </summary>
        /// <param name="graphicsDevice">The GraphicsDevice associated with the event.</param>
        public GraphicsDeviceEventArgs([NotNull] GraphicsDevice graphicsDevice) {
            GraphicsDevice = graphicsDevice;
        }

        /// <summary>
        /// Gets the GraphicsDevice.
        /// </summary>
        [NotNull]
        public GraphicsDevice GraphicsDevice { get; }

    }
}
