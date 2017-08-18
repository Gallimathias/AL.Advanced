using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.ALAdvanced
{
    public class MethodSyntax : ALAdvancedSourceMemberSyntax<MethodDeclarationSyntax>
    {
        public string Identifier { get; set; }
        public SyntaxKind Modifier { get; set; }
        public bool IsOverride
        {
            get => @override == SyntaxKind.OverrideKeyword; set
            {
                if (value)
                    @override = SyntaxKind.OverrideKeyword;
                else
                    @override = SyntaxKind.None;
            }
        }
        public bool IsStatic
        {
            get => @static == SyntaxKind.StaticKeyword; set
            {
                if (value)
                    @static = SyntaxKind.StaticKeyword;
                else
                    @static = SyntaxKind.None;

            }
        }

        private SyntaxKind @override;
        private SyntaxKind @static;

        public override bool TryParse(MemberDeclarationSyntax memberDeclaration,
            Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax)
        {
            memberSyntax = null;

            if (memberDeclaration is MethodDeclarationSyntax methodDeclaration)
            {
                var @override = methodDeclaration.Modifiers.FirstOrDefault(m => m.Kind() == SyntaxKind.OverrideKeyword).Kind();
                var @static = methodDeclaration.Modifiers.FirstOrDefault(m => m.Kind() == SyntaxKind.StaticKeyword).Kind();

                memberSyntax = new MethodSyntax
                {
                    CSharpMember = methodDeclaration,
                    Identifier = (string)methodDeclaration.Identifier.Value,
                    Modifier = methodDeclaration.Modifiers.First().Kind(),
                    IsStatic = @static == SyntaxKind.StaticKeyword,
                    IsOverride = @override == SyntaxKind.OverrideKeyword
                };
                return true;
            }

            return false;
        }

        internal override void Normalize()
        {
            if (Identifier.Length > 0)
                Identifier = Identifier.Substring(0, 1).ToUpper() + Identifier.Substring(1);

            CSharpMember = CSharpMember.NormalizeWhitespace();
        }

        internal override void ParseCSharp()
        {
            CSharpMember = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), Identifier)
                .AddModifiers(SyntaxFactory.Token(Modifier))
                .WithBody(SyntaxFactory.Block())
                .NormalizeWhitespace();
        }

        public override string ToString() => $"Method {Identifier}";
    }
}
