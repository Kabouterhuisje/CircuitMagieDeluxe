using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitMagieDeluxe.Models
{
    interface Visitor 
    {
        int visit(AndNode AndNodeItem);
        int visit(NandNode NandNodeItem);
        int visit(NorNode NorNodeItem);
        int visit(NotNode NotNodeItem);
        int visit(OrNode OrNodeItem);
        int visit(Probe ProbeItem);
        int visit(XorNode XorNodeItem);
    }
}
