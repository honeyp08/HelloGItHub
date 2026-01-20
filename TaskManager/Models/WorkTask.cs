using TaskManagerApp.Interfaces;

namespace TaskManagerApp.Models
{
    public class WorkTask : TaskItem, IPrioritizable
    {
        public string Priority { get; set; } = "Normal";

        public WorkTask() => TaskType = "Work";

        public override void DisplayTask()
        {
            base.DisplayTask();
            Console.WriteLine($"   Priority: {Priority}");
        }
    }
}
