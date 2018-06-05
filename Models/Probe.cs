using System;

namespace CircuitMagieDeluxe.Models
{
   public class Probe : BaseNode
    {
        public Probe() : base()
        {

        }

        public Probe(int propagationDelay)
        {
            this.PropogationDelay = propagationDelay;
        }

        public int accept(Visitor visitor)
        {
            return visitor.visit(this);
        }

        public override void Calculate()
        {
            if (Input.Count == 1)
            {
                Output = Input[0];
            }
            else
            {
                Console.WriteLine("Probe kan niet meer dan 1 input hebben!");
            }
        }

        public override void RegisterAtFactory(NodeFactory factory)
        {
            factory.AddNodeType("Probe", this);
            factory.AddNodeType("INPUT_LOW", this); 
            factory.AddNodeType("INPUT_HIGH", this); 
        }

        protected override BaseNode CreateInstanceForClone()
        {
            return new Probe();
        }
    }
}
