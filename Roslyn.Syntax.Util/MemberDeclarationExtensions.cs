using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roslyn.Syntax.Util
{
    public static class MemberDeclarationExtensions
    {
        public static SyntaxToken[] GetMemberIdentifier(this MemberDeclarationSyntax member)
        {
            switch (member)
            {
                case PropertyDeclarationSyntax prop:
                    return new[] { prop.Identifier };
                    break;
                case FieldDeclarationSyntax field:
                    return field.Declaration.Variables.Select(x => x.Identifier).ToArray();
                    break;
                default:
                    throw new ArgumentException($"Unknown MemberDeclarationSyntax node type : {member.Kind().ToString()}");
            }
        }

        public static TypeSyntax GetMemberType(this MemberDeclarationSyntax member)
        {
            switch (member)
            {
                case PropertyDeclarationSyntax prop:
                    return prop.Type;
                    break;
                case FieldDeclarationSyntax field:
                    return field.Declaration.Type;
                    break;
                default:
                    throw new ArgumentException($"Unknown MemberDeclarationSyntax node type : {member.Kind().ToString()}");
            }
        }
    }
}
