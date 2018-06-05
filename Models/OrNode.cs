namespace CircuitMagieDeluxe.Models
{
    public class OrNode : BaseNode
    {
        public OrNode() : base()
        {

        }

        public OrNode(int propagationDelay)
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
