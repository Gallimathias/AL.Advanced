using Compiler.Core.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Parser
{
    public class Scanner
    {
        public SyntaxSource SyntaxSource { get; private set; }

        private MemberDeclarationSyntax[] tokens;
        private Dictionary<int, SyntaxParseDelegate> syntaxDictionary;
        private List<SyntaxMember> result;

        public Scanner(SyntaxSource source, params MemberDeclarationSyntax[] tokens)
        {
            this.tokens = tokens;
            syntaxDictionary = new Dictionary<int, SyntaxParseDelegate>();
            SyntaxSource = source;
            result = new List<SyntaxMember>();
            GetSnytax(source);
        }

        public Syntax.SyntaxTree Scan()
        {
            foreach (var token in tokens)
            {
               result.Add(Analyse(token));
            }

            return Syntax.SyntaxTree.GetTree(result, SyntaxSource);
        }

        private SyntaxMember Analyse(MemberDeclarationSyntax memberDeclaration)
        {
            foreach (var syntax in syntaxDictionary)
            {
                if (syntax.Value(memberDeclaration, Analyse, out SyntaxMember memberSyntax))
                    return memberSyntax;
            }

            throw new Exception($"{memberDeclaration.Kind()} [{memberDeclaration.RawKind}] " +
                $"is no valid {SyntaxSource} expression. On Span " +
                $"{memberDeclaration.FullSpan}");
            
        }

        private void GetSnytax(SyntaxSource source)
        {
            var typeList = Assembly.GetExecutingAssembly().GetTypes().Where(
                t => t.GetCustomAttributes<SyntaxSourceAttribute>()?.FirstOrDefault(
                        a => a.SyntaxSource == source) != null &&
                        !t.IsAbstract).ToArray();

            for (int i = 0; i < typeList.Length; i++)
            {
                if (Activator.CreateInstance(typeList[i]) is SyntaxMember memberSyntax)
                    syntaxDictionary.Add(i, memberSyntax.TryParse);
            }
        }

    }
}
