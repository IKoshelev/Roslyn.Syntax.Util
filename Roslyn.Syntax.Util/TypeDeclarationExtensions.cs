using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roslyn.Syntax.Util
{
    public static class TypeDeclarationExtensions
    {
        /// <summary>
        /// Warning, this method does not hanlde Partial classes
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MemberDeclarationSyntax[] GetFieldAndPropertyDeclarations(
            this TypeDeclarationSyntax type, 
            bool aknowledgePartialDeclarationLimitation = false)
        {
            if (type.IsPartial() && !aknowledgePartialDeclarationLimitation)
            {
                throw new ArgumentException($"Type {type.Identifier.TrimmedText()} is partial, " +
                                            $"its members are contained in multiple graphs in multiple files. " +
                                            $"To aknowledge this limitation and allow method to proceed, " +
                                            $"pass true to {nameof(aknowledgePartialDeclarationLimitation)}");
            }

            return type
                    .ChildNodes()
                    .Where(node => node.Fits(SyntaxKind.PropertyDeclaration,
                                                SyntaxKind.FieldDeclaration))
                    .OfType<MemberDeclarationSyntax>()
                    .ToArray();

        }

        public static SyntaxToken[] GetFieldAndPropertyIdentifiers(this TypeDeclarationSyntax type)
        {
            return type
                      .GetFieldAndPropertyDeclarations()
                      .SelectMany(member => member.GetMemberIdentifier())
                      .ToArray();
        }

        public static bool IsPartial(this TypeDeclarationSyntax type)
        {
            foreach(var node in type.ChildNodesAndTokens())
            {
                var kind = node.Kind();
                if(kind.Fits(SyntaxKind.ClassKeyword, SyntaxKind.StringKeyword))
                {
                    return false;   // 'partial' can only come before this
                }

                if(kind == SyntaxKind.PartialKeyword)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
