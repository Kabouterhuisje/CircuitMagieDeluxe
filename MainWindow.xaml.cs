using CircuitMagieDeluxe.Helpers;
using CircuitMagieDeluxe.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CircuitMagieDeluxe
{
    public partial class MainWindow : Window
    {
        private int nodeSize = 60;
        private int nodeMarginY = 120;
        private int nodeMarginX = 120;
        private CircuitBuilder circuitBuilder = new CircuitBuilder();
        private CircuitSimulator circuit;
        private ObservableCollection<CheckboxManager> observableNodes { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            observableNodes = new ObservableCollection<CheckboxManager>();
            circuitBuilder = new CircuitBuilder();
            TextWriter writer = new OutputPrinter(output_Text);
            Console.SetOut(writer);
        }

        private void BuildCircuit(object sender, RoutedEventArgs e)
        {
            string path = "";
            OpenFileDialog file = new OpenFileDialog();
            bool? result = file.ShowDialog();

            if (result ?? false)
            {
                path = file.FileName;
            }
            if (!path.Equals(""))
            {
                output_Text.Text = String.Empty;
                circuit = circuitBuilder.BuildHashmap(circuitBuilder.ReadFile(path));
                AddCheckboxes(circuit.GetStartNodes());
                GenerateCircuit(circuit);
            }
            else
            {
                Console.WriteLine("Error: " + path);
            }
        }

        private void SimulateCircuit(object sender, RoutedEventArgs e)
        {
            output_Text.Text = String.Empty;
            circuit.StartSimulation();
            GenerateCircuit(circuit);
        }

        private void AddCheckboxes(List<INode> startnodes)
        {
            input_List.ItemsSource = observableNodes;
            observableNodes.Clear();

            foreach (INode node in startnodes)
            {
                observableNodes.Add(new CheckboxManager(node, circuit));
            }
        }

        private void GenerateCircuit(CircuitSimulator circuit)
        {
            Canvas.Children.Clear(); 
            List<INode> startNodes = circuit.GetStartNodes();
            Dictionary<string, UIElement> nodesDone = new Dictionary<string, UIElement>();
            List<string> connectionsDone = new List<string>();
            int x = 0;
            int y = 0;
            foreach (INode node in startNodes)
            {
                DrawNode(node, nodesDone, x, y, connectionsDone);
                y += nodeMarginY;
            }
        }

        private void DrawNode(INode node, Dictionary<string, UIElement> nodesPassed, int x, int y, List<string> connectionsPassed)
        {
            if (!nodesPassed.Keys.Contains(node.Id))
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Name = node.Id;
                rectangle.Width = nodeSize;
                rectangle.Height = nodeSize;
                rectangle.Stroke = Brushes.Black;
                rectangle.StrokeThickness = 1;
                rectangle.Fill = new SolidColorBrush(Colors.AliceBlue);
                Canvas.Children.Add(rectangle);
                Canvas.SetTop(rectangle, y);
                Canvas.SetLeft(rectangle, x);
                TextBlock textBlock = new TextBlock();
                textBlock.Text = node.Id + "\n" + node.Type;
                textBlock.Foreground = Brushes.Black;
                Canvas.Children.Add(textBlock);
                Canvas.SetTop(textBlock, y + 15);
                Canvas.SetLeft(textBlock, x);

                if (node.Output != null)
                {
                    TextBlock resultTextBlock = new TextBlock();
                    if (node.Output == true)
                    {
                        resultTextBlock.Text = "true";
                        rectangle.Fill = new SolidColorBrush(Colors.LimeGreen);
                    }    
                    else
                    {
                        resultTextBlock.Text = "false";
                        rectangle.Fill = new SolidColorBrush(Colors.IndianRed);
                    }   
                    resultTextBlock.Foreground = Brushes.Black;
                    Canvas.Children.Add(resultTextBlock);
                    Canvas.SetTop(resultTextBlock, y + 0);
                    Canvas.SetLeft(resultTextBlock, x + 6);
                }
                nodesPassed.Add(node.Id, rectangle);
            }
            if (node.NextNodes.Count != 0)
            {
                x += nodeMarginX;
                foreach (INode nextNode in node.NextNodes)
                {
                    DrawNode(nextNode, nodesPassed, x, y, connectionsPassed);
                    DrawConnection(node.Id, nextNode.Id, nodesPassed, connectionsPassed);
                    y += nodeMarginY;
                }
            }
        }

        private void DrawConnection(string nodeName, string nextNodeName, Dictionary<string, UIElement> drawnNodes, List<string> connectionDone)
        {

            if (!connectionDone.Contains(nodeName + nextNodeName))
            {
                double x1 = Canvas.GetLeft(drawnNodes[nodeName]);
                double y1 = Canvas.GetTop(drawnNodes[nodeName]);
                double x2 = Canvas.GetLeft(drawnNodes[nextNodeName]);
                double y2 = Canvas.GetTop(drawnNodes[nextNodeName]);

                Line nodeConnection = new Line();
                nodeConnection.Stroke = Brushes.Black;
                nodeConnection.StrokeThickness = 0.2;
                nodeConnection.X1 = x1 + (nodeSize / 2);
                nodeConnection.Y1 = y1 + (nodeSize / 2);
                nodeConnection.X2 = x2 + (nodeSize / 2);
                nodeConnection.Y2 = y2 + (nodeSize / 2);
                Canvas.Children.Add(nodeConnection);
                connectionDone.Add(nodeName + nextNodeName);
            }

        }
    }
}
