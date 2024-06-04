using INF05010.Structures;

namespace INF05010.Extensions;

public static class ChromossomesExtensions
{
    public static Chromossome SelectBestChromossome(this Chromossome[] chromossomes)
    {
        Chromossome bestChromossome = chromossomes[0];

        int solutionA = bestChromossome.Solution, solutionB;

        foreach (Chromossome chromossome in chromossomes[2..])
        {
            solutionB = chromossome.Solution;

            if (solutionB < solutionA)
            {
                solutionA = solutionB;
                bestChromossome = chromossome;
            }
        }

        return bestChromossome;
    }
}