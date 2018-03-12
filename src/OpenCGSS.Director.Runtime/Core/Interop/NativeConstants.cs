namespace OpenCGSS.Director.Core.Interop {
    internal static class NativeConstants {

        internal const int WM_NCMOUSEMOVE = 0xa0;
        internal const int WM_NCLBUTTONDOWN = 0xa1;
        internal const int WM_NCLBUTTONUP = 0xa2;
        internal const int WM_NCLBUTTONDBLCLK = 0xa3;
        internal const int WM_NCRBUTTONDOWN = 0xa4;
        internal const int WM_NCRBUTTONUP = 0xa5;
        internal const int WM_NCRBUTTONDBLCLK = 0xa6;
        internal const int WM_NCCALCSIZE = 0x83;
        internal const int WM_NCHITTEST = 0x84;
        internal const int WM_NCPAINT = 0x85;
        internal const int WM_NCACTIVATE = 0x86;
        internal const int WM_COMMAND = 0x111;
        internal const int WM_PRINTCLIENT = 0x318;
        internal const int WM_KEYDOWN = 0x100;
        internal const int WM_KEYUP = 0x101;

        internal const int HTCLIENT = 0x1;
        internal const int HTCAPTION = 0x2;
        internal const int HTSYSMENU = 0x3;
        internal const int HTGROWBOX = 0x4;
        internal const int HTMINBUTTON = 0x8;
        internal const int HTMAXBUTTON = 0x9;
        internal const int HTLEFT = 0xa;
        internal const int HTRIGHT = 0xb;
        internal const int HTTOP = 0xc;
        internal const int HTTOPLEFT = 0xd;
        internal const int HTTOPRIGHT = 0xe;
        internal const int HTBOTTOM = 0xf;
        internal const int HTBOTTOMLEFT = 0x10;
        internal const int HTBOTTOMRIGHT = 0x11;
        internal const int HTCLOSE = 0x14;
        internal const int HTTRANSPARENT = -1;

        internal const int SC_MINIMIZE = 0xF020;
        internal const int SC_MAXIMIZE = 0xF030;
        internal const int SC_CLOSE = 0xf060;
        internal const int SC_RESTORE = 0xF120;

        internal const uint TPM_LEFTBUTTON = 0;

        /// <summary>
        /// Passed to <see cref="NativeMethods.GetTokenInformation"/> to specify what
        /// information about the token to return.
        /// </summary>
        internal enum TokenInformationClass {
            None,
            TokenUser,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin,
            TokenElevationType,
            TokenLinkedToken,
            TokenElevation,
            TokenHasRestrictions,
            TokenAccessInformation,
            TokenVirtualizationAllowed,
            TokenVirtualizationEnabled,
            TokenIntegrityLevel,
            TokenUiAccess,
            TokenMandatoryPolicy,
            TokenLogonSid,
            MaxTokenInfoClass
        }

        /// <summary>
        /// The elevation type for a user token.
        /// </summary>
        internal enum TokenElevationType {
            None,
            TokenElevationTypeDefault,
            TokenElevationTypeFull,
            TokenElevationTypeLimited
        }

    }
}
