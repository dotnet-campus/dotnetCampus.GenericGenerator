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
            var options = CommandLine.Parse(args).As<Options>();
        }
    }
}
