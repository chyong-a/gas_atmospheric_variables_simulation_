using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextFile;

namespace GasSimulation
{
    public class Simulation
    {
        #region Exceptions
        public class NoLayersException : Exception { }
        public class NoVariablesException : Exception { }
        public class LayerNotInList : Exception { }
        #endregion
        #region Attributes
        private List<Variable> variables;
        private List<Layer> layers;
        private int initialNumberOfLayers;
        private String variableString;
        #endregion
        #region Constructor
        public Simulation(String filename = "text.txt")
        {
            this.variables = new List<Variable>();
            this.layers = new List<Layer>();
            try
            {
                TextFileReader reader = new(filename);
                Console.WriteLine("The content of the input file:");
                if (reader.ReadInt(out int n))
                {
                    this.initialNumberOfLayers = n;
                }
                Console.WriteLine(n);
                int i = 0;
                for (; i<this.initialNumberOfLayers; i++)
                {
                    reader.ReadChar(out char name);
                    reader.ReadDouble(out double thickness);
                    switch (name)
                    {
                        case 'X':
                            this.layers.Add(new Oxygen(name, thickness));
                            break;
                        case 'Z':
                            this.layers.Add(new Ozone(name, thickness));
                            break;
                        case 'C':
                            this.layers.Add(new CarbonDioxide(name, thickness));
                            break;
                    }
                    Console.WriteLine(name + " " + thickness);
                }
                reader.ReadString(out String vars);
                this.variableString = vars;
                Console.WriteLine(vars);
                foreach (char v in this.variableString)
                {
                    switch (v)
                    {
                        case 'T':
                            this.variables.Add(ThunderStorm.Instance());
                            break;
                        case 'S':
                            this.variables.Add(Sunshine.Instance());
                            break;
                        case 'O':
                            this.variables.Add(Other.Instance());
                            break;
                    }
                }
            }
            catch (System.IO.FileNotFoundException) { Console.WriteLine("The file does not exist!"); }

        }
        #endregion
        #region Methods
        public void Simulate()
        {
            if (this.variables.Count == 0)
            {
                throw new NoVariablesException();
            }
            else
            if (this.layers.Count == 0)
            {
                throw new NoLayersException();
            }
            else
            {
                Console.WriteLine("Simulation starts");
                int MAXIMUM_NUMBER_OF_LAYERS = this.initialNumberOfLayers*3;
                int MINIMUM_NUMBER_OF_LAYERS = 3;
                int round = 0;
                // The program should continue the simulation until the number of layers is the triple of the initial number of layers or is less than three.
                while (this.layers.Count != MAXIMUM_NUMBER_OF_LAYERS && this.layers.Count >= MINIMUM_NUMBER_OF_LAYERS)
                {

                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    foreach (Variable v in this.variables)
                    {
                        for (int j = 0; j < this.layers.Count; j++)
                        {
                            Layer newLayer = this.layers[j].GetAffected(v);
                            if (newLayer != null)
                            {
                                bool shouldIncreaseJ = HandleNewLayer(newLayer, j);
                                if (!layers[j].StillExists())
                                {
                                    this.layers.RemoveAt(j);
                                }
                                if (shouldIncreaseJ)
                                {
                                    j++;
                                }
                            }
                        }
                    }
                    PrintAllLayers(); // printing after each round
                    round++;
                    Console.WriteLine("ROUND #" + round + ". NUMBER OF LAYERS IS " + this.layers.Count);
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                }
            }
        }
        // decide if the new layer goes up or merges
        private bool HandleNewLayer(Layer newLayer, int indexOfCurrentLayer)
        {
            bool result = false;
            if (IsThereIdenticalLayerAbove(newLayer, indexOfCurrentLayer).Item1) // there is the identical layer above
            {
                this.layers[IsThereIdenticalLayerAbove(newLayer, indexOfCurrentLayer).Item2].IncreaseThickness(newLayer.GetThickness());
            }
            else
            {
                if (newLayer.StillExists())
                {
                    this.layers.Insert(0, newLayer);
                    result = true; // to know if we need to increase index
                } // else it perishes
            }
            return result;
        }
        private (bool, int) IsThereIdenticalLayerAbove(Layer layer, int indexOfCurrentLayer)
        {
            bool l = false;
            int index = indexOfCurrentLayer;
            for (int i = indexOfCurrentLayer; !l && i>=0; i--)
            {
                if (layers[i].GetName() == layer.GetName())
                {
                    l = true;
                    index = i;
                }
            }
            return (l, index);
        }
        private void PrintAllLayers()
        {
            Console.WriteLine("-------------------------------");
            if (this.layers.Count > 0)
            {
                foreach (Layer layer in this.layers)
                {
                    Console.WriteLine(layer);
                }
            }
            else
            {
                Console.WriteLine("0 layers in the atmosphere.");
            }
            Console.WriteLine("-------------------------------");
        }
        #endregion
    }
}
