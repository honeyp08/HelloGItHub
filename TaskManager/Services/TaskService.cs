using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TaskManagerApp.Models;

namespace TaskManagerApp.Services
{
    public class TaskService
    {
        private readonly string filePath = "tasks.json";
        private List<TaskItem> tasks = new List<TaskItem>();

        public TaskService()
        {
            LoadTasks();
        }

        public void AddTask(TaskItem task)
        {
            task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
            tasks.Add(task);
            Console.WriteLine(" Task added successfully!");
        }

        public void ViewTasks()
        {
            Console.WriteLine("\n--- Your Tasks (Sorted by Date) ---");
            foreach (var task in tasks.OrderBy(t => t.DueDate))
            {
                task.DisplayTask();
            }
        }

        public void UpdateTaskStatus(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsComplete = true;
                Console.WriteLine(" Marked as complete!");
            }
        }

        public void DeleteTask(int id)
        {
            tasks.RemoveAll(t => t.Id == id);
            Console.WriteLine(" Task deleted!");
        }

        public void SaveTasks()
        {
            var options = new JsonSerializerOptions { WriteIndented = true, Converters = { new TaskJsonConverter() } };
            string json = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(filePath, json);
            Console.WriteLine(" Data saved to file!");
        }

        private void LoadTasks()
        {
            if (!File.Exists(filePath)) return;

            var options = new JsonSerializerOptions { Converters = { new TaskJsonConverter() } };
            string json = File.ReadAllText(filePath);
            tasks = JsonSerializer.Deserialize<List<TaskItem>>(json, options) ?? new List<TaskItem>();
        }
    }
}