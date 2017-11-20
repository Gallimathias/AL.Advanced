using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.AL.Members
{
    public class FieldSyntax : ALSourceMemberSyntax<FieldDeclarationSyntax>
    {
        public string Identifier { get; set; }

        public override bool TryParse(MemberDeclarationSyntax memberDeclaration,
            Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax)
        {
            memberSyntax = null;

            if (memberDeclaration is FieldDeclarationSyntax syntax)
            {

                memberSyntax = new FieldSyntax
                {
                    CSharpMember = syntax,
                    Identifier = syntax.Declaration.Variables[0].Identifier.Text
                };
                return true;
            }

            return false;
        }

        internal override void Normalize()
        {
        }

        internal override void ParseCSharp()
        {
        }
    }
}
