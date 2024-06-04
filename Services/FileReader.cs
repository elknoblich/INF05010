using INF05010.Structures;

namespace INF05010.Services;

public class FileReader(string[] files)
{
    public string[] Files = files;

    public Parameters[] ReadParametersFromFile() => Files.Select(file =>
    {
        string[] lines = File.ReadLines(file).Take(2).ToArray();

        string[] first = lines[0].Split(',');
        string[] second = lines[1].Split(',');

        return new Parameters
        {
            File = file,
            Professors = int.Parse(first[0]),
            Students = int.Parse(first[1]),
            Hours = int.Parse(second[0]),
            Distance = float.Parse(second[1])
        };
      
    }).ToArray();

    public Professor[][] ReadProfessorsFromFile(Parameters[] parameters) => parameters.Select((parameter, index) =>
    {
        return File.ReadLines(Files[index]).Skip(2).Take(parameter.Professors).Select(line =>
        {
            string[] values = line.Split(',');

            return new Professor
            {
                Salary = int.Parse(values[3]),
                Coordinate = new Coordinate
                {
                    X = double.Parse(values[1]),
                    Y = double.Parse(values[2])
                }
            };

        }).ToArray();

    }).ToArray();

    public Student[][] ReadStudentsFromFile(Parameters[] parameters) => parameters.Select((parameter, index) =>
    {
        return File.ReadLines(Files[index]).Skip(2 + parameter.Professors).Take(parameter.Students).Select(line =>
        {
            string[] values = line.Split(',');

            return new Student
            {
                Hours = short.Parse(values[3]),
                Selected = 0,
                Coordinate = new Coordinate
                {
                    X = double.Parse(values[1]),
                    Y = double.Parse(values[2])
                }
            };

        }).ToArray();

    }).ToArray();
}