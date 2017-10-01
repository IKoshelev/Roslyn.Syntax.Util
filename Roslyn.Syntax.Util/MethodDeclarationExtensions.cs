using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslyn.Syntax.Util
{
    public static class MethodDeclarationExtensions
    {
        public static (TypeSyntax type, SyntaxToken identifier)[] 
            GetParameters(this MethodDeclarationSyntax methodDecl)
        {
            var parameters = methodDecl.ParameterList;
            var result = parameters
                            .Parameters
                            .Select(x => (x.Type, x.Identifier))
                            .ToArray();

            return result;
        }

        public static  SyntaxToken[] GetParameterIdentifiers(this MethodDeclarationSyntax methodDecl)
        {
            return methodDecl
                        .GetParameters()
                        .Select(x => x.identifier)
                        .ToArray();
        }

        public static SyntaxToken[] GetLocalVariableIdentifiers(this MethodDeclarationSyntax methodDecl)
        {
            return methodDecl
                            .DescendantNodes()
                            .OfType<VariableDeclaratorSyntax>()
                            .Where(methodDecl.DirectlyContains)
                            .Select(declarator => declarator.Identifier)
                            .ToArray();
        }

        public static MethodDeclarationSyntax WithStatementAtTheBegining(
                                            this MethodDeclarationSyntax methodDecl,
                                            params StatementSyntax[] statements)
        {
            MethodDeclarationSyntax newMethod;

            if (methodDecl.Body.ChildNodes().Any() == false)
            {
                newMethod = methodDecl.WithBody(SF.Block(statements));
                return newMethod;
            }

            var firstNode = methodDecl.Body.ChildNodes().First();
            newMethod = methodDecl.InsertNodesBefore(firstNode, statements);

            return newMethod;
        }

        public static bool DirectlyContains(
                                    this MemberDeclarationSyntax methodDecl,
                                    SyntaxNode node)
        {
            var parent = node.Parent;
            do
            {
                var parentIsAKindOfInnerFunction =
                    parent.Fits(SyntaxKind.LocalFunctionStatement,
                                SyntaxKind.DelegateDeclaration,
                                SyntaxKind.SimpleLambdaExpression,
                                SyntaxKind.ParenthesizedExpression);

                if (parentIsAKindOfInnerFunction)
                {
                    return false;
                };

                if(parent == methodDecl)
                {
                    return true;
                }

                parent = parent.Parent;

            } while (parent != null);

            return false;
        }

        public static TypeDeclarationSyntax GetDeclaringType(
                            this MemberDeclarationSyntax methodDecl)
        {
            return methodDecl.FirstAncestorOrSelf<TypeDeclarationSyntax>();
        }
    }
}
