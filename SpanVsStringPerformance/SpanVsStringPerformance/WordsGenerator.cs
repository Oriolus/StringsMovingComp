using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace SpanVsStringPerformance
{
    public class WordsGenerator : IEnumerable<string>
    {
        private int _wordCount;
        private string _fileName;

        private string[] _words;
        private Random _random;

        public WordsGenerator(int wordCount, string fileName)
        {
            _wordCount = wordCount;
            _fileName = fileName;

            using (var fileStream = File.OpenRead(_fileName))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    _words = reader.ReadToEnd().Split(' ');
                }
            }

            _random = new Random(Convert.ToInt32(DateTime.UtcNow.Millisecond));
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < _wordCount; i++)
            {
                yield return _words[_random.Next(0, _words.Length - 1)];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
