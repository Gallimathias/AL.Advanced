using AL.Advanced.Core;
using AL.Advanced.Core.Definition;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Al.Advanced.Definition.CSharp
{
    class CSharpScanner : Scanner
    {
        public override bool TryScan(object member, out Member root)
        {
            root = null;

            if (member is ClassDeclarationSyntax classDeclaration)
            {
                var typeName = classDeclaration.BaseList.Types.FirstOrDefault()?.ToString();

                if (string.IsNullOrWhiteSpace(typeName))
                    return false;
                
                root = new ObjectDeclaration((ObjectType)Enum.Parse(typeof(ObjectType), typeName, true));
                return root.TryParse(classDeclaration);
            }

            return false;
        }
    }
}
