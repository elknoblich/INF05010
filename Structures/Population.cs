using INF05010.Extensions;

namespace INF05010.Structures;

public struct Population
{
    public Chromossome[] Chromossomes { get; set; }

    public Population(int populationChromossomes) => Chromossomes = new Chromossome[populationChromossomes];

    public Population(int populationChromossomes, Random randomizer, Parameters parameters, Professor[] professors, Student[] students)
    {
        Chromossomes = new Chromossome[populationChromossomes];
        Chromossomes = Chromossomes.Select(chromossome => new Chromossome(parameters.Distance, parameters.Hours, professors, students, randomizer)).ToArray();
    }

    public readonly (Chromossome, Chromossome) SelectPopulationChromossomes(Random randomizer)
    {
        Chromossome[] chromossomes = new Chromossome[randomizer.Next(2, Chromossomes.Length / 4)];

        for (int i = 0; i < chromossomes.Length; i++)
        {
            chromossomes[i] = Chromossomes[randomizer.Next(0, Chromossomes.Length)];
        }

        Chromossome chromossome1 = chromossomes[0];
        Chromossome chromossome2 = chromossomes[1];

        if (chromossome2.Solution < chromossome1.Solution)
        {
            (chromossome1, chromossome2) = (chromossome2, chromossome1);
        }

        foreach (Chromossome chromossome in chromossomes[2..])
        {
            if (chromossome.Solution < chromossome1.Solution)
            {
                chromossome1 = chromossome;
            }
            else if (chromossome.Solution < chromossome2.Solution)
            {
                chromossome2 = chromossome;
            }
        }

        return (chromossome1, chromossome2);
    }

    public readonly Chromossome SelectPopulationBestChromossome() => Chromossomes.SelectBestChromossome();
}