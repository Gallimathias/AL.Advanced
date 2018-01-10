using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Core.Syntax;
using Compiler.Core.Parser;
using ALMember = Compiler.Core.Syntax.AL.Members;
using AdvancedMember = Compiler.Core.Syntax.ALAdvanced.Members;
using Compiler.Core.Translators.Maps;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;

namespace Compiler.Core.Translators
{
    public class AlToAdvanced : Translator
    {
        public AlToAdvanced(ALSyntaxTree tree) : base(tree)
        {
        }

        public ALAdvancedSyntaxTree Translate()
        {
            var root = Translate<AdvancedMember.CodeUnitSyntax>(SourceTree.RootMember);

            TranslateMember((ALMember.ObjectSyntax)SourceTree.RootMember, root);

            root.Normalize();
            root.ParseCSharp();

            return new ALAdvancedSyntaxTree(root);
        }

        public void TranslateMember(ALMember.ObjectSyntax rootMember, AdvancedMember.ObjectSyntax root)
        {
            var map = new AlToAdvancedMemberMap(this);

            var members = rootMember.Members;
            var tmp = new List<SyntaxMember>();

            foreach (var member in members)
            {
                var type = member.GetType();
                var obj = map.Invoke(type.Name, member);

                if (obj is SyntaxMember targetMember)
                    tmp.Add(targetMember);
            }

            root.AddMember(tmp.ToArray());
        }

        public T Translate<T>(ALMember.ObjectSyntax source) where T : AdvancedMember.ObjectSyntax
        {
            return base.Translate<T>(source);
        }

        internal SyntaxStatement TranslateStatement(SyntaxStatement statement)
        {
            var map = new AlToAdvancedStatementMap(this);
            
                var obj = map.Invoke(statement.Name, statement);

                if (obj is SyntaxStatement syntaxStatement)
                    return syntaxStatement;

            return null;
        }

        internal SyntaxType TranslateType(SyntaxType syntaxType)
        {
            if (syntaxType == null)
                return null;

            var map = new AlToAdvancedTypeMap(this);
            
            var obj = map.Invoke(syntaxType.Name, syntaxType);

            if (obj is SyntaxType type)
                return type;

            return null;
        }
    }
}
