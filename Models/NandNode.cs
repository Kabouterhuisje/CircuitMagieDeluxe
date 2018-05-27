namespace CircuitMagieDeluxe.Models
{
    class NandNode : BaseNode
    {
        public NandNode() : base()
        {

        }

        public override void Calculate()
        {
            Output = false;
            foreach (bool input in Input)
            {
                if (!input)
                {
                    Output = true;
                    break;
                }
            }
        }

        public override void RegisterAtFactory(NodeFactory factory)
        {
            factory.AddNodeType("Nand", this);
        }

        protected override BaseNode CreateInstanceForClone()
        {
            return new NandNode();
        }
    }
}
