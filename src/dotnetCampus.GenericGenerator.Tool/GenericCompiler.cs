using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetCampus.Runtime.CompilerServices
{
    public class GenericCompiler
    {
        private DirectoryInfo _targetDirectory;

        public GenericCompiler(DirectoryInfo generatedSourceDirectory)
        {
            _targetDirectory = generatedSourceDirectory;
        }

        internal void Compile(string[] csharpFiles)
        {
            foreach (var file in csharpFiles)
            {
                var text = File.ReadAllText(file);
                if (text.Contains("GenerateGenericFromThis", StringComparison.Ordinal))
                {
                    Compile(text);
                }
            }
        }

        private void Compile(string code)
        {

        }
    }
}
