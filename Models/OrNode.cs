namespace CircuitMagieDeluxe.Models
{
    class OrNode : BaseNode
    {
        public OrNode() : base()
        {

        }

        public override void Calculate()
        {
            Output = false;

            foreach (bool input in Input)
            {
                if (input)
                {
                    Output = true;
                    break;
                }
            }
        }

        public override void RegisterAtFactory(NodeFactory factory)
        {
            factory.AddNodeType("Or", this);
        }

        protected override BaseNode CreateInstanceForClone()
        {
            return new OrNode();
        }
    }
}
