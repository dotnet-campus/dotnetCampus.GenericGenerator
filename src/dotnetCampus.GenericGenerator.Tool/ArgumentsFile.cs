using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnetCampus.Configurations;

namespace dotnetCampus.Runtime.CompilerServices
{
    internal class ArgumentsFile : Configuration
    {
        public ArgumentsFile() : base("")
        {
        }

        public string[] Compile => ((string)GetString()).Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();

        public string[] DefineConstants => ((string)GetString()).Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
    }
}
