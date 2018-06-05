using CircuitMagieDeluxe.Models;

namespace CircuitMagieDeluxe
{
    public class PropagationDelayVisitor : Visitor
    {
        public PropagationDelayVisitor()
        {

        }

        public int visit(NotNode NotNodeItem)
        {
            return (NotNodeItem.PropogationDelay) + 1;
        }

        public int visit(Probe ProbeItem)
        {
            return (ProbeItem.PropogationDelay) + 2;
        }

        public int visit(XorNode XorNodeItem)
        {
            return (XorNodeItem.PropogationDelay) + 3;
        }

        public int visit(OrNode OrNodeItem)
        {
            return (OrNodeItem.PropogationDelay) + 4;
        }

        public int visit(NorNode NorNodeItem)
        {
            return (NorNodeItem.PropogationDelay) + 5;
        }

        public int visit(NandNode NandNodeItem)
        {
            return (NandNodeItem.PropogationDelay) + 6;
        }

        public int visit(AndNode AndNodeItem)
        {
            return (AndNodeItem.PropogationDelay) + 7;
        }
    }
}
