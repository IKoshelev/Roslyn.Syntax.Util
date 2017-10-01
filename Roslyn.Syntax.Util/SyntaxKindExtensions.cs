using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roslyn.Syntax.Util
{
    public static class SyntaxKindExtensions
    {
        public static bool Fits(this SyntaxKind kind, params SyntaxKind[] options)
        {
            return options.Contains(kind);
        }

        public static bool Fits(this SyntaxNode node, params SyntaxKind[] options)
        {
            return options.Contains(node.Kind());
        }

        public static bool Fits(this SyntaxToken token, params SyntaxKind[] options)
        {
            return options.Contains(token.Kind());
        }
    }
}
