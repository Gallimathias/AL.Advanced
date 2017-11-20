using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALMember = Compiler.Core.Syntax.AL.Members;
using AdvancedMember = Compiler.Core.Syntax.ALAdvanced.Members;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Compiler.Core.Syntax;

namespace Compiler.Core.Translators.Maps
{
    public class AlToAdvancedMemberMap : Map
    {
        public MethodInfo[] Methods => GetType().GetMethods();

        public AlToAdvancedMemberMap(AlToAdvanced translator) : base(translator)
        {
        }

        public override object Invoke(string memberName, object member) => GetType().GetMethod(memberName).Invoke(this, new[] { member });

        public object ObjectCtorSyntax(ALMember.ObjectCtorSyntax ctor) => null;

        public AdvancedMember.MethodSyntax MethodHeadSyntax(ALMember.MethodHeadSyntax head)
        {
            var parent = (ALMember.CodeUnitSyntax)head.Parent;

            var method = Copy<AdvancedMember.MethodSyntax>(head);

            method.ReturnType = ((AlToAdvanced)Translator).TranslateType(head.ReturnType);

            var body = (ALMember.MethodBodySyntax)parent.Members.FirstOrDefault(m =>
            {
                if (m is ALMember.MethodBodySyntax bodySyntax)
                {
                    if (bodySyntax.Identifier == $"{head.Identifier}_Scope")
                        return true;
                }

                return false;
            });

            if (body == null)
                return null;

            foreach (var statement in body.Statements)
            {
                var tmp = ((AlToAdvanced)Translator).TranslateStatement(statement);

                if (tmp == null)
                    continue;

                tmp.Parent = method;
                method.Statements.Add(tmp);
            };

            var parameters = new Dictionary<string, SyntaxType>();
            foreach (var parameter in head.Parameters)
            {
                parameters.Add(parameter.Key,
                    ((AlToAdvanced)Translator).TranslateType(parameter.Value));
            }

            method.Parameters = parameters;

            return method;
        }

        public object MethodBodySyntax(ALMember.MethodBodySyntax body) => null;

        public object PropertySyntax(ALMember.PropertySyntax property) => null;

        public object FieldSyntax(ALMember.FieldSyntax syntax) => null;

    }
}
