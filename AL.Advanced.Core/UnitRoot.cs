using AL.Advanced.Core.Definition;
using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Core
{
    public class UnitRoot
    {
        public List<Member> Members { get; private set; }

        public UnitRoot(params Member[] members)
        {
            Members = new List<Member>(members);
        }
        public UnitRoot(List<Member> list) : this(list.ToArray())
        {
        }

        public string ToText()
        {
            var strb = new StringBuilder();

            foreach (var member in Members)
            {
                strb.AppendLine(member.ToText());
            }

            return strb.ToString();
        }
    }
}
