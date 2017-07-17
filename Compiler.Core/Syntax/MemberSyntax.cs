using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Compiler.Core.Syntax
{
    public delegate bool SyntaxParsDelegate(MemberDeclarationSyntax memberDeclaration, Func<MemberDeclarationSyntax, MemberSyntax> analyser, out MemberSyntax memberSyntax);
    public abstract class MemberSyntax
    {
        public abstract bool TryParse(MemberDeclarationSyntax memberDeclaration, Func<MemberDeclarationSyntax, MemberSyntax> analyser, out MemberSyntax memberSyntax);
    }
}
