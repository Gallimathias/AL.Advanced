using Compiler.Core.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Translators
{
    public abstract class Translator
    {
        protected SyntaxTree SourceTree;

        public Translator(SyntaxTree tree)
        {
            SourceTree = tree;
        }
        

        public virtual T Translate<T>(object source)
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
