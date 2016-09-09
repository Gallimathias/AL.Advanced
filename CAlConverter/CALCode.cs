using System;
using System.Collections.Generic;

namespace CAlConverter
{
    public class CALCode
    {
        public List<CALMethod> CALMethods { get; set; }
        public List<CALVar> Globals { get; set; }

        public CALCode(string source)
        {
            if (!source.StartsWith("CODE"))
                return;

            Globals = GetGlobals(source);
            CALMethods = GetMethods(source);
        }

        private List<CALMethod> GetMethods(string source)
        {
            throw new NotImplementedException();
        }

        private List<CALVar> GetGlobals(string source)
        {
            int start = source.IndexOf("VAR") + 3;
            int length = source.LastIndexOf("LOCAL") - start;

            var temp = source.Substring(start, length).Split(';');

            var returnValue = new List<CALVar>();

            foreach (var item in temp)
                returnValue.Add(new CALVar(item));

            return returnValue;
        }
    }
}