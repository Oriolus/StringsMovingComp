using System;
using System.Text;

namespace SpanVsStringPerformance
{
    public class StringHolder
    {
        string[] _storage;

        public int WordCount => _storage.Length;

        public StringHolder(RawWordsColumn column)
        {
            _storage = new string[column.WordsCount];
            var bytes = column.WordsStream.ToArray();

            int index_found = 0, last_index = 0, index = 0;
            while ((index_found = Array.IndexOf<byte>(bytes, 0, last_index)) > -1)
            {
                _storage[index++] = Encoding.UTF8.GetString(bytes, last_index, index_found - last_index);

                index_found++;
                last_index = index_found;
            }
        }

        public string GetString(int index)
        {
            if (index > WordCount) throw new IndexOutOfRangeException(nameof(index));
            return _storage[index];
        }
    }
}
