using AL.Advanced.Core.Definition;
using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Definition.CSharpALFormatting.Object
{
    internal class CodeUnit : ALObject
    {
        public CodeUnit() : base()
        {
            ObjectType = ObjectType.CodeUnit;
        }

        public override string AsString()
        {
            throw new NotImplementedException();
        }

        public override bool Check()
        {
            throw new NotImplementedException();
        }

        public override void Parse()
        {
            throw new NotImplementedException();
        }

        public override void TryParse()
        {
            throw new NotImplementedException();
        }
    }
}
