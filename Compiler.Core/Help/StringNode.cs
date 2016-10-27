using System;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Core.Help
{
    public class StringNode
    {
        public int OrderNumber { get; set; }
        public string Text { get; set; }
        public string ClearText { get; private set; }
        public string[] Elements { get; private set; }
        public List<StringNode> Children { get; set; }

        public int Start { get { return value.Key; } }
        public int End { get { return value.Value; } }
        public int Length { get { return Text.Length; } }

        private KeyValuePair<int, int> value;

        public StringNode()
        {
            Children = new List<StringNode>();
            processText();
        }

        public StringNode(int key, KeyValuePair<int, int> value) : this()
        {
            OrderNumber = key;
            this.value = value;
        }

        public StringNode(int key, KeyValuePair<int, int> value, string nodetext) : this(key, value)
        {
            Text = nodetext;
        }

        public StringNode(int key, string nodetext) : this(key, new KeyValuePair<int, int>(0,0), nodetext)
        {
        }

        internal void Add(StringNode stringNode)
        {
            Children.Add(stringNode);
            processText();
        }

        private void processText()
        {
            var temp = Text;
            foreach (var item in Children)
                temp = temp.Remove(temp.IndexOf(item.Text, 0) - 1, item.Length + 2);

            ClearText = temp;

            Elements = temp?.Split().Where(c => !string.IsNullOrWhiteSpace(c)).ToArray();
        }

        public override string ToString() => $"{OrderNumber}:[{Start}:{End}] {ClearText}";

    }
}