using System;
using System.IO;
using Al.Advanced.Definition.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpTests
{
    [TestClass]
    public class CodeUnit
    {
        [TestMethod]
        public void EmptyCodeunitCSharp()
        {
            var text = File.ReadAllText(@"..\..\..\..\examples\CSharp\CodeUnit.cs");

            var tree = CSharpTree.Parse(text);
            var txt = tree.ToText();

            Assert.IsTrue(txt.EndsWith("}"));
            Assert.IsTrue(txt.Length > 80);

            var evaluateA = File.ReadAllText(@"..\..\evaluation\CodeUnit_Empty.cs").Split(
                new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var evaluateB = txt.Split(
                new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual(evaluateA.Length, evaluateB.Length);

            for (int i = 0; i < evaluateA.Length; i++)
                Assert.AreEqual(evaluateA[i].Trim(), evaluateB[i].Trim());
        }
    }
}
