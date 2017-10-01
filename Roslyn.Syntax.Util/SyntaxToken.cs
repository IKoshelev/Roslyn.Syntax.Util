using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslyn.Syntax.Util
{
    public static class SyntaxTokenExtensions
    {
        public static  string TrimmedText(this SyntaxToken token)
        {
            return token.Text.Trim();
        }

        public static InvocationExpressionSyntax ToNameof(this SyntaxToken token)
        {
            return token.TrimmedText().ToNameof();
        }

        public static InvocationExpressionSyntax ToNameof(this string name)
        {
            return SF.InvocationExpression(
                        SF.IdentifierName("nameof"))
                            .WithArgumentList(
                                SF.ArgumentList(
                                    SF.SingletonSeparatedList<ArgumentSyntax>(
                                        SF.Argument(
                                            SF.IdentifierName("C")))));
        }
    }
}
