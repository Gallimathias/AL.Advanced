using System;
using System.IO;
using Al.Advanced.Definition.CSharp;
using AL.Advanced.Definition.CSharpALFormatting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConvertingTests
{
    [TestClass]
    public class CodeUnit
    {
        [TestMethod]
        public void EmptyCodeunitALFormattingToCSharp()
        {
            var text = File.ReadAllText(@"..\..\..\..\examples\CSharp-ALFormatting\CodeUnit.cs");

            var tree = CSharpALTree.Parse(text);
            var target = tree.Convert<CSharpTree>();

            var result = target.ToText();

            //Assert.IsTrue(txt.EndsWith("}"));
            //Assert.IsTrue(txt.Length > 250);

            //var evaluateA = File.ReadAllText(@"..\..\evaluation\CodeUnit_Empty.cs").Split(
            //    new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            //var evaluateB = txt.Split(
            //    new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            //Assert.AreEqual(evaluateA.Length, evaluateB.Length);

            //for (int i = 0; i < evaluateA.Length; i++)
            //    Assert.AreEqual(evaluateA[i].Trim(), evaluateB[i].Trim());
        }
    }
}
