using INF05010.Services;
using INF05010.Structures;

namespace INF05010;

public class Program
{
    public static void Main(string[] arguments)
    {
        Console.Clear();
        Console.WriteLine("INF05010: Combinatorial Optimization\n");

        int seed = int.Parse(arguments[0]);
        int repetitions = int.Parse(arguments[1]);
        int populationChromossomes = int.Parse(arguments[2]);
        float crossOverRate = float.Parse(arguments[3]);
        float mutationRate = float.Parse(arguments[4]);

        var randomizer = new Random(seed);

        Console.WriteLine($"Seed: {seed}");
        Console.WriteLine($"Repetitions: {repetitions}");
        Console.WriteLine($"Population Chromossomes: {populationChromossomes}");
        Console.WriteLine($"Cross-Over Rate: {crossOverRate}");
        Console.WriteLine($"Mutation Rate: {mutationRate}\n");

        Array.ForEach(arguments[5..], file =>
        {
            Information information = FileReader.ReadInformation(file);

            Heuristic.Solve(repetitions, populationChromossomes, crossOverRate, mutationRate, randomizer, information.Parameters, information.Professors, information.Students);
        });
    }  
}