using System;
using System.IO;

namespace SpanVsStringPerformance
{
    /// <summary>
    /// Класс для тестирования занимаемой памяти
    /// </summary>
    public class InstanceCreator
    {
        public static string BaseDir = @"C:\Users\User\source\repos\SpanVsStringPerformance\SpanVsStringPerformance\";

        private SpanHolder _spanHolder;
        private StringHolder _stringHolder;
        private MemoryStream _memoryStream;
        private RawWordsColumn _wordsColumn;


        public static int Size = 30000000;

        public InstanceCreator()
        {
            WordsGenerator words = new WordsGenerator(Size, Path.Combine(BaseDir, "top_5000_words.txt"));
            var wordsColumn = new RawWordsColumn(words.GetEnumerator());

            _spanHolder = new SpanHolder(wordsColumn);
            _stringHolder = new StringHolder(wordsColumn);

            words = null;
            wordsColumn = null;

            GC.Collect(2, GCCollectionMode.Forced);

        }

    }
}
