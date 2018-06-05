using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CircuitMagieDeluxe
{
    public class CircuitBuilder
    {
        private Dictionary<string, string> Nodes;
        private Dictionary<string, List<string>> Edges;
        private CircuitSimulator circuit { get; set; }
        public CircuitBuilder()
        {
            Nodes = new Dictionary<string, string>();
            Edges = new Dictionary<string, List<string>>();
            this.circuit = new CircuitSimulator();
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

        public CircuitSimulator BuildHashmap(string text) 
        {
            circuit.ResetCircuit();

            foreach (string line in new Helpers.LineReader(() => new StringReader(text)))
            {
                string newLine = line;
                if (!(string.IsNullOrEmpty(newLine) || newLine[0] == '#' || !newLine.ToLower().Contains(':') || !newLine.ToLower().Contains(';')))
                {
                    // Tabs en spaties filteren
                    var charsToRemove = new string[] { "\t", " ", ";" };

                    foreach (var c in charsToRemove)
                    {
                        newLine = newLine.Replace(c, string.Empty);
                        newLine = newLine.ToLower(); // All Lower Case
                    }

                    // Splitten op ":"
                    List<string> nodeDescription = newLine.Split(':').ToList();

                    // Checken op node
                    if (!Nodes.ContainsKey(nodeDescription[0]))
                    {
                        // Node toevoegen
                        Nodes.Add(nodeDescription[0], nodeDescription[1]);
                    }            
                    else // Edge
                    {
                        List<string> edgeDescription;
                        if (nodeDescription[1].ToLower().Contains(','))    // Meerdere edges tussen nodes
                        {
                            edgeDescription = nodeDescription[1].Split(',').ToList();
                        }
                        else                                           
                        {
                            edgeDescription = new List<string>();
                            edgeDescription.Add(nodeDescription[1]);
                        }
                        Edges.Add(nodeDescription[0], edgeDescription);
                    }
                }
            }

            // Build circuit
            circuit.BuildCircuit(Nodes, Edges);
            return circuit;
        }
    }
}
