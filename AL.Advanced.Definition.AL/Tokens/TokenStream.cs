using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AL.Advanced.Definition.AL.Tokens
{
    public class TokenStream
    {
        public bool CanRead => true;
        public bool CanSeek => true;
        public bool CanWrite => canWrite;
        public int Length => tokens.Length;
        public int Position { get; set; }

        public Token this[int index] => tokens[index];

        private bool canWrite;
        private Token[] tokens;

        public TokenStream(params Token[] tokens)
        {
            this.tokens = new Token[tokens.Length];

            Array.Copy(tokens, this.tokens, tokens.Length);

            canWrite = true;
            Position = 0;
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public int Read(Token[] buffer, int offset, int count)
        {
            var length = Position + offset + count > Length ? Length : count;
            Array.Copy(tokens, Position + offset, buffer, 0, length);
            Position += offset + length;
            return length;
        }

        internal List<TokenStream> Split(string definitonType)
        {
            var tmpList = new List<TokenStream>();
            var start = 0;
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Name != definitonType &&
                    i < tokens.Length - 1)
                    continue;

                var length = i - start + 1;

                if (length <= 1)
                {
                    start = 0;
                    continue;
                }

                var buffer = new Token[length];
                Array.Copy(tokens, start, buffer, 0, length);
                tmpList.Add(new TokenStream(buffer));

                start = i;
            }

            return tmpList;
        }

        public int Seek(int offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    return Position = offset;
                case SeekOrigin.Current:
                    return Position += offset;
                case SeekOrigin.End:
                    return Position = Length + offset;
                default:
                    throw new Exception("This should never happend");
            }
        }

        public void Write(Token[] buffer, int offset, int count)
        {
            Array.Copy(buffer, 0, tokens, Position, count);
            Position += count;
        }
    }
}