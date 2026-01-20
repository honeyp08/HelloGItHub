using System;

namespace TaskManagerApp.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsComplete { get; set; }
        public string TaskType { get; set; }

        public virtual void DisplayTask()
        {
            string status = IsComplete ? " Done" : " Pending";
            Console.WriteLine($"[{Id}] {Title} ({TaskType}) - Due: {DueDate.ToShortDateString()} - {status}");
        }
    }
}
