using System;
using System.Collections.Generic;

namespace CircuitMagieDeluxe.Models
{
    public class BaseNode : INode
    {
        public String Id { get; set; }
        public String Type { get; set; }
        public int PropogationDelay { get; set; }
        public List<bool> Input { get; set; }
        public bool? Output { get; set; }
        public List<INode> PreviousNodes { get; set; }
        public List<INode> NextNodes { get; set; }

        public BaseNode()
        {
            PreviousNodes = new List<INode>();
            NextNodes = new List<INode>();
            Input = new List<bool>();
        }

        public virtual BaseNode Clone()
        {
            var bn = CreateInstanceForClone();
            bn.PreviousNodes = new List<INode>();
            bn.Input = new List<bool>();
            return bn;
        }

        protected virtual BaseNode CreateInstanceForClone()
        {
            return new BaseNode();
        }

        public bool GetResult()
        {
            if (Output == null)
            {
                if (Input.Count == 0)
                {
                    if (PreviousNodes.Count != 0)
                    {
                        foreach (var node in PreviousNodes)
                        {
                            Input.Add(node.GetResult());
                        }
                    }
                }
                this.Calculate();
            }
            return (bool)Output;
        }

        public virtual void Calculate() { }

        public virtual void RegisterAtFactory(NodeFactory factory) { }
    }
}
