using RandomPerson.Models;

namespace RandomPerson.Services
{
    public static class DrawService
    {
        public static int LuckyNumber { get; set; } = 0;

        public static Student? DrawStudent(Class classData)
        {
            var students = classData.Students;

            foreach (var s in students)
            {
                if (s.DrawnCount > 0) s.DrawnCount--;
            }

            var availableStudents = students
                .Select((s, index) => new { Student = s, Number = index + 1 })
                .Where(x => x.Student.IsPresent && x.Student.DrawnCount == 0 && x.Number != LuckyNumber)
                .Select(x => x.Student)
                .ToList();

            if (!availableStudents.Any())
            {
                availableStudents = students
                    .Where(s => s.IsPresent && s.DrawnCount == 0)
                    .ToList();
            }

            if (!availableStudents.Any()) return null;

            var drawnStudent = availableStudents[Random.Shared.Next(availableStudents.Count)];
            drawnStudent.DrawnCount = 4;

            return drawnStudent;
        }
    }
}
