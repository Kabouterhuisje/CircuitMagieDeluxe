using CircuitMagieDeluxe.Helpers;
using System.Collections.Generic;

namespace CircuitMagieDeluxe.Models
{
    public class NodeFactory
    {
        private Dictionary<string, INode> _types;

        public NodeFactory()
        {
            _types = new Dictionary<string, INode>();
        }

        public void AddNodeType(string name, INode node)
        {
            name = name.ToLower();
            _types[name] = node;
        }

        public object CreateNode(string type)
        {
            INode node = _types[type].Clone();
            return HelperNode.Instance.SetDefaultInputNode(node, type);
        }
    }
}
