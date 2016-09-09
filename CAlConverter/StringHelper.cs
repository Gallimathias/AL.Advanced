using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAlConverter
{
    static class StringHelper
    {

        public static StringTree ParseElementTree(string text, char open, char close)
        {
            var openedStack = new Stack<KeyValuePair<int, int>>();
            var rawTree = new Dictionary<KeyValuePair<int, int>, KeyValuePair<int, int>>();
            var tempDic = new Dictionary<int, KeyValuePair<int, int>>();
            var resultTree = new StringTree();
            

            int openBrackets = 1;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == open)
                    openedStack.Push(new KeyValuePair<int, int>(openBrackets++, i + 1));


                if (text[i] == close)
                {
                    var openBracket = openedStack.Pop();
                    tempDic.Add(openBracket.Key, new KeyValuePair<int, int>(openBracket.Value, i));
                }
            }

            foreach (var item in tempDic)
            {
                var parent = tempDic.FirstOrDefault(b => b.Value.Key < item.Value.Key && b.Value.Value > item.Value.Value);

                rawTree.Add(new KeyValuePair<int, int>(parent.Key, item.Key), item.Value);

            }
            var count = rawTree.Count;

            resultTree.BaseNode = new StringNode(0, text);

            for (int i = 0; i < count; i++)
            {
                var temp = rawTree.OrderBy(n => n.Key.Key).First();
                rawTree.Remove(temp.Key);
                var nodetext = text.Substring(temp.Value.Key, temp.Value.Value - temp.Value.Key);
                //basetext = basetext.Where(t => t != nodetext.ToArray());

                resultTree.Add(temp.Key.Key, new StringNode(temp.Key.Value, temp.Value, nodetext));

            }
            
            return resultTree;
        }
    }
}