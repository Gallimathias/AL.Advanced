﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.ALAdvanced
{
    internal class MethodBuilder : ISubBuilder
    {
        public MemberDeclarationSyntax CurrentSourceNode { get; private set; }
        public MemberDeclarationSyntax CurrentNode { get; private set; }

        public MethodBuilder(MemberDeclarationSyntax baseMember)
        {
            CurrentSourceNode = baseMember;

        }

        public MemberDeclarationSyntax Create()
        {
            baseMethod();
            baseBody();

            return CurrentNode.NormalizeWhitespace();
        }

        private void baseBody()
        {
            var classDeclaration = (ClassDeclarationSyntax)CurrentSourceNode;
            CurrentSourceNode = classDeclaration.Members.FirstOrDefault(
                                     m => m.Kind() == SyntaxKind.MethodDeclaration &&
                                        ((MethodDeclarationSyntax)m).Identifier.ValueText == $"OnRun");
            var builder = new BodyBuilder((MethodDeclarationSyntax)CurrentSourceNode);
            CurrentNode = ((MethodDeclarationSyntax)CurrentNode).WithBody(builder.Create());


        }

        private void baseMethod()
        {
            var classDeclaration = (ClassDeclarationSyntax)CurrentSourceNode;
            string name = "";

            foreach (AttributeListSyntax attributeList in classDeclaration.AttributeLists)
            {
                foreach (AttributeSyntax attribute in attributeList.Attributes)
                {
                    if (((IdentifierNameSyntax)attribute.Name).Identifier.ValueText != "NavName")
                        continue;

                    name = ((LiteralExpressionSyntax)attribute.ArgumentList.Arguments[0].Expression)
                                                    .Token.ValueText;
                    break;
                }
            }

            var methodDeclaration = SyntaxFactory.MethodDeclaration(
                SyntaxFactory.ParseTypeName("void"),
                name);

            CurrentNode = methodDeclaration;
        }
    }
}