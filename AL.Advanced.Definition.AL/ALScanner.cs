using AL.Advanced.Core;
using AL.Advanced.Core.Definition;
using AL.Advanced.Definition.AL.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Definition.AL
{
    class ALScanner : Scanner
    {
        public override bool TryGetCopy(object member, out Member copie)
        {
            copie = null;

            if (member is ALObject originObject)
            {
                copie = originObject.GetCopyAs<ObjectDeclaration>();
                return true;
            }

            return false;
        }

        public override bool TryScan(object member, out Member root)
        {
            root = null;

            if(member is TokenStream tokenStream)
            {
                var typeName = tokenStream[0].Value;

                if (string.IsNullOrWhiteSpace(typeName))
                    return false;

                root = new ObjectDeclaration((ObjectType)Enum.Parse(typeof(ObjectType), typeName, true));
                return root.TryParse(tokenStream);
            }

            return false;
        }
    }
}
