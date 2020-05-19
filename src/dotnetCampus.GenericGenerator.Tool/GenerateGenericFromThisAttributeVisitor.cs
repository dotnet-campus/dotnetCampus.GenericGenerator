using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace dotnetCampus.Runtime.CompilerServices
{
    public class GenerateGenericFromThisAttributeVisitor : CSharpSyntaxRewriter
    {
        private readonly IList<string> _attributeNames;
        private readonly int _defaultGenerateFromCount;
        private readonly int _defaultGenerateToCount;

        public bool ShouldGenerateGenericCode { get; private set; }
        public int GenerateFromCount { get; private set; }
        public int GenerateToCount { get; private set; }

        public GenerateGenericFromThisAttributeVisitor()
        {
            var defaultAttribute = new GenerateGenericFromThisAttribute();
            _attributeNames = new List<string>
            {
                typeof(GenerateGenericFromThisAttribute).Name,
                typeof(GenerateGenericFromThisAttribute).Name.Replace(typeof(Attribute).Name, "", StringComparison.Ordinal),
            };
            _defaultGenerateFromCount = defaultAttribute.From;
            _defaultGenerateToCount = defaultAttribute.To;
        }

        public override SyntaxNode? VisitAttribute(AttributeSyntax node)
        {
            if (node.Parent is AttributeListSyntax attributeListSyntax)
            {
                if (attributeListSyntax.Parent is ClassDeclarationSyntax
                    || attributeListSyntax.Parent is StructDeclarationSyntax
                    || attributeListSyntax.Parent is DelegateDeclarationSyntax)
                {
                    string? attributeName = null;
                    if (node.Name is IdentifierNameSyntax identifierName)
                    {
                        attributeName = identifierName.ToString();
                    }
                    else if (node.Name is QualifiedNameSyntax qualifiedName)
                    {
                        attributeName = qualifiedName.ChildNodes().OfType<IdentifierNameSyntax>().LastOrDefault()?.ToString();
                    }

                    if (attributeName != null && _attributeNames.Contains(attributeName))
                    {
                        ShouldGenerateGenericCode = true;

                        var argumentList = node.ChildNodes().OfType<AttributeArgumentListSyntax>().FirstOrDefault();
                        if (argumentList is null)
                        {
                            GenerateFromCount = 2;
                            GenerateToCount = 8;
                        }
                        else
                        {
                            var arguments = argumentList.ChildNodes().OfType<AttributeArgumentSyntax>().SelectMany(x => x.ChildNodes());
                            var assignIndex = 0;
                            foreach (var argument in arguments)
                            {
                                int.TryParse(argument.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var value);
                                if (assignIndex == 0)
                                {
                                    GenerateFromCount = value;
                                }
                                else
                                {
                                    GenerateToCount = value;
                                }
                                assignIndex++;
                            }
                        }
                    }
                }
            }

            return base.VisitAttribute(node);
        }
    }
}
