using CommandLine;

namespace OpenCGSS.StarlightDirector.DirectorApplication {
    internal sealed class StartupOptions {

        [Option("--enable-bvsp", Default = false, Required = false, HelpText = "Enable BVSP communication (client/server)")]
        public bool BvspCommunicationEnabled { get; set; }

        public static readonly StartupOptions Default = new StartupOptions {
            BvspCommunicationEnabled = false
        };

    }
}
