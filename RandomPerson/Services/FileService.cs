namespace RandomPerson.Services;

using RandomPerson.Models;

public static class FileService
{
    private static string BasePath =>
        Path.Combine(FileSystem.AppDataDirectory, "classes");

    public static void EnsureDirectory() =>
        Directory.CreateDirectory(BasePath);

    public static List<string> GetClassNames()
    {
        EnsureDirectory();
        return Directory.GetFiles(BasePath, "*.txt")
                        .Select(Path.GetFileNameWithoutExtension)
                        .ToList()!;
    }

    public static void SaveClass(Class classData)
    {
        EnsureDirectory();
        var path = Path.Combine(BasePath, $"{classData.Name}.txt");
        var lines = classData.Students.Select(s =>
            $"{s.Name};{s.IsPresent};{s.DrawnCount}");
        File.WriteAllLines(path, lines);
    }

    public static Class LoadClassData(string className)
    {
        return new Class
        {
            Name = className,
            Students = LoadStudents(className)
        };
    }

    private static List<Student> LoadStudents(string className)
    {
        EnsureDirectory();
        var path = Path.Combine(BasePath, $"{className}.txt");
        if (!File.Exists(path)) return new();

        return File.ReadAllLines(path)
                   .Where(l => !string.IsNullOrWhiteSpace(l))
                   .Select(ParseStudent)
                   .ToList();
    }

    private static Student ParseStudent(string line)
    {
        var parts = line.Split(';');
        var drawnCountIndex = parts.Length > 3 ? 3 : 2;
        var drawnCount = parts.Length > drawnCountIndex && int.TryParse(parts[drawnCountIndex], out var d) ? d : 0;

        return new Student
        {
            Name = parts[0],
            IsPresent = parts.Length > 1 && bool.TryParse(parts[1], out var p) && p,
            DrawnCount = drawnCount
        };
    }

    public static void DeleteClass(string className)
    {
        var path = Path.Combine(BasePath, $"{className}.txt");
        if (File.Exists(path)) File.Delete(path);
    }
}