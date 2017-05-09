using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.ALAdvanced
{
    public class BodyBuilder : IBlockBuilder
    {
        public MethodDeclarationSyntax CurrentSourceNode { get; private set; }
        public BlockSyntax CurrentNode { get; private set; }

        public BodyBuilder(MethodDeclarationSyntax baseMember)
        {
            CurrentSourceNode = baseMember;
        }

        public BlockSyntax Create()
        {
            matching();
            return CurrentNode.NormalizeWhitespace();
        }

        private void matching()
        {
            foreach (var statement in CurrentSourceNode.Body.Statements)
            {
              //CurrentNode = CurrentNode.AddStatements(
              //     TypeMappingManager.MapStatement(
              //         DefinitionFormat.AL, DefinitionFormat.ALAdvanced, statement));
            }
        }
    }
}
