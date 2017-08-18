using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace Compiler.Core.Syntax.AL
{
    public class MethodHeadSyntax : ALSourceMemberSyntax<MethodDeclarationSyntax>
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


        private BlockSyntax block;

        public override bool TryParse(MemberDeclarationSyntax memberDeclaration,
            Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax)
        {
            memberSyntax = null;

            if (memberDeclaration is MethodDeclarationSyntax methodDeclaration)
            {
                var @override = methodDeclaration.Modifiers.FirstOrDefault(m => m.Kind() == SyntaxKind.OverrideKeyword).Kind();
                var @static = methodDeclaration.Modifiers.FirstOrDefault(m => m.Kind() == SyntaxKind.StaticKeyword).Kind();
                
                

                memberSyntax = new MethodHeadSyntax
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
            CSharpMember = CSharpMember.NormalizeWhitespace();
        }

        internal override void ParseCSharp()
        {
            if (!IsStatic && !IsOverride)
                block = BuildBlock();
            else
                block = SyntaxFactory.Block().NormalizeWhitespace();

            CSharpMember = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), Identifier)
                .AddModifiers(SyntaxFactory.Token(Modifier))
                .WithBody(block)
                .NormalizeWhitespace();
        }

        public override string ToString() => $"MethodHead {Identifier}";

        private BlockSyntax BuildBlock()
        {
            var use = (UsingStatementSyntax)SyntaxFactory.ParseStatement($"using({Identifier}_Scope scope = new {Identifier}_Scope(this))");
            var exp = SyntaxFactory.ParseStatement(@"scope.Run();");

            use = use.WithStatement(exp).NormalizeWhitespace();

            return SyntaxFactory.Block(use);
        }
    }
}
