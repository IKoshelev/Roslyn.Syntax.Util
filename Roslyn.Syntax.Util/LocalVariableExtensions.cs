using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslyn.Syntax.Util
{
    public static class LocalVariableExtensions
    {
        public static LocalDeclarationStatementSyntax LocalVairableDeclaration(
                                                                    this TypeSyntax type, 
                                                                    string name, 
                                                                    ExpressionSyntax value = null)
        {
            var declarator = SF.VariableDeclarator(SF.Identifier(name));

            if (value != null)
            {
                declarator = declarator.WithInitializer(SF.EqualsValueClause(value));
            }

            var delaration = SF.LocalDeclarationStatement(
                                    SF.VariableDeclaration(type)
                                    .WithVariables(SF.SingletonSeparatedList(declarator)));

            return delaration;

        }
    }
}
