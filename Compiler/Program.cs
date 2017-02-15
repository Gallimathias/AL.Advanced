using Compiler.Core;
using Compiler.Core.AL;
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
            var pas = new Parser(new ALScanner(File.ReadAllText(@"C:\Temp\examples\function.cs")));
            pas.Parse();
        }
    }
}
