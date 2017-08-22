using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.AL
{
    internal static class AlParser
    {
        private static Dictionary<int, SyntaxMemberParseDelegate> memberDictionary;
        private static Dictionary<int, SyntaxStatementParseDelegate> statementDictionary;
        private static Dictionary<int, SyntaxTypeParseDelegate> typeDictionary;

        static AlParser()
        {
            memberDictionary = new Dictionary<int, SyntaxMemberParseDelegate>();
            statementDictionary = new Dictionary<int, SyntaxStatementParseDelegate>();
            typeDictionary = new Dictionary<int, SyntaxTypeParseDelegate>();
            GetSnytax();
        }

        public static SyntaxStatement ParseStatement(StatementSyntax statementSyntax)
        {
            foreach (var statement in statementDictionary)
            {
                if (statement.Value(statementSyntax, ParseStatement, out SyntaxStatement syntaxStatement))
                    return syntaxStatement;
            }

            throw new Exception($"{statementSyntax.Kind()} [{statementSyntax.RawKind}] " +
                $"is no valid {SyntaxSource.ALSource} statement. On Span " +
                $"{statementSyntax.FullSpan}");
        }

        public static SyntaxType ParseType(TypeSyntax typeSyntax)
        {
            foreach (var type in typeDictionary)
            {
                if (type.Value(typeSyntax, ParseType, out SyntaxType syntaxType))
                    return syntaxType;
            }

            throw new Exception($"{typeSyntax.Kind()} [{typeSyntax.RawKind}] " +
                $"is no valid {SyntaxSource.ALSource} type. On Span " +
                $"{typeSyntax.FullSpan}");
        }
       

        private static void GetSnytax()
        {
            var typeList = Assembly.GetExecutingAssembly().GetTypes().Where(
                t => t.GetCustomAttributes<SyntaxSourceAttribute>()?.FirstOrDefault(
                        a => a.SyntaxSource == SyntaxSource.ALSource) != null &&
                        !t.IsAbstract).ToArray();

            for (int i = 0; i < typeList.Length; i++)
            {
                var obj = Activator.CreateInstance(typeList[i]);

                if (obj is SyntaxMember memberSyntax)
                    memberDictionary.Add(i, memberSyntax.TryParse);
                else if (obj is SyntaxStatement statement)
                    statementDictionary.Add(i, statement.TryParse);
                else if (obj is SyntaxType type)
                    typeDictionary.Add(i, type.TryParse);

            }
        }
    }
}
