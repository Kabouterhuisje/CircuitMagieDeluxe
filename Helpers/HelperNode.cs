using CircuitMagieDeluxe.Models;

namespace CircuitMagieDeluxe.Helpers
{
    class HelperNode
    {
        private static HelperNode instance;

        private HelperNode() { }

        public static HelperNode Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HelperNode();
                }
                return instance;
            }
        }

        public INode SetDefaultInputNode(INode node, string type)
        {
            if (type == "input_high")
            {
                node.Input.Add(true);
            }
            else if (type == "input_low")
            {
                node.Input.Add(false);
            }
            return node;
        }
    }
}
