using AL.Advanced.Core;
using AL.Advanced.Core.Definition;
using AL.Advanced.Definition.AL.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AL.Advanced.Definition.AL
{
    public class ALTree : Tree
    {
        protected ALTree(UnitRoot unitRoot) : base(unitRoot)
        {
        }


        public static ALTree Parse(string text)
        {
            if (TryParse(text, out ALTree tree))
                return tree;

            throw new Exception("Could not parse text");
        }

        public static bool TryParse(string text, out ALTree tree)
        {
            var tokenizer = new Tokenizer(GetDefinitionFromAssembly());
            var tokenStream = tokenizer.Parse(text);
            var scanner = new ALScanner();
            var tmpList = new List<Member>();
            var splitStreams = tokenStream.Split("ClassType");

            foreach (var classType in splitStreams)
            {
                if(scanner.TryScan(classType, out Member root))
                {
                    tmpList.Add(root);
                }
                else
                {
                    tree = null;
                    return false;
                }
            }

            tree = new ALTree(new UnitRoot(tmpList));
            return true;
        }

        public override void CopyRoot(UnitRoot unitRoot)
        {
            var scanner = new ALScanner();

            foreach (var member in unitRoot.Members)
            {
                if (scanner.TryGetCopy(member, out Member newMember))
                    UnitRoot.Members.Add(newMember);
                else
                    throw new Exception("Can not copy Member");
            }
        }

        private static List<TokenDefinition> GetDefinitionFromAssembly()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                "AL.Advanced.Definition.AL.Tokens.TokenDefinitions.json"))
            {
                using (var textReader = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<List<TokenDefinition>>(textReader.ReadToEnd());
                }
            }
        }
    }
}
