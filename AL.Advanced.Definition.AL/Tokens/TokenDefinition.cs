using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AL.Advanced.Definition.AL.Tokens
{
    public class TokenDefinition
    {
        public string Name { get; set; }
        public int Group { get; set; }
        public List<Regex> Expressions { get; set; }
        public int ChildrenGroup { get; set; }

        public bool Skip { get; set; }

        private TokenDefinition()
        {
            Expressions = new List<Regex>();
        }
        public TokenDefinition(string name, params string[] patterns) : this()
        {
            Name = name;
            new List<string>(patterns).ForEach(p => Expressions.Add(new Regex(p)));
        }

        public TokenDefinition(string name, string pattern, bool skip) : this(name, pattern)
        {
            Skip = skip;
        }

        public override string ToString() => Name;
    }
}
