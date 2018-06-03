namespace CircuitMagieDeluxe.Models
{
    class NorNode : BaseNode
    {
        public NorNode() : base()
        {

        }

        public NorNode(int propagationDelay)
        {
            this.PropogationDelay = propagationDelay;
        }

        public int accept(Visitor visitor)
        {
            return visitor.visit(this);
        }

        public override void Calculate()
        {
            Output = true;
            foreach (bool input in Input)
            {
                if (input)
                {
                    Output = false;
                    break;
                }
            }
        }

        public override void RegisterAtFactory(NodeFactory factory)
        {
            factory.AddNodeType("Nor", this);
        }

        protected override BaseNode CreateInstanceForClone()
        {
            return new NorNode();
        }
    }
}
