using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Walterlv.IO.PackageManagement;

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
            PackageDirectory.Delete(_targetDirectory);
            PackageDirectory.Create(_targetDirectory);
            foreach (var file in argumentsFile.Compile)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var text = File.ReadAllText(file);
                if (text.Contains("GenerateGenericFromThis", StringComparison.Ordinal))
                {
                    var generatedCodes = Compile(text, argumentsFile.DefineConstants);
                    foreach (var pair in generatedCodes)
                    {
                        var filePath = Path.Combine(
                            _targetDirectory.FullName,
                            $"{fileName}.{pair.Key}.cs");
                        File.WriteAllText(filePath, pair.Value);
                    }
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
