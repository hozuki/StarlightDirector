using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using StarlightDirector.Core.Interop;

namespace StarlightDirector.Core {
    public static class SystemHelper {

        /// <summary>
        /// http://stackoverflow.com/a/6405799
        /// </summary>
        /// <returns></returns>
        public static bool IsUserElevated() {
            var identity = WindowsIdentity.GetCurrent();
            if (identity == null) {
                throw new InvalidOperationException("Couldn't get the current user identity.");
            }
            var principal = new WindowsPrincipal(identity);

            // Check if this user has the Administrator role. If they do, return immediately.
            // If UAC is on, and the process is not elevated, then this will actually return false.
            if (principal.IsInRole(WindowsBuiltInRole.Administrator)) {
                return true;
            }

            // If we're not running in Vista onwards, we don't have to worry about checking for UAC.
            if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 6) {
                // Operating system does not support UAC; skipping elevation check.
                return false;
            }

            var tokenInfLength = Marshal.SizeOf(typeof(int));
            var tokenInformation = Marshal.AllocHGlobal(tokenInfLength);

            try {
                var token = identity.Token;
                var result = NativeMethods.GetTokenInformation(token, NativeConstants.TokenInformationClass.TokenElevationType, tokenInformation, tokenInfLength, out tokenInfLength);

                if (!result) {
                    var exception = Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
                    throw new InvalidOperationException("Couldn't get token information.", exception);
                }

                var elevationType = (NativeConstants.TokenElevationType)Marshal.ReadInt32(tokenInformation);

                switch (elevationType) {
                    case NativeConstants.TokenElevationType.TokenElevationTypeDefault:
                        // TokenElevationTypeDefault - User is not using a split token, so they cannot elevate.
                        return false;
                    case NativeConstants.TokenElevationType.TokenElevationTypeFull:
                        // TokenElevationTypeFull - User has a split token, and the process is running elevated. Assuming they're an administrator.
                        return true;
                    case NativeConstants.TokenElevationType.TokenElevationTypeLimited:
                        // TokenElevationTypeLimited - User has a split token, but the process is not running elevated. Assuming they're an administrator.
                        return true;
                    default:
                        // Unknown token elevation type.
                        return false;
                }
            } finally {
                if (tokenInformation != IntPtr.Zero) {
                    Marshal.FreeHGlobal(tokenInformation);
                }
            }
        }

    }
}
