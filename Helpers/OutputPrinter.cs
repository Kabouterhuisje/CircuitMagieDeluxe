using System.IO;
using System.Text;
using System.Windows.Controls;

namespace CircuitMagieDeluxe.Helpers
{
    public class OutputPrinter : TextWriter
    {
        TextBox output = null; 

        public OutputPrinter(TextBox _output)
        {
            output = _output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            output.AppendText(value.ToString());
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
