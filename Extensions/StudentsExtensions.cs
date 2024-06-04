using INF05010.Structures;

namespace INF05010.Extensions;

public static class StudentsExtensions
{
    public static int Hours(this Student[] students) => students.Sum(student => student.Hours * student.Selected);
    public static void Reset(this Student[] students)
    {
        for (int i = 0; i < students.Length; i++)
        {
            students[i].Selected = 0;
        }
    }
}