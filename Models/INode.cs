using System;
using System.Collections.Generic;

namespace CircuitMagieDeluxe.Models
{
    interface INode
    {
        String Id { get; set; }
        String Type { get; set; }
        int PropogationDelay { get; set; }
        List<bool> Input { get; set; }
        bool? Output { get; set; }
        List<INode> PreviousNodes { get; set; }
        List<INode> NextNodes { get; set; }

        bool GetResult();

        void Calculate();

        void RegisterAtFactory(NodeFactory factory);

        BaseNode Clone();
    }
}
