using System;

namespace CircuitMagieDeluxe.Models
{
    class AndNode : BaseNode, Visitable
    {
        public AndNode() : base()
        {

        }

        public AndNode(int propagationDelay)
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
