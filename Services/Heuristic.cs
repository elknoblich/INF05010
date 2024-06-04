using INF05010.Structures;

namespace INF05010.Services;

public class Heuristic(string file, int repetitions, int populationChromossomes, float crossOverRate, float mutationRate, Random randomizer, Parameters parameters, Professor[] professors, Student[] students)
{
    public string File = file;
    public int Repetitions = repetitions;
    public int PopulationChromossomes = populationChromossomes;
    public float CrossOverRate = crossOverRate;
    public float MutationRate = mutationRate;
    public Random Randomizer = randomizer;
    public Parameters Parameters = parameters;
    public Professor[] Professors = professors;
    public Student[] Students = students;
    
    public void Solve()
    {
        int i = 0;

        var population1 = new Population(PopulationChromossomes, Randomizer, Parameters, Professors, Students);

        int solution1 = population1.SelectPopulationBestChromossome().Solution;

        while (true)
        {
            int j = 0;

            var population2 = new Population(PopulationChromossomes);

            while (j < PopulationChromossomes)
            {
                var offspring = new Chromossome(Professors.Length);

                (Chromossome chromossome1, Chromossome chromossome2) = population1.SelectPopulationChromossomes(Randomizer);

                if (Randomizer.NextDouble() < CrossOverRate)
                {
                    offspring = chromossome1.CrossoverChromossome(chromossome2, Professors, Randomizer);
                }
                else
                {
                    offspring = chromossome1.Solution < chromossome2.Solution ? chromossome1 : chromossome2;
                }

                if (Randomizer.NextDouble() < MutationRate)
                {
                    offspring.MutateChromossome(Randomizer);
                }

                if (offspring.ValidateChromossome(Parameters.Hours, Parameters.Distance, Professors, Students))
                {
                    population2.Chromossomes[j] = offspring;

                    j++;
                }
            }

            int solution2 = population2.SelectPopulationBestChromossome().Solution;

            if (solution2 >= solution1)
            {
                i++;

                if (i == Repetitions)
                {
                    break;
                }
            }
            else
            {
                solution1 = solution2;

                i = 0;
            }

            population1 = population2;
        }

        DisplaySolution(solution1);
    }

    public void DisplaySolution(int solution) => Console.WriteLine($"File: {File}, Solution: {solution}");
}