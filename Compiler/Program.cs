using Compiler.Core;
using Compiler.Core.Parser;
using Compiler.Core.Tokenizer;
using Compiler.Core.Translators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CAlConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenscanner = new TokenScanner(File.ReadAllText(@"C:\Temp\test\onRun.cs"));
            var scanner = new Scanner(SyntaxSource.ALSource, tokenscanner.Scan());
            var translator = new AlToAdvanced((ALSyntaxTree)scanner.Scan());
            var target = translator.Translate();

            File.WriteAllText(@"C:\Temp\test\result_onRun.cs", target.ToFullString());
        }
    }
}
