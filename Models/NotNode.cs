namespace CircuitMagieDeluxe.Models
{
    class NotNode : BaseNode
    {
        public NotNode() : base()
        {

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
