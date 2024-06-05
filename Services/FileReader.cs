using INF05010.Structures;

namespace INF05010.Services;

public static class FileReader
{
    public static Information ReadInformation(string file)
    {
        Parameters parameters = ReadParameters(file);

        DisplayParameters(file, parameters);

        return new Information
        {
            Parameters = parameters,
            Professors = ReadProfessors(file, parameters.NumberOfProfessors),
            Students = ReadStudents(file, parameters.NumberOfProfessors, parameters.NumberOfStudents)
        };
    }
    
    private static Parameters ReadParameters(string file)
    {
        string[] values = File.ReadLines(file).Take(2).SelectMany(line => line.Split(',')).ToArray();

        return new Parameters
        {
            NumberOfProfessors = int.Parse(values[0]),
            NumberOfStudents = int.Parse(values[1]),
            MinimumHours = int.Parse(values[2]),
            MaximumDistance = float.Parse(values[3])
        };
    }

    private static Professor[] ReadProfessors(string file, int numberOfProfessors) => File.ReadLines(file).Skip(2).Take(numberOfProfessors).Select(line =>
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

    private static Student[] ReadStudents(string file, int numberOfProfessors, int numberOfStudents) => File.ReadLines(file).Skip(2 + numberOfProfessors).Take(numberOfStudents).Select(line =>
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

    private static void DisplayParameters(string file, Parameters parameters) => Console.WriteLine($"File: {file}\nNumber of Professors: {parameters.NumberOfProfessors}\nNumber of Students: {parameters.NumberOfStudents}\nMinimum Hours: {parameters.MinimumHours}\nMaximum Distance: {parameters.MaximumDistance}");
}