using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitMagieDeluxe
{
    class CircuitBuilder
    {
        public CircuitBuilder()
        {

        }

        public string ReadFile(String path)
        {
            string text;
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }

            return text;
        }
    }
}
