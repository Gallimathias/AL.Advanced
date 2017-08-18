using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AL = Compiler.Core.Syntax.AL;
using Advanced = Compiler.Core.Syntax.ALAdvanced;
using System.Reflection;

namespace Compiler.Core.Translators.Maps
{
    public class AlToAdvancedMemberMap
    {

        public MethodInfo[] Methods => GetType().GetMethods();
        
        
        public object Invoke(string memberName, object member) => GetType().GetMethod(memberName).Invoke(this, new[] { member });

        public object ObjectCtorSyntax(AL.ObjectCtorSyntax ctor) => null;

        public Advanced.MethodSyntax MethodHeadSyntax(AL.MethodHeadSyntax head) {
            var parent = (AL.CodeUnitSyntax)head.Parent;

            var method = Copy<Advanced.MethodSyntax>(head);

            var body = parent.Members.FirstOrDefault(m => {
                if(m is AL.MethodBodySyntax bodySyntax)
                {
                    if (bodySyntax.Identifier == $"{head.Identifier}_Scope")
                        return true;
                }

                return false;
            });


            return method;
        }

        public object MethodBodySyntax(AL.MethodBodySyntax body) => null;

        public object PropertySyntax(AL.PropertySyntax property) => null;

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
