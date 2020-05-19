using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnetCampus.Cli;

namespace dotnetCampus.Runtime.CompilerServices
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var options = CommandLine.Parse(args).As<Options>();
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
                Console.WriteLine("error: Compiling arguments are null. This may be a bug of dotnetCampus.GenericGenerator.");
            }
        }
    }
}
