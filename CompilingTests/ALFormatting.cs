using System;
using System.IO;
using AL.Advanced.Definition.CSharpALFormatting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompilingTests
{
    [TestClass]
    public class ALFormatting
    {
        [TestMethod]
        public void CompileExample()
        {
            var text = File.ReadAllText(@"..\..\..\examples\CSharp-ALFormatting\CodeUnit.cs");

            CSharpALTree.Parse(text);
        }
    }
}
