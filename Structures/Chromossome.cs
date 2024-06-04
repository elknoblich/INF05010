using INF05010.Extensions;

namespace INF05010.Structures;

public struct Chromossome
{
    public byte[] Values { get; set; }
    public int Solution { get; set; }

    public Chromossome(int lenght) => Values = new byte[lenght];

    public Chromossome(float distance, int hours, Professor[] professors, Student[] students, Random randomizer)
    {
        do
        {
            Values = Enumerable.Range(0, professors.Length).Select(_ => (byte) randomizer.Next(2)).ToArray();
        }
        while (!ValidateChromossome(hours, distance, professors, students));
    }

    public bool ValidateChromossome(int hours, float distance, Professor[] professors, Student[] students)
    {
        bool output = false;

        for (int i = 0; i < Values.Length; i++)
        {    
            if (Values[i] == 1)
            {
                for (int j = 0; j < students.Length; j++)
                {
                    if (Math.Sqrt(Math.Pow(professors[i].Coordinate.X - students[j].Coordinate.X, 2) + Math.Pow(professors[i].Coordinate.Y - students[j].Coordinate.Y, 2)) <= distance)
                    {
                        students[j].Selected = 1;
                    }
                }
            }           
        }

        if (students.Hours() >= hours)
        {
            Solution = professors.Salary(Values);

            output = true;
        }

        students.Reset();

        return output;
    }

    public readonly void MutateChromossome(Random randomizer) => Values[randomizer.Next(0, Values.Length)] ^= 1;

    public readonly Chromossome CrossoverChromossome(Chromossome chromossome, Professor[] professors, Random randomizer)
    {
        var offsrping = new Chromossome(professors.Length);

        int pivot = randomizer.Next(0, Values.Length);

        if (randomizer.Next(0, 2) == 0)
        {
            for (int i = 0; i <= pivot; i++)
            {
                offsrping.Values[i] = Values[i];
            }

            for (int i = Values.Length - 1; i >= pivot; i--)
            {
                offsrping.Values[i] = chromossome.Values[i];
            }
        }
        else
        {
            for (int i = 0; i <= pivot; i++)
            {
                offsrping.Values[i] = chromossome.Values[i];
            }

            for (int i = Values.Length - 1; i >= pivot; i--)
            {
                offsrping.Values[i] = Values[i];
            }
        }
        
        offsrping.Solution = professors.Salary(offsrping.Values);

        return offsrping;
    }
}