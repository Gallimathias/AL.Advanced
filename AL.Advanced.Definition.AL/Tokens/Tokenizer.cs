using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AL.Advanced.Definition.AL.Tokens
{
    public class Tokenizer
    {
        public List<TokenDefinition> Definitions { get; private set; }

        public Tokenizer(List<TokenDefinition> definitions)
        {
            Definitions = definitions;
        }

        public TokenStream Parse(string input)
        {
            var list = new List<Token>();
            var expectedGroup = 0;

            for (int i = 0; i < input.Length;)
            {
                bool isSuccess = false;

                foreach (var definition in Definitions.Where(d => d.Group == expectedGroup || d.Group < 0))
                {
                    Match result = null;
                    foreach (var expression in definition.Expressions)
                    {
                        result = expression.Match(input, i);

                        if (result.Success)
                            break;
                    }

                    if (result == null)
                        continue;

                    if (!result.Success || result.Index != i)
                        continue;

                    isSuccess = true;
                    i += result.Length;

                    if (definition.ChildrenGroup > -1)
                        expectedGroup = definition.ChildrenGroup;

                    if (!definition.Skip)
                        list.Add(new Token(result, definition));

                    break;
                }

                if (!isSuccess)
                    throw new Exception("Invalid expression");
            }

            return new TokenStream(list.ToArray());
        }
    }
}
