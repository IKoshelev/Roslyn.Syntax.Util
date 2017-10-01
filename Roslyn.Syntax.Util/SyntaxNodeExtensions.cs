using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Syntax.Util;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslyn.Syntax.Util
{
    public static class SyntaxNodeExtensions
    {
        public static T WithTracking<T>(
            this T rootNode, 
            Func<T, Func<SyntaxNode, IEnumerable<SyntaxNode>, T>> getMutatorMethod, 
            SyntaxNode targetNode, 
            IEnumerable<SyntaxNode> mutationNodes) where T: SyntaxNode
        {
            var currentNode = rootNode.GetCurrentNode(targetNode);
            var method = getMutatorMethod(rootNode);
            return method(currentNode, mutationNodes);
        }

    //    public static T WithTracking<T>(
    //this T rootNode,
    //Func<T, Func<SyntaxNode, IEnumerable<SyntaxNode>, T>> getMutatorMethod,
    //SyntaxNode targetNode,
    //IEnumerable<SyntaxNode> mutationNodes) where T : SyntaxList
    //    {
    //        var currentNode = rootNode.GetCurrentNode(targetNode);
    //        var method = getMutatorMethod(rootNode);
    //        return method(currentNode, mutationNodes);
    //    }
    }
}
