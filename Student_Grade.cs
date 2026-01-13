using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public int Id { get; }
    public string Name { get; }
    public string Subject { get; }
    public int Marks { get; }

    public Student(int id, string name, string subject, int marks)
    {
        if (marks < 0 || marks > 100)
            throw new ArgumentOutOfRangeException(nameof(marks), "Marks must be between 0 and 100");

        Id = id;
        Name = string.IsNullOrWhiteSpace(name) ? "Unknown" : name;
        Subject = string.IsNullOrWhiteSpace(subject) ? "General" : subject;
        Marks = marks;
    }

    public string GetGrade()
    {
        if (Marks >= 90) return "A+";   
        if (Marks >= 75) return "A";
        if (Marks >= 60) return "B";
        if (Marks >= 50) return "C";
        return "Fail";
    }
}

class StudentManager
{
    private readonly Dictionary<int, Student> studentsById = new();
    private readonly Dictionary<string, List<Student>> studentsBySubject = new();

    public bool AddStudent(Student student)
    {
        if (studentsById.ContainsKey(student.Id))
            return false;

        studentsById[student.Id] = student;

        if (!studentsBySubject.ContainsKey(student.Subject))
            studentsBySubject[student.Subject] = new List<Student>();

        studentsBySubject[student.Subject].Add(student);
        return true;
    }

    public double GetAverageBySubject(string subject)
    {
        if (!studentsBySubject.ContainsKey(subject) || studentsBySubject[subject].Count == 0)
            return 0;

        return studentsBySubject[subject].Average(s => s.Marks);
    }

    public IEnumerable<Student> GetTopStudents(int minMarks)
    {
        return studentsById.Values
            .Where(s => s.Marks >= minMarks)
            .OrderByDescending(s => s.Marks);
    }

    public IEnumerable<Student> GetFailingStudents(int passMarks)
    {
        return studentsById.Values.Where(s => s.Marks < passMarks);
    }
}

class Program
{
    static void Main()
    {
        StudentManager manager = new StudentManager();

        manager.AddStudent(new Student(1, "Amit", "Math", 85));
        manager.AddStudent(new Student(2, "Riya", "Math", 72));
        manager.AddStudent(new Student(3, "Neha", "Science", 92));
        manager.AddStudent(new Student(4, "Rahul", "Science", 65));
        manager.AddStudent(new Student(5, "Kiran", "Math", 40));

        Console.WriteLine("Top Students (>= 85):");
        foreach (var s in manager.GetTopStudents(75))
            Console.WriteLine($"{s.Name} - {s.Marks} - Grade {s.GetGrade()}");

        Console.WriteLine("\nAverage Marks (Math):");
        Console.WriteLine(manager.GetAverageBySubject("Math"));

        Console.WriteLine("\nFailing Students (< 50):");
        foreach (var s in manager.GetFailingStudents(50))
            Console.WriteLine($"{s.Name} - {s.Marks} - Grade {s.GetGrade()}");
    }
}
