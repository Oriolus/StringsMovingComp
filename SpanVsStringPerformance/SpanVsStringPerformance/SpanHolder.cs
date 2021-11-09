using System;

namespace SpanVsStringPerformance
{
    public class SpanHolder
    {
        private byte[] _storage;
        private int[] _word_indeces;

        public int WordCount => _word_indeces.Length - 2;

        public SpanHolder(RawWordsColumn wordsColumn)
        {
            _word_indeces = new int[wordsColumn.WordsCount + 2];
            _storage = wordsColumn.WordsStream.ToArray();

            int index_found = 0, last_index = 0, index = 0;
            _word_indeces[index++] = 0;
            while ((index_found = Array.IndexOf<byte>(_storage, 0, last_index)) > -1)
            {
                index_found++;
                _word_indeces[index++] = index_found;
                last_index = index_found;
            }
            _word_indeces[index++] = _storage.Length;

        }

        public ReadOnlySpan<byte> GetString(int index)
        {
            if (index > WordCount) throw new IndexOutOfRangeException(nameof(index));
            int from = _word_indeces[index];
            int to = _word_indeces[index + 1];
            return new ReadOnlySpan<byte>(_storage, from, to - from - 1);
        }

    }
}
