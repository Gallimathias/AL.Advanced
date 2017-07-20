using Compiler.Core;
using Compiler.Core.Parser;
using Compiler.Core.Tokenizer;
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
            //var scanner = new ALScanner(File.ReadAllText(@"C:\Temp\examples\function.cs"));
            //scanner.Scan();

            //var pas = new Parser(new ALScanner(File.ReadAllText(@"C:\Temp\examples\function.cs")));
            //pas.Parse();

            var tokenscanner = new TokenScanner(File.ReadAllText(@"C:\Temp\examples\function.cs"));
            var scanner = new Scanner(SyntaxSource.ALSource, tokenscanner.Scan());
            scanner.Scan();
        }
    }
}
