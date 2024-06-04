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
        var random = new Random(seed);

        Console.WriteLine($"Seed: {seed}");
        Console.WriteLine($"Repetitions: {repetitions}");
        Console.WriteLine($"Population Chromossomes: {populationChromossomes}");
        Console.WriteLine($"Cross-Over Rate: {crossOverRate}");
        Console.WriteLine($"Mutation Rate: {mutationRate}\n");

        var fileReader = new FileReader(arguments[5..]);

        Parameters[] parameters = fileReader.ReadParametersFromFile();
        Professor[][] professors = fileReader.ReadProfessorsFromFile(parameters);
        Student[][] students = fileReader.ReadStudentsFromFile(parameters);

        for (int i = 0; i < parameters.Length; i++)
        {
            var heuristic = new Heuristic(parameters[i].File, repetitions, populationChromossomes, crossOverRate, mutationRate, random, parameters[i], professors[i], students[i]);

            heuristic.Solve();
        }
    }  
}