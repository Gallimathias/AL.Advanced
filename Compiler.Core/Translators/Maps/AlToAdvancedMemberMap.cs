using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALMember = Compiler.Core.Syntax.AL.Members;
using AdvancedMember = Compiler.Core.Syntax.ALAdvanced.Members;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

            var onRun = (ALMember.MethodHeadSyntax)body.Members.FirstOrDefault(m =>
            {
                if (m is ALMember.MethodHeadSyntax onRunHead)
                {
                    if (onRunHead.Identifier == "OnRun")
                        return true;
                }
                return false;
            });

            var block = onRun.CSharpMember.Body;

            var statements = ((AlToAdvanced)Translator).TranslateStatements(block.Statements);
            

            return method;
        }

        public object MethodBodySyntax(ALMember.MethodBodySyntax body) => null;

        public object PropertySyntax(ALMember.PropertySyntax property) => null;

        public object FieldSyntax(ALMember.FieldSyntax syntax) => null;

        private T Copy<T>(object source)
        {
            var target = Activator.CreateInstance<T>();

            var sourceProps = source.GetType().GetProperties();
            var targetProps = target.GetType().GetProperties();

            foreach (var sourceProp in sourceProps)
            {
                if (!sourceProp.CanRead)
                    continue;

                var targetProp = targetProps.FirstOrDefault(p => p.Name == sourceProp.Name);

                if (targetProp == null)
                    continue;

                if (!targetProp.CanWrite)
                    continue;

                targetProp.SetValue(target, sourceProp.GetValue(source));

            }

            return target;
        }
        
    }
}
