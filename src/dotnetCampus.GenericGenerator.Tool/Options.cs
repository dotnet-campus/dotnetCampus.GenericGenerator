using dotnetCampus.Cli;

namespace dotnetCampus.Runtime.CompilerServices
{
    internal class Options
    {
        [Option("ArgumentsFile")]
        public string? ArgumentsFile { get; set; }

        [Option("GeneratedSourceDirectory")]
        public string? GeneratedSourceDirectory { get; set; }

        [Option("Debug")]
        public bool OpenDebugger { get; set; }
    }
}
