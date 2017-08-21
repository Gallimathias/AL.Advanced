using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Compiler.Core.Syntax
{
    public delegate bool SyntaxMemberParseDelegate(
        MemberDeclarationSyntax memberDeclaration, 
        Func<MemberDeclarationSyntax, SyntaxMember> analyser, 
        out SyntaxMember memberSyntax);

    public abstract class SyntaxMember 
    {
        public SyntaxMember Parent { get; set; }

        public abstract bool TryParse(MemberDeclarationSyntax memberDeclaration, Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax);

        internal abstract void ParseCSharp();

        internal abstract MemberDeclarationSyntax GetCSharpSyntax();

        internal abstract void Normalize();        
    }
}
