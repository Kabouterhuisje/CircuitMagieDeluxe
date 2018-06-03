namespace CircuitMagieDeluxe.Models
{
    class XorNode : BaseNode
    {
        public XorNode() : base()
        {

        }

        public XorNode(int propagationDelay)
        {
            this.PropogationDelay = propagationDelay;
        }

        public int accept(Visitor visitor)
        {
            return visitor.visit(this);
        }

        public override void Calculate()
        {
            Output = false;
            for (int i = 0; i < Input.Count - 1; i++)
            {
                bool? currentBool = Input[0];
                bool? nextBool = Input[1];
                if (currentBool != nextBool)
                {
                    Output = true;
                }
            }
        }

        public override void RegisterAtFactory(NodeFactory factory)
        {
            factory.AddNodeType("Xor", this);
        }

        protected override BaseNode CreateInstanceForClone()
        {
            return new XorNode();
        }
    }
}
