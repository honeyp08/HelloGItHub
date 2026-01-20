using System;
using TaskManagerApp.Models;
using TaskManagerApp.Services;

namespace TaskManagerApp
{
    class Program
    {
        static TaskService taskService = new TaskService();

        static void Main()
{
    while (true)
    {
        RunTaskManager(); // Run menu
    }
}

static void RunTaskManager()
{
    while (true)
    {
        Console.WriteLine("\n--- Task Manager ---");
        Console.WriteLine("1. Add Task"); 
        Console.WriteLine("2. View Tasks");
        Console.WriteLine("3. Mark Completed Task");
        Console.WriteLine("4. Delete Task");
        Console.WriteLine("5. Exit Task");
        Console.Write("Choice : ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": 
                AddNewTask(); 
                break;
            case "2": 
                taskService.ViewTasks(); 
                break;
            case "3": 
                MarkTaskComplete(); 
                break;
            case "4": 
                DeleteTask(); 
                break;
            case "5":
                taskService.SaveTasks();
                Console.WriteLine("\nTasks saved successfully.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                break;;

        }
    }
}


        static void AddNewTask()
        {
            Console.Write("Title: "); string title = Console.ReadLine();


            
            Console.Write("Type (1 for Personal, 2 for Work): ");
            string type = Console.ReadLine();

            TaskItem newTask = type == "2" ? new WorkTask() : new PersonalTask();
            newTask.Title = title;
            newTask.DueDate = DateTime.Now.AddDays(1);

            taskService.AddTask(newTask);
        }

        static void MarkTaskComplete()
        {
            Console.Write("Enter Task ID to mark complete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
                taskService.UpdateTaskStatus(id);
        }

        static void DeleteTask()
        {
            Console.Write("Enter Task ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
                taskService.DeleteTask(id);
        }
    }
}