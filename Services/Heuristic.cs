using INF05010.Structures;

namespace INF05010.Services;

public static class Heuristic
{
    public static void Solve(string file, int repetitions, int populationChromossomes, float crossOverRate, float mutationRate, Random randomizer, Parameters parameters, Professor[] professors, Student[] students)
    {
        int i = 0;

        var population1 = new Population(populationChromossomes, randomizer, parameters, professors, students);

        int solution1 = population1.SelectPopulationBestChromossome().Solution;

        while (true)
        {
            int j = 0;

            var population2 = new Population(populationChromossomes);

            while (j < populationChromossomes)
            {
                var offspring = new Chromossome(professors.Length);

                (Chromossome chromossome1, Chromossome chromossome2) = population1.SelectPopulationChromossomes(randomizer);

                if (randomizer.NextDouble() < crossOverRate)
                {
                    offspring = chromossome1.CrossoverChromossome(chromossome2, professors, randomizer);
                }
                else
                {
                    offspring = chromossome1.Solution < chromossome2.Solution ? chromossome1 : chromossome2;
                }

                if (randomizer.NextDouble() < crossOverRate)
                {
                    offspring.MutateChromossome(randomizer);
                }

                if (offspring.ValidateChromossome(parameters.MinimumHours, parameters.MaximumDistance, professors, students))
                {
                    population2.Chromossomes[j] = offspring;

                    j++;
                }
            }

            int solution2 = population2.SelectPopulationBestChromossome().Solution;

            if (solution2 >= solution1)
            {
                i++;

                if (i == repetitions)
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

        DisplaySolution(file, solution1);
    }

    public static void DisplaySolution(string file, int solution) => Console.WriteLine($"File: {file}, Solution: {solution}");
}