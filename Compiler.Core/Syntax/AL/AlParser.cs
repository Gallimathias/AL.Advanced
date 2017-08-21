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

        static AlParser()
        {
            memberDictionary = new Dictionary<int, SyntaxMemberParseDelegate>();
            statementDictionary = new Dictionary<int, SyntaxStatementParseDelegate>();
            GetSnytax();
        }

        public static SyntaxStatement ParseStatement(StatementSyntax statement)
        {
            foreach (var syntax in statementDictionary)
            {
                if (syntax.Value(statement, ParseStatement, out SyntaxStatement syntaxStatement))
                    return syntaxStatement;
            }

            throw new Exception($"{statement.Kind()} [{statement.RawKind}] " +
                $"is no valid {SyntaxSource.ALSource} expression. On Span " +
                $"{statement.FullSpan}");
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
            }
        }
    }
}
