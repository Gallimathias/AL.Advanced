using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace Compiler.Core.Syntax.AL.Members
{
    public abstract class ObjectSyntax : ALSourceMemberSyntax<ClassDeclarationSyntax>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public SyntaxKind Modifier { get; set; }

        public SyntaxMember[] Members => members.ToArray();

        protected List<SyntaxMember> members;

        public ObjectSyntax()
        {
            members = new List<SyntaxMember>();
        }

        public override abstract bool TryParse(MemberDeclarationSyntax memberDeclaration,
            Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax);

        public override string ToString() => $"Object {ID} {Name}";

        internal override void Normalize()
        {
            foreach (var member in members)
                member.Normalize();

            if (CSharpMember != null)
                CSharpMember = CSharpMember.NormalizeWhitespace();
        }

        public void AddMember(params SyntaxMember[] member)
        {
            foreach (var tmp in member)
                tmp.Parent = this;

            members.AddRange(member);

        }
    }
}
