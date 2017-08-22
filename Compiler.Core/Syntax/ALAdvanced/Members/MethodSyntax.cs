using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.ALAdvanced.Members
{
    public class MethodSyntax : ALAdvancedMemberSyntax<MethodDeclarationSyntax>
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

        public List<SyntaxStatement> Statements { get; set; }
        public BlockSyntax Body { get; set; }

        private SyntaxKind @override;
        private SyntaxKind @static;

        public MethodSyntax()
        {
            Body = SyntaxFactory.Block();
            Statements = new List<SyntaxStatement>();
        }

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
            Body = SyntaxFactory.Block();

            foreach (var statement in Statements)
            {
                statement.ParseCSharp();
                Body = Body.AddStatements(statement.GetCSharpSyntax());
            }


            CSharpMember = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), Identifier)
                .AddModifiers(SyntaxFactory.Token(Modifier))
                .WithBody(Body)
                .NormalizeWhitespace();
        }

        public override string ToString() => $"Method {Identifier}";
    }
}
