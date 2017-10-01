using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace Roslyn.Syntax.Util
{
    public static class AttributeExtensions
    {
        public static SyntaxToken[] GetAttributeIdentifiers(this SyntaxNode target)
        {
            return target.DescendantNodes()
                    .OfType<AttributeSyntax>()
                    .SelectMany(n => n.DescendantTokens())
                    .Where(n => n.Fits(SyntaxKind.IdentifierToken))
                    .ToArray();
        }

        public static bool FitsAttrIdentifier(this SyntaxToken token, Type attributeType)
        {
            var attirubteNames = new[]
            {
                attributeType.Name,
                attributeType.Name.Replace("Attribute","")
            };

            return attirubteNames.Contains(token.Value);
        }
    }
}
