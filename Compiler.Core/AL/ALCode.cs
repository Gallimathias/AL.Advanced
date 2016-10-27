using System;
using System.Collections.Generic;

namespace Compiler.Core.AL
{
    public class ALCode
    {
        public List<ALMethod> CALMethods { get; set; }
        public List<ALVar> Globals { get; set; }

        public ALCode(string source)
        {
            if (!source.StartsWith("CODE"))
                return;

            Globals = GetGlobals(source);
            CALMethods = GetMethods(source);
        }

        private List<ALMethod> GetMethods(string source)
        {
            throw new NotImplementedException();
        }

        private List<ALVar> GetGlobals(string source)
        {
            int start = source.IndexOf("VAR") + 3;
            int length = source.LastIndexOf("LOCAL") - start;

            var temp = source.Substring(start, length).Split(';');

            var returnValue = new List<ALVar>();

            foreach (var item in temp)
                returnValue.Add(new ALVar(item));

            return returnValue;
        }
    }
}