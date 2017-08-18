using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Core.Syntax;
using Compiler.Core.Parser;
using AL = Compiler.Core.Syntax.AL;
using Advanced = Compiler.Core.Syntax.ALAdvanced;
using Compiler.Core.Translators.Maps;

namespace Compiler.Core.Translators
{
    public class AlToAdvanced : Translator
    {
        public AlToAdvanced(ALSyntaxTree tree) : base(tree)
        {
        }

        public ALAdvancedSyntaxTree Translate()
        {
            var root = Translate<Advanced.CodeUnitSyntax>(SourceTree.RootMember);

            TranslateMember((AL.ObjectSyntax)SourceTree.RootMember, root);

            root.Normalize();
            root.ParseCSharp();

            return new ALAdvancedSyntaxTree(root);
        }

        private void TranslateMember(AL.ObjectSyntax rootMember, Advanced.ObjectSyntax root)
        {
            var map = new AlToAdvancedMemberMap();
           
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

        public T Translate<T>(AL.ObjectSyntax source) where T : Advanced.ObjectSyntax
        {
            return base.Translate<T>(source);
        }

    }
}
