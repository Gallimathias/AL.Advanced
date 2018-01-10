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
    public abstract class ObjectSyntax : ALAdvancedMemberSyntax<ClassDeclarationSyntax>
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

        public abstract override bool TryParse(MemberDeclarationSyntax memberDeclaration,
            Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax);

        internal override void Normalize()
        {
            foreach (var member in members)
                member.Normalize();

            if (CSharpMember != null)
                CSharpMember = CSharpMember.NormalizeWhitespace();

            if (Name.Length > 0)
                Name = Name.Substring(0, 1).ToUpper() + Name.Substring(1);
        }

        public void AddMember(params SyntaxMember[] member)
        {
            foreach (var tmp in member)
                tmp.Parent = this;

            members.AddRange(member);

        }
    }
}
