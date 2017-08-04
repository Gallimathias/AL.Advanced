using Compiler.Core.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax
{
    public abstract class SyntaxTree
    {
        public SyntaxSource SyntaxSource { get; protected set; }
        public SyntaxStream SyntaxStream { get; protected set; }
        public SyntaxMember RootMember { get; protected set; }

        public SyntaxTree(SyntaxMember memberSyntax)
        {
            RootMember = memberSyntax;
        }
        public SyntaxTree(List<SyntaxMember> memberSyntaxList)
        {

        }

        public abstract SyntaxTree Parse(SyntaxTree syntaxTree);
        public virtual TSyntaxTree Parse<TSyntaxTree>(SyntaxTree syntaxTree) where TSyntaxTree : SyntaxTree
            => (TSyntaxTree)Parse(syntaxTree);

        internal static TSyntaxTree GetTree<TSyntaxTree>(List<SyntaxMember> result) where TSyntaxTree : SyntaxTree
            => Activator.CreateInstance<TSyntaxTree>();
    }
}
