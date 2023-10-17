using static GasSimulation.Simulation;

namespace GasSimulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Simulation simulation = new Simulation();
            try
            {
                simulation.Simulate();
            } catch (NoVariablesException)
            {
                Console.WriteLine("Variable string is empty");
            } catch (NoLayersException)
            {
                Console.WriteLine("0 layers were provided");
            }
        }
    }
}