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
    }
}