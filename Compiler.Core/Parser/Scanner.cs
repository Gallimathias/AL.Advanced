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

        public Scanner(SyntaxSource source, params MemberDeclarationSyntax[] tokens)
        {
            this.tokens = tokens;
            syntaxDictionary = new Dictionary<int, SyntaxParseDelegate>();
            SyntaxSource = source;
            GetSnytax(source);
        }

        public void Scan()
        {
            foreach (var token in tokens)
            {
                Analyse(token);
            }
        }

        private MemberSyntax Analyse(MemberDeclarationSyntax memberDeclaration)
        {
            foreach (var syntax in syntaxDictionary)
            {
                if (syntax.Value(memberDeclaration, Analyse, out MemberSyntax memberSyntax))
                    return memberSyntax;
            }

            throw new Exception($"{ memberDeclaration } is no valid expression");
        }

        private void GetSnytax(SyntaxSource source)
        {
            var typeList = Assembly.GetExecutingAssembly().GetTypes().Where(
                t => t.GetCustomAttributes<SyntaxSourceAttribute>()?.FirstOrDefault(
                        a => a.SyntaxSource == source) != null &&
                        !t.IsAbstract).ToArray();

            for (int i = 0; i < typeList.Length; i++)
            {
                if (Activator.CreateInstance(typeList[i]) is MemberSyntax memberSyntax)
                    syntaxDictionary.Add(i, memberSyntax.TryParse);
            }
        }

    }
}
