using CircuitMagieDeluxe.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CircuitMagieDeluxe
{
    class CircuitSimulator
    {
        private Dictionary<string, string> _nodes;
        private Dictionary<string, List<string>> _edges;
        private List<INode> AllNodes;
        private List<INode> EndNodes;
        private List<INode> StartNodes;
        private NodeFactory NodeFactory;
        private PropagationDelayVisitor delayCalc = new PropagationDelayVisitor();
        private AndNode AND = new AndNode(15);
        private NandNode NAND = new NandNode(15);
        private NorNode NOR = new NorNode(15);
        private NotNode NOT = new NotNode(15);
        private OrNode OR = new OrNode(15);
        private Probe PROBE = new Probe(15);
        private XorNode XOR = new XorNode(15);

        public CircuitSimulator()
        {
            NodeFactory = new NodeFactory();
            AllNodes = new List<INode>();
            EndNodes = new List<INode>();
            StartNodes = new List<INode>();
        }

        public void BuildCircuit(Dictionary<string, string> nodes, Dictionary<string, List<string>> edges)
        {
            _nodes = nodes;
            _edges = edges;
            CollectAllNodeTypes();
            Console.WriteLine("Circuit bouwen..");
            CreateNodes();
        }

        public void ResetCircuit()
        {
            if (_nodes != null)
            {
                _nodes.Clear();
            }              
            if (_edges != null)
            {
                _edges.Clear();
            }      
            if (AllNodes != null)
            {
                AllNodes.Clear();
            }           
            if (StartNodes != null)
            {
                StartNodes.Clear();
            }  
            if (EndNodes != null)
            {
                EndNodes.Clear();
            }
        }

        // Voeg alle node types toe aan de factory
        public void CollectAllNodeTypes()
        {
            var type = typeof(BaseNode);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);
            foreach (var nodeItem in types)
            {
                INode nodeToRegister = (INode)Activator.CreateInstance(nodeItem);
                nodeToRegister.RegisterAtFactory(NodeFactory);
            }
        }

        public void CreateNodes()
        {
            foreach (KeyValuePair<string, string> nodeItemStrings in _nodes)
            {
                INode newNode = (INode)NodeFactory.CreateNode(nodeItemStrings.Value);
                newNode.Id = nodeItemStrings.Key;
                newNode.Type = nodeItemStrings.Value;
                if(nodeItemStrings.Value == "and") {
                    newNode.PropogationDelay = AND.accept(delayCalc);
                }
                else if (nodeItemStrings.Value == "nand") {
                    newNode.PropogationDelay = NAND.accept(delayCalc);
                }
                else if (nodeItemStrings.Value == "nor") {
                    newNode.PropogationDelay = NOR.accept(delayCalc);
                }
                else if (nodeItemStrings.Value == "not") {
                    newNode.PropogationDelay = NOT.accept(delayCalc);
                }
                else if (nodeItemStrings.Value == "or") {
                    newNode.PropogationDelay = OR.accept(delayCalc);
                }
                else if (nodeItemStrings.Value == "probe") {
                    newNode.PropogationDelay = PROBE.accept(delayCalc);
                }
                else if (nodeItemStrings.Value == "xor") {
                    newNode.PropogationDelay = XOR.accept(delayCalc);
                }
                else {
                    newNode.PropogationDelay = 15;
                }
                AllNodes.Add(newNode);
            }

            // Lin edges aan nodes
            foreach (var node in AllNodes)
            {
                List<string> linkedEdges;
                if (_edges.TryGetValue(node.Id, out linkedEdges))
                {
                    foreach (var linkedEdgeString in linkedEdges)
                    {
                        INode linkedNode = AllNodes.First(item => item.Id == linkedEdgeString);
                        linkedNode.PreviousNodes.Add(node);
                        node.NextNodes.Add(linkedNode);
                    }
                }
                else
                {
                    Console.WriteLine("Final node: " + node.Id);
                    EndNodes.Add(node);
                }
            }

            bool noErrors = true;

            // Check op fouten m.b.t connecties
            foreach (var node in AllNodes)
            {
                // Probe moet PreviousNodes of NextNodes hebben, Alle andere nodes moeten zowel PreviousNodes als NextNodes hebben
                if (node.GetType() == typeof(Probe) && (node.PreviousNodes.Count == 0 && node.NextNodes.Count == 0) || node.GetType() != typeof(Probe) && (node.PreviousNodes.Count == 0 || node.NextNodes.Count == 0))
                {
                    Console.WriteLine("Er is een node binnen je circuit aanwezig die een foute connectie heeft!");
                    noErrors = FoundError();
                    break;
                }
            }

            // Check op infinite loops
            foreach (var node in EndNodes)
            {
                List<INode> routeVisited = new List<INode>();
                if (!HasNoInfiniteLoop(node, routeVisited))
                {
                    Console.WriteLine("Er is een infinite loop binnen je circuit aanwezig!");
                    noErrors = FoundError();
                    break;
                }
            }
            if (noErrors)
            {
                Console.WriteLine("Nodes en edges zijn aangemaakt en gelinkt!");
            }                
        }

        public bool FoundError()
        {
            Console.WriteLine("Er is een fout opgetreden binnen je circuit, bouwen is niet gelukt!");
            ResetCircuit();
            return false;
        }

        // Find Infinite Loop using FloodFill
        public bool HasNoInfiniteLoop(INode node, List<INode> routeVisited)
        {
            if (node.PreviousNodes.Count == 0)
            {
                return true;
            }
            else if (routeVisited.Contains(node))
            {
                // Infinite loop
                return false;
            }

            routeVisited.Add(node);
            List<INode> cloneRouteVisited = new List<INode>();
            cloneRouteVisited.AddRange(routeVisited);
            foreach (var nodeItem in node.PreviousNodes)
            {
                if (!HasNoInfiniteLoop(nodeItem, cloneRouteVisited))
                {
                    // Infinite loop
                    return false;
                }
            }
            // No infinite loop
            return true;
        }

        // Simulate circuit
        public void StartSimulation()
        {
            if (EndNodes == null || EndNodes.Count == 0)
            {
                Console.WriteLine("Het circuit is nog niet gebouwd!");
            }
            else
            {
                Console.WriteLine("Simulatie wordt gestart!");
                foreach (var finalNode in EndNodes)
                {
                    if (finalNode.PreviousNodes != null)
                    {
                        Console.WriteLine("Laatste node: " + finalNode.Id + " = " + finalNode.GetResult());
                    }
                    else
                    {
                        Console.WriteLine("Er is iets fout gegaan! Geen PreviousNodes gevonden voor node: " + finalNode.Id);
                    }
                }
            }

        }

        public List<INode> GetStartNodes()
        {
            if (StartNodes.Count == 0)
            {
                foreach (var node in AllNodes)
                {
                    if (node.PreviousNodes.Count == 0)
                    {
                        StartNodes.Add(node);
                    }
                }
            }
            return StartNodes;
        }

        public void ResetNodes()
        {
            foreach (var node in AllNodes)
            {
                node.Input.Clear();
                node.Output = null;
                Helpers.HelperNode.Instance.SetDefaultInputNode(node, node.Type);
            }
        }
    }
}
