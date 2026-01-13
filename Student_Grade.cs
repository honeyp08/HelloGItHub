using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
    public int Marks { get; set; }
}

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Amit", Subject = "Math", Marks = 85 },
            new Student { Id = 2, Name = "Riya", Subject = "Math", Marks = 72 },
            new Student { Id = 3, Name = "Neha", Subject = "Science", Marks = 92 },
            new Student { Id = 4, Name = "Rahul", Subject = "Science", Marks = 65 },
            new Student { Id = 5, Name = "Kiran", Subject = "Math", Marks = 40 }
        };


        Console.WriteLine("Top Students (>= 85):");
        var highScorers = students.Where(s => s.Marks >= 75);

        foreach (var s in highScorers)
            Console.WriteLine($"{s.Name} - {s.Subject} - {s.Marks}");


        Console.WriteLine("\nAverage Marks by Subject:");
        var avgBySubject = students
            .GroupBy(s => s.Subject)
            .Select(g => new
            {
                Subject = g.Key,
                Average = g.Average(s => s.Marks)
            });

        foreach (var item in avgBySubject)
            Console.WriteLine($"{item.Subject}: {item.Average:F2}");

        // Data Visualization
        Console.WriteLine("\nMarks Visualization:");
        foreach (var s in students)
        {
            Console.Write($"{s.Name,-10} || ");
            Console.WriteLine(new string('*', s.Marks / 2));
        }
    }
}