namespace CircuitMagieDeluxe.Models
{
    class NotNode : BaseNode
    {
        public NotNode() : base()
        {

        }

        public NotNode(int propagationDelay)
        {
            this.PropogationDelay = propagationDelay;
        }

        public int accept(Visitor visitor)
        {
            return visitor.visit(this);
        }

        public override void Calculate()
        {
            Output = !Input[0];
        }

        public override void RegisterAtFactory(NodeFactory factory)
        {
            factory.AddNodeType("Not", this);
        }

        protected override BaseNode CreateInstanceForClone()
        {
            return new NotNode();
        }
    }
}
