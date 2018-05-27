namespace CircuitMagieDeluxe.Models
{
    class AndNode : BaseNode
    {
        public AndNode() : base()
        {

        }

        public override void Calculate()
        {
            Output = true;
            foreach (bool input in Input)
            {
                if (!input)
                {
                    Output = false;
                    break;
                }
            }
        }

        public override void RegisterAtFactory(NodeFactory factory)
        {
            factory.AddNodeType("And", this);
        }

        protected override BaseNode CreateInstanceForClone()
        {
            return new AndNode();
        }
    }
}
