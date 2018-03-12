namespace StarlightDirector.Core.Interop {
    public static class NativeConstants {

        public const int WM_NCMOUSEMOVE = 0xa0;
        public const int WM_NCLBUTTONDOWN = 0xa1;
        public const int WM_NCLBUTTONUP = 0xa2;
        public const int WM_NCLBUTTONDBLCLK = 0xa3;
        public const int WM_NCRBUTTONDOWN = 0xa4;
        public const int WM_NCRBUTTONUP = 0xa5;
        public const int WM_NCRBUTTONDBLCLK = 0xa6;
        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_NCHITTEST = 0x84;
        public const int WM_NCPAINT = 0x85;
        public const int WM_NCACTIVATE = 0x86;
        public const int WM_COMMAND = 0x111;
        public const int WM_PRINTCLIENT = 0x318;

        public const int HTCLIENT = 0x1;
        public const int HTCAPTION = 0x2;
        public const int HTSYSMENU = 0x3;
        public const int HTGROWBOX = 0x4;
        public const int HTMINBUTTON = 0x8;
        public const int HTMAXBUTTON = 0x9;
        public const int HTLEFT = 0xa;
        public const int HTRIGHT = 0xb;
        public const int HTTOP = 0xc;
        public const int HTTOPLEFT = 0xd;
        public const int HTTOPRIGHT = 0xe;
        public const int HTBOTTOM = 0xf;
        public const int HTBOTTOMLEFT = 0x10;
        public const int HTBOTTOMRIGHT = 0x11;
        public const int HTCLOSE = 0x14;
        public const int HTTRANSPARENT = -1;

        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_CLOSE = 0xf060;
        public const int SC_RESTORE = 0xF120;

        public const uint TPM_LEFTBUTTON = 0;

        /// <summary>
        /// Passed to <see cref="NativeMethods.GetTokenInformation"/> to specify what
        /// information about the token to return.
        /// </summary>
        public enum TokenInformationClass {
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
        public enum TokenElevationType {
            None,
            TokenElevationTypeDefault,
            TokenElevationTypeFull,
            TokenElevationTypeLimited
        }

    }
}
