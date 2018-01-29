using AL.Advanced.Core.Definition;
using AL.Advanced.Definition.AL.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Definition.AL
{
    class ObjectDeclaration : ALObject<TokenStream>
    {
        public ObjectDeclaration(ObjectType type)
        {
            ObjectType = type;
        }

        public override bool Check(TokenStream root)
        {
            if (root.Length < 5)
                return false;

            if (root[0].Value.ToLower() != ObjectType.ToString().ToLower())
                return false;

            return true;
        }

        public override void Parse(TokenStream root)
        {
            if(!TryParse(root))
                throw new Exception("Could not Parse");
        }

        public override string ToText()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"codeunit {ID} {Name}");
            builder.AppendLine("{");
            builder.AppendLine("}");

            return builder.ToString();
        }

        public override bool TryParse(TokenStream root)
        {
            if (!Check(root))
                return false;

            if (!int.TryParse(root[1].Value, out int id))
                return false;
            else
                ID = id;

            Name = root[2].Value;

            return true;
        }
    }
}
