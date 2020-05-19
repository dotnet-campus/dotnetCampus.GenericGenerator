using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace dotnetCampus.Runtime.CompilerServices
{
    public class GenericCompiler
    {
        private DirectoryInfo _targetDirectory;

        public GenericCompiler(DirectoryInfo generatedSourceDirectory)
        {
            _targetDirectory = generatedSourceDirectory;
        }

        internal void Compile(ArgumentsFile argumentsFile)
        {
            foreach (var file in argumentsFile.Compile)
            {
                var text = File.ReadAllText(file);
                if (text.Contains("GenerateGenericFromThis", StringComparison.Ordinal))
                {
                    Compile(text, argumentsFile.DefineConstants);
                }
            }
        }

        private Dictionary<int, string> Compile(string code, IEnumerable<string> defineConstancts)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code, new CSharpParseOptions(
                languageVersion: LanguageVersion.Latest,
                documentationMode: DocumentationMode.None,
                kind: SourceCodeKind.Regular,
                preprocessorSymbols: defineConstancts));
            var visitor = new GenerateGenericFromThisAttributeVisitor();
            visitor.Visit(syntaxTree.GetRoot());

            var dictionary = new Dictionary<int, string>();
            if (visitor.ShouldGenerateGenericCode)
            {
                for (var i = visitor.GenerateFromCount; i <= visitor.GenerateToCount; i++)
                {
                    var generatedCode = Generate(code, i);
                    dictionary[i] = generatedCode;
                }
            }
            return dictionary;
        }

        private string Generate(string code, int genericCount)
        {
            return new GenericTypeGenerator(code).Generate(genericCount);
        }
    }
}
