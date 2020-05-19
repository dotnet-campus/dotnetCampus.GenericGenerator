using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnetCampus.Cli;
using dotnetCampus.Configurations.Core;

namespace dotnetCampus.Runtime.CompilerServices
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var options = CommandLine.Parse(args).As<Options>();
                if (options.OpenDebugger && !Debugger.IsAttached)
                {
                    Debugger.Launch();
                }
                Run(options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: dotnetCampus.GenericGenerator error \"{ex}\"");
            }
        }

        private static void Run(Options options)
        {
            if (options.ArgumentsFile is null || options.GeneratedSourceDirectory is null)
            {
                ReportBug("Compiling arguments are null.");
                return;
            }

            var argumentsFile = ConfigurationFactory.FromFile(options.ArgumentsFile).CreateAppConfigurator().Of<ArgumentsFile>();
            var generatedSourceDirectory = new DirectoryInfo(options.GeneratedSourceDirectory);
            new GenericCompiler(generatedSourceDirectory).Compile(argumentsFile);
        }

        private static void ReportBug(string text)
        {
            Console.WriteLine($"error: {text} This may be a bug of dotnetCampus.GenericGenerator. Please post it here: https://github.com/dotnet-campus/dotnetCampus.GenericGenerator/issues/new");
        }
    }
}
