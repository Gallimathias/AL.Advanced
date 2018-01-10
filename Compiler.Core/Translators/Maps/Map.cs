using System;
using System.Linq;

namespace Compiler.Core.Translators.Maps
{
    public abstract class Map
    {
        protected Translator Translator;

        public Map(Translator translator)
        {
            Translator = translator;
        }

        public abstract object Invoke(string name, object obj);

        public T Copy<T>(object source)
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