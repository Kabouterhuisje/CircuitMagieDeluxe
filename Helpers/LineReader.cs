using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CircuitMagieDeluxe.Helpers
{
    public sealed class LineReader : IEnumerable<string>
    {
        readonly Func<TextReader> dataSource;

        // LineReader maken op basis van de streamSource (UTF8 default)
        public LineReader(Func<Stream> streamSource)
            : this(streamSource, Encoding.UTF8)
        {
        }

        // LineReader maken op basis van de streamSource (custom encoding)
        public LineReader(Func<Stream> streamSource, Encoding encoding)
            : this(() => new StreamReader(streamSource(), encoding))
        {
        }

        // LineReader maken op basis van filename (UTF8 default)
        public LineReader(string filename)
            : this(filename, Encoding.UTF8)
        {
        }

        // LineReader maken op basis van filename (custom encoding)
        public LineReader(string filename, Encoding encoding)
            : this(() => new StreamReader(filename, encoding))
        {
        }

        // LineReader maken op basis van de textReaderSource
        public LineReader(Func<TextReader> dataSource)
        {
            this.dataSource = dataSource;
        }

        // Loop door alle lines heen
        public IEnumerator<string> GetEnumerator()
        {
            using (TextReader reader = dataSource())
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        // Roep enumerator aan om loop te starten
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
