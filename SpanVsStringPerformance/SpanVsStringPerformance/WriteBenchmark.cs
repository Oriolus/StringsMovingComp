using BenchmarkDotNet.Attributes;
using System;
using System.IO;
using System.Runtime;

namespace SpanVsStringPerformance
{
    /// <summary>
    /// Дан файл (или иной IO источник) со строками, которые необходимо переложить в другие места
    /// Порядок входящий и исходящих строк может не совпадать
    /// В качестве источника используются MemoryStream чтобы не влиял реальный IO
    /// </summary>
    [MemoryDiagnoser]
    public class WriteBenchmark
    {
        public static string BaseDir = @"C:\Users\User\source\repos\SpanVsStringPerformance\SpanVsStringPerformance\";

        private SpanHolder _spanHolder;
        private StringHolder _stringHolder;
        private MemoryStream _memoryStream;
        private RawWordsColumn _wordsColumn;

        [Params(500000, 1000000, 10000000)]
        public int WordsSize { get; set; }

        public WriteBenchmark()
        {
            Setup();
        }

        [GlobalSetup]
        public void Setup()
        {
            WordsGenerator words = new WordsGenerator(WordsSize, Path.Combine(BaseDir, "top_5000_words.txt"));
            _wordsColumn = new RawWordsColumn(words.GetEnumerator());

            _spanHolder = new SpanHolder(_wordsColumn);
            _stringHolder = new StringHolder(_wordsColumn);

            _memoryStream = new MemoryStream(Convert.ToInt32(_wordsColumn.WordsStream.Length));

            words = null;
            //wordsColumn = null;

            GC.Collect(2, GCCollectionMode.Forced);

            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }

        [Benchmark]
        public void WriteWithStringHolder()
        {
            _memoryStream.Position = 0;
            using (StreamWriter streamWriter = new StreamWriter(_memoryStream, leaveOpen: true))
            {
                for (int i = 0; i < _stringHolder.WordCount; i++)
                {
                    streamWriter.Write(_stringHolder.GetString(i));
                    streamWriter.Write(' ');
                }
            }
        }

        [Benchmark]
        public void WriteWithSpanWriter()
        {
            _memoryStream.Position = 0;
            byte[] space = new byte[] { (byte)32 };
            for (int i = 0; i < _spanHolder.WordCount; i++)
            {
                _memoryStream.Write(_spanHolder.GetString(i));
                _memoryStream.Write(space, 0, 1);
            }
        }

    }
}
