using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace SpanVsStringPerformance
{
    public class RawWordsColumn
    {
        public int WordsCount { get; private set; }
        public MemoryStream WordsStream { get; private set; }

        public RawWordsColumn(IEnumerator<string> words)
        {
            WordsStream = new MemoryStream();
            
            while (words.MoveNext())
            {
                WordsStream.Write(new ReadOnlySpan<byte>(Encoding.ASCII.GetBytes(words.Current)));
                WordsStream.WriteByte(0);
                WordsCount++;
            }
            WordsStream.Position = 0;
        }

    }
}
