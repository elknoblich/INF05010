using INF05010.Structures;

namespace INF05010.Extensions;

public static class ProfessorsExtensions
{
    public static int Salary(this Professor[] professors, byte[] values) => professors.Select((professor, i) => professor.Salary * values[i]).Sum();
}