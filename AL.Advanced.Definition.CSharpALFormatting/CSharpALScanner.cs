using AL.Advanced.Core;
using AL.Advanced.Core.Definition;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace AL.Advanced.Definition.CSharpALFormatting
{
    class CSharpALScanner : Scanner<MemberDeclarationSyntax>
    {
        private Dictionary<string, Type> objects;

        public override void Initialize()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            objects = new Dictionary<string, Type>();

            foreach (var type in types.Where(t => t.BaseType == typeof(ALObject<MemberDeclarationSyntax>)))
                objects.Add(type.Name, type);
        }

        public override bool TryScan(MemberDeclarationSyntax member, out Member<MemberDeclarationSyntax> root)
        {
            root = null;

            if (member is ClassDeclarationSyntax classDeclaration)
            {
                var typeName = classDeclaration.BaseList.Types.FirstOrDefault()?.ToString();

                if (string.IsNullOrWhiteSpace(typeName))
                    return false;

                if (typeName.ToLower().StartsWith("nav"))
                    typeName = typeName.Substring(3);

                var obj = (ALObject<MemberDeclarationSyntax>)Activator.CreateInstance(objects[typeName]);

                obj.TryParse(classDeclaration);
            }

            return false;
        }
    }
}
