using System;
using System.IO;
using AL.Advanced.Definition.AL;
using AL.Advanced.Definition.AL.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ALTests
{
    [TestClass]
    public class CodeUnit
    {
        [TestMethod]
        public void EmptyCodeunitAL()
        {
            var text = File.ReadAllText(@"..\..\..\..\examples\AL\CodeUnit.al");

            var tree = ALTree.Parse(text);
            var txt = tree.ToText();

            Assert.IsTrue(txt.EndsWith("}"));
            Assert.IsTrue(txt.Length > 25);

            var evaluateA = File.ReadAllText(@"..\..\evaluation\CodeUnit_Empty.al").Split(
                new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var evaluateB = txt.Split(
                new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual(evaluateA.Length, evaluateB.Length);

            for (int i = 0; i < evaluateA.Length; i++)
                Assert.AreEqual(evaluateA[i].Trim(), evaluateB[i].Trim());
        }

        
    }
}
