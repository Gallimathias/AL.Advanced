﻿using AL.Advanced.Core.Definition;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Definition.CSharpALFormatting.Object
{
    internal class Codeunit : ALObject<MemberDeclarationSyntax>
    {
        public Codeunit() : base()
        {
            ObjectType = ObjectType.CodeUnit;
        }

        public override string ToText()
        {
            throw new NotImplementedException();
        }

        public override bool Check(MemberDeclarationSyntax root)
        {
            throw new NotImplementedException();
        }

        public override void Parse(MemberDeclarationSyntax root)
        {
            throw new NotImplementedException();
        }

        public override bool TryParse(MemberDeclarationSyntax root)
        {
            throw new NotImplementedException();
        }
    }
}
